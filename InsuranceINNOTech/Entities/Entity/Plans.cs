using System.Text.Json.Serialization;

namespace InsuranceINNOTech;
    public class Plans : BaseSettingEntity
    {
        public double Cost { get; set; }
        public ICollection<HospitalPlan>? hospitalPlans { get; set; }
        [JsonIgnore]
        public ICollection<Dependent>? dependents { get; set; }
        [JsonIgnore]
         public ICollection<UserPlans>? userPlan { get; set; }

    }
