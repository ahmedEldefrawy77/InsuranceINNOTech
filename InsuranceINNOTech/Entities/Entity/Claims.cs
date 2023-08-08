namespace InsuranceINNOTech;

public class Claims : BaseEntity
{
    public DateTime? DateCreatedAt { get; set; }
    public float? Expenses { get; set; }
    public string? Discription { get; set; }
    public User? user { get; set; }
    public Dependent? dependent { get; set; }
    public Guid userId { get; set; }
    public Guid dependentId { get; set; }   
}
