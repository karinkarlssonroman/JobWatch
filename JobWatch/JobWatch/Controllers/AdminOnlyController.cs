using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

[Authorize(Roles = "Admin")]
public class AdminOnlyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
