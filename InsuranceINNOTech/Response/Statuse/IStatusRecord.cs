namespace InsuranceINNOTech;

public interface IStatusRecord 
{
    Task<IEnumerable<User>> UsersOverAge(int age);
    Task<IEnumerable<User>> UsersUnderAndEqualAge(int age);
    Task<IEnumerable<User>> UsersGender(GenderRecord genderSpec);
}
