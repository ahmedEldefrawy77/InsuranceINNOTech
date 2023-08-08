namespace InsuranceINNOTech;

public class UserPlans
{
    public User user { get; set; } = null!;
    public Guid userId { get; set; }
    public Plans? plan { get; set; }
    public Guid plainId { get; set; }
}
