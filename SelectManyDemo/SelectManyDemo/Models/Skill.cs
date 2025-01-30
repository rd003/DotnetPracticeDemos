namespace SelectManyDemo.Models;

public class Skill
{
    public int SkillId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<EmployeeSkill> EmployeeSkills { get; set; } = [];

}