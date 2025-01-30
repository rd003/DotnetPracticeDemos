namespace SelectManyDemo.Models;
public class EmployeeSkill
{
    public int EmployeeId { get; set; }
    public int SkillId { get; set; }

    public Employee Employee { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
}