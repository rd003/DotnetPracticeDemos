using SelectManyDemo.Models;

namespace SelectManyDemo.ExtensionMethods;

public static class DbInitializer
{
    public static void InitializeDb(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            if (context.Departments.Any() == false)
            {
                List<Department> departments = [
                    new Department { Name = "Engineering" },
        new Department { Name = "Marketing" },
        new Department { Name = "HR" }
                    ];
                context.Departments.AddRange(departments);
                context.SaveChanges();
            }

            if (context.Employees.Any() == false)
            {
                List<Employee> employees = [
                new Employee {  Name = "John Smith", DepartmentId = 1 },
new Employee {   Name = "Jane Doe", DepartmentId = 1 },
new Employee{Name = "Bob Wilson",DepartmentId = 2}];
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            if (context.Skills.Any() == false)
            {
                List<Skill> skills = [
                new Skill { Name = "C#" },
new Skill {  Name = "SQL" },
new Skill {  Name = "Python" },
new Skill { Name = "Digital Marketing" }];
                context.Skills.AddRange(skills);
                context.SaveChanges();
            }

            if (context.EmployeeSkills.Any() == false)
            {
                List<EmployeeSkill> empSkills = [
                    new EmployeeSkill { EmployeeId = 1, SkillId = 1 },
                    new EmployeeSkill { EmployeeId = 1, SkillId = 2},
                    new EmployeeSkill { EmployeeId = 2, SkillId = 1 },
                    new EmployeeSkill { EmployeeId = 2, SkillId = 3},
                    new EmployeeSkill { EmployeeId = 3, SkillId = 4}
                ];

                context.EmployeeSkills.AddRange(empSkills);
                context.SaveChanges(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}