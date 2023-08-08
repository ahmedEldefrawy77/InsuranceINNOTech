namespace InsuranceINNOTech;

public class JwtRefreshOptionsSetup : OptionSetup<JwtRefreshOptions>
{
    public JwtRefreshOptionsSetup(IConfiguration configuration, string sectionName = "JwtRefresh")
        : base(configuration, sectionName) { }

}
