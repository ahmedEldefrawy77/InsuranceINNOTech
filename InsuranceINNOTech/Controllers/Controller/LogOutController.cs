namespace InsuranceINNOTech;

[Route("api/[controller]")]
[ApiController]
public class LogOutController : BaseSettingController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public LogOutController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Logout(Token? refreshToken)
    {
        var oldToken = Request.Cookies["RefreshToken"]??string.Empty;

        if (refreshToken != null && refreshToken.RefreshToken != null)
            oldToken = refreshToken.RefreshToken;
            
        await _userUnitOfWork.Logout(oldToken);

        Response.Cookies.Delete("RefreshToken");
        Response.Cookies.Delete("AccessToken");

        ResponseResult<string> response = new ("Logout Success");
        return Ok(response);
    }
}
