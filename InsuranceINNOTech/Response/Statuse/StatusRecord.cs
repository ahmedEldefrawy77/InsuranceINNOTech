namespace InsuranceINNOTech;

public class StatusRecord : IStatusRecord
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public StatusRecord(IUserUnitOfWork userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
    }


    public async Task<IEnumerable<User>> UsersOverAge(int age)
    {
        IEnumerable<User> UserList = new List<User>();
        UserList = await _userUnitOfWork.ReadAll();

        List<User> UsersOver18 = new List<User>();

        foreach (User user in UserList)
        {
            if (user.Age > age)
                UsersOver18.Add(user);
        }
        return UsersOver18;
    }
    public async Task<IEnumerable<User>> UsersUnderAndEqualAge(int age)
    {
        IEnumerable<User> UserList = new List<User>();
        UserList = await _userUnitOfWork.ReadAll();

        List<User> UsersUnderAndEqual18 = new List<User>();

        foreach (User user in UserList)
        {
            if (user.Age <= age)
                UsersUnderAndEqual18.Add(user);
        }
        return UsersUnderAndEqual18;
    }
    public async Task<IEnumerable<User>>UsersGender(GenderRecord genderSpec)
    {
        IEnumerable<User> UserList = new List<User>();
        UserList = await _userUnitOfWork.ReadAll();

        List<User> UsersGender = new List<User>();
        foreach (User user in UserList)
        {
            if (user.Gender.CompareTo(genderSpec.Gender) == 0)
            {
                UsersGender.Add(user);
            }

        }
        return UsersGender;
    }
}

   
   

        

