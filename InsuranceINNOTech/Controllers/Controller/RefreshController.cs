namespace InsuranceINNOTech;

[Route("api/[controller]")]
    [ApiController]
    public class RefreshController : BaseSettingController<User>
    {
      private readonly IUserUnitOfWork _userUnitOfWork;
      public RefreshController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork) =>
        _userUnitOfWork = userUnitOfWork;
     [HttpPost]
        public async Task<IActionResult>Post(Token refreshToken)
        {
            string OldToken = refreshToken.RefreshToken ??
            Request.Cookies["RefreshToken"] ??
            string.Empty;

         Token token = await _userUnitOfWork.GetUserByToken(OldToken);
    
            SetCookie("AccessToken",
            token.AccessToken,
            token.AccessTokenExpiresAt);

            SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAt);

         ResponseResult<Token> result = new(token);
         return Ok(result);
        }
    }