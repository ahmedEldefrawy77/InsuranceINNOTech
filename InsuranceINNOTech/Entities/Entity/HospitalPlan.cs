namespace InsuranceINNOTech;

public class HospitalPlan
{
    public Hospital? hospital { get; set; }
    public Guid HospitalId { get; set; }
    public Plans? plans { get; set; }
    public Guid planId {  get; set; }
}
