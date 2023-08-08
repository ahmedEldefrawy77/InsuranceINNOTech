namespace InsuranceINNOTech;

public interface IJwtProvider
{
    string GenrateAccessToken(User user);
    string GenrateRefreshToken();

}
