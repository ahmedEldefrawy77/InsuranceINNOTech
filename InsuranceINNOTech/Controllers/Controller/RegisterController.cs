using FluentValidation;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InsuranceINNOTech;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(UserRegisterFiltersAttributes<User>))]

public class RegisterController : BaseSettingController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IValidator<User> _validator;
   
    public RegisterController(IUserUnitOfWork userUnitOfWork, IValidator<User> validate)
        : base(userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
        _validator = validate;
       
    }

    [HttpPost]
    [UserRegisterFiltersAttributes<User>()]
    public  async Task<IActionResult> Post(User user)
    {
        var result= await _validator.ValidateAsync(user);
        
        if (!result.IsValid)
        {
           BindingList<string> errors = new BindingList<string>();
           foreach(var validationFailure in result.Errors)
           {
                errors.Add($"{validationFailure.PropertyName}:{validationFailure.ErrorMessage}");
           }
           ResponseResult<BindingList<string>> resultError = new(errors);

            return Ok(resultError);
        }

        Token token = await _userUnitOfWork.Register(user);

        SetCookie("AccessToken",
            token.AccessToken,
            token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAt);

        ResponseResult<Token> response = new(token);
        return Ok(response);
    }
}
