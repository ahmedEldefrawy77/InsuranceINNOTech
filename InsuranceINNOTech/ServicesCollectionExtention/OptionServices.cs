namespace InsuranceINNOTech;

public static class OptionServices
{
    public static void AddOptionService(this IServiceCollection services)
    {
        services.AddOptions();
        services.ConfigureOptions<JwtAccessOptionsSetup>();
        services.ConfigureOptions<JwtRefreshOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

    }
}
