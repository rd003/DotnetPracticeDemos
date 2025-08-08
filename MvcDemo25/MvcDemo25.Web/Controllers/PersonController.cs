using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDemo25.Web.Models;
using System;

namespace MvcDemo25.Web.Controllers;

public class PersonController : Controller
{
    private readonly ILogger<PersonController> _logger;
    private readonly AppDbContext _ctx;

    public PersonController(ILogger<PersonController> logger, AppDbContext ctx)
    {
        _logger = logger;
        _ctx = ctx;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var people = await _ctx
                .People
                .AsNoTracking()
                .OrderBy(p=>p.FirstName)
                .ThenByDescending(p=>p.LastName)
                .ToListAsync();
            return View(people);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = "Error on saving Person record!";
            return View();
        }
        
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Person person)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            _ctx.People.Add(person);  // person object is added to context
            await _ctx.SaveChangesAsync(); // actual database call
            TempData["success"] = "Saved successfully!";
            return RedirectToAction(nameof(Add)); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = "Error on saving Person record!";
            return View();
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var person = await _ctx.People.AsNoTracking().SingleAsync(p=>p.Id==id);
            return View(person);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = "Something went wrong!";
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Person person)
    {
        if (!ModelState.IsValid)
        {
            return View(person);
        }

        try
        {
            if(!await _ctx.People.AnyAsync(p=>p.Id != person.Id))
            {
                TempData["error"] = "No record found";
                return View(person);
            }

            _ctx.People.Update(person);  // person object is added to context
            await _ctx.SaveChangesAsync(); // actual database call
            TempData["success"] = "Updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = "Error on saving Person record!";
            return View();
        }
    }


    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var person = await _ctx.People.AsNoTracking().SingleAsync(p => p.Id == id);
            _ctx.People.Remove(person);
            await _ctx.SaveChangesAsync();
            TempData["success"] = "Deleted successfull!";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = "Something went wrong!";
        }
        return RedirectToAction(nameof(Index));
    }
}
