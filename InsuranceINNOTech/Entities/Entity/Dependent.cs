    namespace InsuranceINNOTech;

public class Dependent : BaseSettingEntity
{
    public string? Email { get; set; }
    public int Age { get; set; }
    public int Telephone { get; set; }
    public User? user { get; set; }
    public Guid UserId { get; set; }
    public Plans? plans { get; set; }
    public Guid PlansId { get; set; }

    public Hospital? hospital { get; set; }
    public Guid hospitalId { get; set; }
    public ICollection<Claims>? claims { get; set; }
     
}
