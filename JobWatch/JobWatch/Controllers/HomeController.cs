using System.Diagnostics;
using JobWatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var buildSha = Environment.GetEnvironmentVariable("BUILD_SHA") ?? "local";
            var hostName = Environment.MachineName;
            ViewData["BuildSha"] = buildSha;
            ViewData["HostName"] = hostName;

            if (buildSha == "local")
                _logger.LogWarning("BUILD_SHA environment variable is not set; falling back to {Fallback}", buildSha);

            _logger.LogDebug("Resolved hostName={HostName} buildSha={BuildSha}", hostName, buildSha);
            _logger.LogInformation("Home page rendered for {HostName} build {BuildSha}", hostName, buildSha);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
