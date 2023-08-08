namespace InsuranceINNOTech.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseSettingController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public UserController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
    }
       

    
    [HttpGet,Authorize]
    public  async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();
        return await Read(userId);
    }

    
    [HttpPut,Authorize]
    public async Task<IActionResult> Put([FromForm]User userRequest)
    {
        Guid userId = GetUserId();

        User user = await _userUnitOfWork.Update(userRequest);

        ResponseResult<User> response = new(user);

        return Ok(response);
    }

    
    [HttpPut, Route("UpdatePassword"),Authorize]
    public  async Task<IActionResult> Put(PasswordRequest passwordRequest)
    {
        Guid userId = GetUserId();
        Token token = await _userUnitOfWork.UpdatePassword(passwordRequest, userId);

        SetCookie("AccessToken",
            token.AccessToken,
             token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAt);

        ResponseResult<Token> response = new(token);
        return Ok(response);
   
    }

    
    [HttpDelete,Authorize]
    public async Task<IActionResult> Delete()
    {
        Guid id = GetUserId();

        User user = await _userUnitOfWork.Read(id);

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");

        return await Delete(user);
    }
}

