using System.ComponentModel.DataAnnotations.Schema;

namespace unit_of_work_sample.Entities;

public class Student : BaseEntity
{
    [ForeignKey("School")]
    public int SchoolId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public School School { get; set; }
}
