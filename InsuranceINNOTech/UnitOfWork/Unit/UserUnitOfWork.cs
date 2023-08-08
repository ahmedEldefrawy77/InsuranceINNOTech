using FluentValidation;

namespace InsuranceINNOTech;

public class UserUnitOfWork : BaseSettingUnitOfWork<User>, IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUnitOfWork> _Logger;
    private readonly IJwtProvider _jwt;
    private readonly RefreshTokenValidator _validator;
    private readonly IRefreshTokenRepository _RefreshTokenRepository;
    private readonly JwtRefreshOptions _JwtRefreshOptions;
    private readonly JwtAccessOptions _JwtAccessOptions;
    private readonly IValidator<User> _UserValidator;

    public UserUnitOfWork(IUserRepository userRepository,
        ILogger<UserUnitOfWork> logger,
        IJwtProvider jwt,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtRefreshOptions> jwtRefeshTokenOptions,
        IOptions<JwtAccessOptions> jwtAccessOptions,
        RefreshTokenValidator validator)
        : base(userRepository, logger)
    {
        _Logger = logger;
        _userRepository = userRepository;
        _jwt = jwt;
        _validator = validator;
        _RefreshTokenRepository = refreshTokenRepository;
        _JwtAccessOptions = jwtAccessOptions.Value;
        _JwtRefreshOptions = jwtRefeshTokenOptions.Value;
    }

    public async Task<User> SearchByEmail(string email) => await _userRepository.SearchByMail(email);
    public async Task DeleteByEmail(string email)
    { 
        using IDbContextTransaction transaction =  await _userRepository.GetTransaction();
        try
        {
            await _userRepository.DeleteByMail(email);
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _Logger.LogError(ex.Message);
        }
       await transaction.CommitAsync();
    }
    public async Task<IEnumerable<User>> ReadUsers() => await _userRepository.GetAll();
    public async Task<Token> ReadUser(User user)
    {
        User userFromDb = await _userRepository.Get(user.Id);

        if (userFromDb == null)
            throw new ArgumentException("there is no user with such Id, Pleas try again Later");

        if (!_validator.Validate(userFromDb.RefreshToken.Value))
           userFromDb.RefreshToken =
                GenerateNewRefreshToken(userFromDb.Id, userFromDb.RefreshToken.Id); // generating new Refresh Token based on User id

        string AccessToken = _jwt.GenrateAccessToken(userFromDb);

        Token token = new()
        {
            AccessToken = AccessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_JwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.RefreshToken.Value,
            RefreshTokenExpiresAt = DateTime.UtcNow.AddMonths(_JwtRefreshOptions.ExpireTimeInMonths)
        };
        return token;
    }
    public async Task<Token> GetUserByToken(string refreshT)
    {
        User userFromDb = await _userRepository.GetbyToken(refreshT);
        if (userFromDb == null)
            throw new ArgumentNullException("invalid Token");

        Token token = new Token()
        {
            AccessToken = _jwt.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_JwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.RefreshToken?.Value,
            RefreshTokenExpiresAt = userFromDb.RefreshToken.DateExAt
        };
        return token;
    }

    public async override Task Create(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await base.Create(user);
    }

    public async Task<Token> Register(User user)
    {
        // Generating a new Refresh token with Default id
        user.RefreshToken = GenerateNewRefreshToken();

        await this.Create(user);

        Token token = new()
        {
            AccessToken = _jwt.GenrateAccessToken(user),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_JwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = user.RefreshToken.Value,
            RefreshTokenExpiresAt = DateTime.UtcNow.AddMonths(_JwtRefreshOptions.ExpireTimeInMonths)
        };
       
        return token;
    }

    public async Task<Token> Loggin(UserRequest userRequest)
    {
        if (userRequest == null)
            throw new ArgumentNullException("this User is Invalid");

        User? UserFromDb = await SearchByEmail(userRequest.Mail);
        if (UserFromDb == null)
            throw new ArgumentException("there is no User with Such Email");

         
        if (!BCrypt.Net.BCrypt.Verify(userRequest.Password, UserFromDb.Password))
            throw new ArgumentException("Password is not Correct");

        if (!_validator.Validate(UserFromDb.RefreshToken.Value))
            UserFromDb.RefreshToken =
                GenerateNewRefreshToken(UserFromDb.Id, UserFromDb.RefreshToken.Id);// generating new Refresh Token based on User Id


        await Update(UserFromDb);

        var AccessToken = _jwt.GenrateAccessToken(UserFromDb);
        Token token = new()
        {
            AccessToken = AccessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_JwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = UserFromDb.RefreshToken.Value,
            RefreshTokenExpiresAt = DateTime.UtcNow.AddMonths(_JwtRefreshOptions.ExpireTimeInMonths)
        };

        return token;
    }
    public async Task Logout(string refreshToken)
    {
        User userFromDb = await _userRepository.GetbyToken(refreshToken);
        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        using IDbContextTransaction transaction = await _RefreshTokenRepository.GetTransaction();

        try
        {
            await _RefreshTokenRepository.Remove(userFromDb.RefreshToken.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            _Logger.LogError(ex.Message);
        }
        await transaction.CommitAsync();

    }

    public async Task<User> Update(User userRequest)
    {
        User? UserFromDb = await SearchByEmail(userRequest.Mail!);
        if (UserFromDb == null)
            throw new ArgumentNullException("this User was not found");

        User user = new()
        {
            Id = UserFromDb.Id,
            Name = userRequest.Name,
            Password = UserFromDb.Password,
            Mail = userRequest.Mail,
            Role = UserFromDb.Role,
            Gender = UserFromDb.Gender,
            Age  = userRequest.Age,
            Telephone = userRequest.Telephone,
           RefreshToken = userRequest.RefreshToken,
        };
        await UpdateAsync(user);
        return user;
    }

    public async Task<Token> UpdatePassword(PasswordRequest passwordRequest, Guid id)
    {
        User? userFromDb = await _userRepository.Get(id);
        if (userFromDb == null)
            throw new ArgumentException("invalid User");

        if (!BCrypt.Net.BCrypt.Verify(userFromDb.Password, passwordRequest.OldPassword))
            throw new ArgumentException("Password doesnt matched, please try again later");

        if (passwordRequest.NewPassword == null || passwordRequest.NewPassword.Length < 4)
            throw new ArgumentException("Password can not be null or less then 4 chrarachters ");

        if (!_validator.Validate(userFromDb.RefreshToken!.Value))
            GenerateNewRefreshToken(userFromDb.Id, userFromDb.RefreshToken.Id);
        string AccessToken = _jwt.GenrateAccessToken(userFromDb);

        userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(passwordRequest.NewPassword);

        await UpdateAsync(userFromDb);
        Token token = new()
        {
            AccessToken = AccessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_JwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.RefreshToken.Value,
            RefreshTokenExpiresAt = DateTime.UtcNow.AddMonths(_JwtRefreshOptions.ExpireTimeInMonths),
        };

        return token;
    }
    private RefreshToken GenerateNewRefreshToken(Guid userId = default(Guid)
        , Guid id = default(Guid))
    {
        string refreshTokenValue = _jwt.GenrateRefreshToken();
        RefreshToken refreshToken = new()
        {
            Id = id,
            Value = refreshTokenValue,
            DateCreatedAt = DateTime.UtcNow,
            DateExAt = DateTime.UtcNow.AddMonths(_JwtRefreshOptions.ExpireTimeInMonths),
            UserId = userId
            
        };
        return refreshToken;
    }
}
