using System.Text.Json.Serialization;

namespace InsuranceINNOTech;

public class Hospital : BaseSettingEntity
{
    public ICollection<HospitalPlan>? hospitalPlans { get; set; }
    [JsonIgnore]
    public ICollection<User>? users { get; set; }
    [JsonIgnore]
    public ICollection<Dependent>? dependents { get; set; }

}
