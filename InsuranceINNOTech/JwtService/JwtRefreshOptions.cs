namespace InsuranceINNOTech;

public record JwtRefreshOptions 
{
    public string? SecretKey { get; init; }
    public int ExpireTimeInMonths { get; init; }
}