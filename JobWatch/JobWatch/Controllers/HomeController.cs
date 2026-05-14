using System.Diagnostics;
using JobWatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var buildSha = Environment.GetEnvironmentVariable("BUILD_SHA") ?? "local";
            var hostName = Environment.MachineName;
            ViewData["BuildSha"] = buildSha;
            ViewData["HostName"] = hostName;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
