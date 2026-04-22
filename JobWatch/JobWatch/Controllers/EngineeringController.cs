using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

[Authorize(Policy = "RequireEngineering")]
public class EngineeringController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
