

namespace InsuranceINNOTech;
public class UserRegisterValidator : AbstractValidator<User>
{
    private readonly IUserRepository _userRepository;
    public UserRegisterValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(u => u.Mail)
         .MustAsync(async (Mail, _) =>
        {
            return await IsEmailUniq(Mail);
        })
         .WithMessage("{PropertyName} There is An E-mail with the same Name, pleas choose another email")
         .NotEmpty()
         .WithMessage("{PropertyName} is Empty")
         .Length(4, 50)
         .WithMessage("{PropertyName} must be at least 4 charachter and maximum 50 charachter");

        RuleFor(u => u.Name)   
        .NotEmpty().WithMessage("{PropertyName} is Empty")
        .Length(4, 20).WithMessage("{PropertyName} must be at least 4 charachter and maximum 10 charachter")
        .Matches("^[a-zA-Z]*$").WithMessage("Name should not contain any special characters or numbers.")
        .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Charachters");

        RuleFor(u => u.Password)
         .NotEmpty()
         .WithMessage(" is Empty")
         .Length(4, 50)
         .WithMessage("{PropertyName} must be at least 4 charachter and maximum 50 charachter");

        RuleFor(u => u.Age)
         .NotEmpty()
         .WithMessage("Has to be filled")
         .Must(BeAValidAge)
         .WithMessage("is invalid, your Age has to be between 16 and 120 years in order to be able to Register");

        RuleFor(u => u.Telephone)
        .NotEmpty()
        .WithMessage("Has to be filled");
        


    }
    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(char.IsLetter);
    }
    protected bool BeAValidAge(int age)
    {
        if(age <= 121 && age < 16)
        {
            return false;
        }
        return true;
    }
    protected async Task<bool> IsEmailUniq(string mail)
    {
        User user = await _userRepository.SearchByMail(mail);
        if (user == null)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
}

