using JobWatch.Models;
using JobWatch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobWatch.Controllers;

public class SalesController : Controller
{
    private readonly JobService _jobService;

    public SalesController(JobService jobService)
    {
        _jobService = jobService;
    }

    public async Task<IActionResult> Index()
    {
        var jobs = await _jobService.GetAllJobsAsync(JobType.Sales);
        return View(jobs);
    }

    [Authorize(Roles = "Admin", Policy = "RequireSales")]
    public IActionResult AddSales()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin", Policy = "RequireSales")]
    public async Task<IActionResult> AddSales(Job job)
    {
        if (!ModelState.IsValid)
        {
            return View(job);
        }

        job.Type = JobType.Sales;
        await _jobService.CreateJobAsync(job);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin", Policy = "RequireSales")]
    public async Task<IActionResult> EditSales(int id)
    {
        var job = await _jobService.GetJobByIdAsync(id);
        if (job is null) return NotFound();
        return View(job);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin", Policy = "RequireSales")]
    public async Task<IActionResult> EditSales(Job job)
    {
        if (!ModelState.IsValid)
        {
            return View(job);
        }

        job.Type = JobType.Sales;
        await _jobService.UpdateJobAsync(job);
        return RedirectToAction(nameof(Index));
    }
}
