using System.Threading.Tasks;
using AzureBlobDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzureBlobDemo.Controllers;

// [Authorize]
public class PersonController : Controller
{
    private readonly AppDbContext _dbcontext;

    public PersonController(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<IActionResult> Index()
    {
        var people = await _dbcontext.People.ToListAsync();
        return View(people);
    }

    public IActionResult CreatePerson()
    {
        return View(new PersonDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonDto personToCreate)
    {
        if (!ModelState.IsValid)
        {
            return View(personToCreate);
        }
        try
        {
            if (personToCreate.File.Length > 300 * 1024)
            {
                personToCreate.ErrorMessage = "File can not exceed 300kb length";
                return View(personToCreate);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(personToCreate.File.FileName)?.ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                personToCreate.ErrorMessage = "You can only upload .jpg, .jpeg, .png files";
                return View(personToCreate);
            }

            // TODO: refactor this logic
            string profilePicUrl = personToCreate.File.FileName;
            var person = new Person
            {
                FirstName = personToCreate.FirstName,
                LastName = personToCreate.LastName,
                ProfilePicture = profilePicUrl
            };
            _dbcontext.Add(person);
            await _dbcontext.SaveChangesAsync();
            personToCreate.SuccessMessage = "Saved successfully";
        }
        catch (Exception ex)
        {
            personToCreate.ErrorMessage = ex.Message;
            Console.WriteLine(ex.Message);
        }
        return View(personToCreate);
    }

    public async Task<IActionResult> EditPerson(int id)
    {
        var person = await _dbcontext.People.FindAsync(id);
        if (person is null) throw new InvalidOperationException("Person does not exists");

        var personUpdateDto = new PersonUpdateDto
        {
            PersonId = person.PersonId,
            FirstName = person.FirstName,
            LastName = person.LastName,
            ProfilePicture = person.ProfilePicture
        };
        return View(personUpdateDto);
    }

    [HttpPost]
    public async Task<IActionResult> EditPerson(PersonUpdateDto personToUpdate)
    {
        if (!ModelState.IsValid)
        {
            return View(personToUpdate);
        }
        try
        {
            if (personToUpdate.File != null)
            {

                if (personToUpdate.File.Length > 300 * 1024)
                {
                    personToUpdate.ErrorMessage = "File can not exceed 300kb length";
                    return View(personToUpdate);
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                var fileExtension = Path.GetExtension(personToUpdate.File.FileName)?.ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    personToUpdate.ErrorMessage = "You can only upload .jpg, .jpeg, .png files";
                    return View(personToUpdate);
                }
            }

            // TODO: refactor this logic
            string profilePicUrl = personToUpdate.ProfilePicture ?? Guid.NewGuid().ToString();
            var person = new Person
            {
                PersonId = personToUpdate.PersonId,
                FirstName = personToUpdate.FirstName,
                LastName = personToUpdate.LastName,
                ProfilePicture = profilePicUrl
            };
            _dbcontext.Update(person);
            await _dbcontext.SaveChangesAsync();
            personToUpdate.SuccessMessage = "Saved successfully";
        }
        catch (Exception ex)
        {
            personToUpdate.ErrorMessage = ex.Message;
            Console.WriteLine(ex.Message);
        }
        return View(personToUpdate);
    }

    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            await _dbcontext.People.Where(a => a.PersonId == id).ExecuteDeleteAsync();
        }
        catch (Exception ex)
        {
        }

        return RedirectToAction(nameof(Index));
    }
}
