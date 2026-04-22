using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

public class WhoAmIController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
