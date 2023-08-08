using FluentValidation;
using System.Data;

namespace InsuranceINNOTech;

public static class DependancyInjectionService
{
   
    public static void AddDependencyInjectionService(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

       

        services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddSingleton(typeof(IBaseSettingRepository<>), typeof(BaseSettingRepository<>));
        services.AddSingleton(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
        services.AddSingleton(typeof(IBaseSettingUnitOfWork<>), typeof(BaseSettingUnitOfWork<>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDependentRepository, DependentRepository>();
        services.AddScoped<IHospitalRepository, HospitalRepository>();
         services.AddScoped<IPlaneRepository, PlaneRepository>();
        services.AddScoped<IClaimsRepository, ClaimsRepository>();

        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
        services.AddScoped<IDependentUnitOfWork, DependentUnitOfWork>();
        services.AddScoped<IHospitalUnitOfWork, HospitalUnitOfWork>();
        services.AddScoped<IPlaneUnitOfWork, PlaneUnitOfWork>();
        services.AddScoped<IClaimsUnitOfWork, ClaimsUnitOfWork>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<RefreshTokenValidator>();

        services.AddTransient<GlobalErrorHandlerMiddleware>();
        services.AddTransient<TransactionHandlingMiddleware>();
       

        services.AddScoped<IValidator<User> , UserRegisterValidator>();
        services.AddScoped<IStatusRecord, StatusRecord>();

        services.AddSignalR();
    }
}
