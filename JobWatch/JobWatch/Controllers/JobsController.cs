using JobWatch.Models;
using JobWatch.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

public class JobsController : Controller
{
    private readonly JobService _jobService;

    public JobsController(JobService jobService)
    {
        _jobService = jobService;
    }

    public async Task<IActionResult> Index()
    {
        var jobs = await _jobService.GetAllJobsAsync();
        return View(jobs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Job job)
    {
        if (!ModelState.IsValid)
        {
            return View(job);
        }

        await _jobService.CreateJobAsync(job);
        return RedirectToAction(nameof(Index));
    }
}
