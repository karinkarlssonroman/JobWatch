using JobWatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobWatch.Controllers;

[Authorize(Policy = "RequireSystemAdministrator")]
public class SystemAdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SystemAdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(string username, string password, string department)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(department))
        {
            ModelState.AddModelError(string.Empty, "All fields are required.");
            return View();
        }

        if (department != "Engineering" && department != "Sales")
        {
            ModelState.AddModelError(string.Empty, "Department must be Engineering or Sales.");
            return View();
        }

        var user = new ApplicationUser { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View();
        }

        await _userManager.AddToRoleAsync(user, "Admin");
        await _userManager.AddClaimAsync(user, new Claim("Department", department));

        TempData["Success"] = $"User '{username}' created with role Admin and Department={department}.";
        return RedirectToAction(nameof(CreateUser));
    }
}
