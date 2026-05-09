using JobWatch.Models;
using JobWatch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

//[Authorize(Policy = "RequireEngineering")]
public class EngineeringController : Controller
{
    private readonly JobService _jobService;

    public EngineeringController(JobService jobService)
    {
        _jobService = jobService;
    }

    public async Task<IActionResult> Index()
    {
        var jobs = await _jobService.GetAllJobsAsync(JobType.Enginnering);
        
        return View(jobs);
    }

    [Authorize(Roles = "Admin", Policy = "RequireEngineering")]
    public IActionResult AddEngineering()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin", Policy = "RequireEngineering")]
    public async Task<IActionResult> AddEngineering(Job job)
    {
        if (!ModelState.IsValid)
        {
            return View(job);
        }

        job.Type = JobType.Enginnering;
        await _jobService.CreateJobAsync(job);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin", Policy = "RequireEngineering")]
    public async Task<IActionResult> EditEngineering(int id)
    {
        var job = await _jobService.GetJobByIdAsync(id);
        if (job is null) return NotFound();
        return View(job);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin", Policy = "RequireEngineering")]
    public async Task<IActionResult> EditEngineering(Job job)
    {
        if (!ModelState.IsValid)
        {
            return View(job);
        }

        job.Type = JobType.Enginnering;
        await _jobService.UpdateJobAsync(job);
        return RedirectToAction(nameof(Index));
    }
}
