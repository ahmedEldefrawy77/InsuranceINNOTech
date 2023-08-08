namespace InsuranceINNOTech;

[Route("api/[controller]")]

[ApiController]
public class loginController : BaseSettingController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public loginController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
    }

    [HttpPost]
    public  async Task<IActionResult> Post(UserRequest userRequest)
    {
        Token token = await _userUnitOfWork.Loggin(userRequest);

        SetCookie("AccessToken",
            token.AccessToken,
             token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken!,
            token.RefreshTokenExpiresAt);

        ResponseResult<Token> response = new(token);

        return Ok(response);
    }
}
