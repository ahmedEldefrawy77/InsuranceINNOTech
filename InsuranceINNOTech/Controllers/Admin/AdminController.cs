using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace InsuranceINNOTech.Server.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AdminController : BaseSettingController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IStatusRecord _statusRecord;
    public AdminController(IUserUnitOfWork userUnitOfWork, IStatusRecord statusRecord) : base(userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
        _statusRecord = statusRecord;

    }


    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        IEnumerable<User> users = await _userUnitOfWork.ReadAll();

        List<ResponseResult<User>> resultList = new List<ResponseResult<User>>();

        foreach (var user in users)
        {
            ResponseResult<User> response = new(user);

            resultList.Add(response);
        }
        return Ok(resultList);
    }


    [HttpGet,Route("GetUser"), Authorize(Roles ="Admin")]
    public async Task<IActionResult> Get(string Mail)
    {
         User user = await _userUnitOfWork.SearchByEmail(Mail);
        ResponseResult<User> response = new(user);
        return Ok(response);
    }


    [HttpGet,Route("GetUserbyId"), Authorize]
    public async Task Get(Guid id)
    {
        await Read(id);
    }


    [HttpPut, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(User userRequet)
    {
        User user  =  await _userUnitOfWork.Update(userRequet);
        ResponseResult<User> response = new(user);
        return Ok(response);
    }


    [HttpDelete , Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(string Mail)
    {
        await _userUnitOfWork.DeleteByEmail(Mail);
        ResponseResult<string> response = new ResponseResult<string>("entity deleted successfully");

        return Ok();
    }

    [HttpGet,Route("GetOverAge"),Authorize(Roles ="Admin")]
    public async Task<IActionResult> GetOverAge(int Age)
    {
        IEnumerable<User> UsersOverAge = new List<User>();
        UsersOverAge =  await _statusRecord.UsersOverAge(Age);

        List<ResponseResult<User>> ResponseList = new List<ResponseResult<User>>();
        foreach (User user in UsersOverAge)
        {
            ResponseResult<User> response = new(user);
            ResponseList.Add(response);
        } 
        return Ok(ResponseList);
    }

    [HttpGet, Route("GetUnderAndEqualAge"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUnderAge(int Age)
    {
        IEnumerable<User> UsersOverAge = new List<User>();
        UsersOverAge = await _statusRecord.UsersUnderAndEqualAge(Age);

        List<ResponseResult<User>> ResponseList = new List<ResponseResult<User>>();
        foreach (User user in UsersOverAge)
        {
            ResponseResult<User> response = new(user);
            ResponseList.Add(response);
        }
        return Ok(ResponseList);
    }

    [HttpGet, Route("GetGender"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetGender(GenderRecord spec)
    {
        IEnumerable<User> UsersOverAge = new List<User>();
        UsersOverAge = await _statusRecord.UsersGender(spec);

        List<ResponseResult<User>> ResponseList = new List<ResponseResult<User>>();
        foreach (User user in UsersOverAge)
        {
            ResponseResult<User> response = new(user);
            ResponseList.Add(response);
        }
        return Ok(ResponseList);
    }
}
