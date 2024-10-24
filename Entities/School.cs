namespace unit_of_work_sample.Entities;

public class School : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime EstablishedDate { get; set; }
    public Decimal AnnualBudget { get; set; }

    public ICollection<Student> Students { get; set; }
}
