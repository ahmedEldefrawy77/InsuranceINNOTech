using Newtonsoft.Json;

namespace InsuranceINNOTech;

public class User : BaseSettingEntity 
{
    public string? Mail { get; set; }
    public string? Password { get; set; }
    public string? Gender { get; set; }
    public string? Role { get; set; } = "User";
    public int Age { get; set; }
    public int? Telephone { get; set; }
    public ICollection<UserPlans>? userPlan { get; set; }
    public ICollection<Dependent>? Dependents { get; set; }
    public ICollection<Claims>? claims { get; set; }
    public Hospital? hospital { get; set; }
    public Guid? hospitalId { get; set; }
    [JsonIgnore]
    public RefreshToken? RefreshToken { get; set; } 
   
}
