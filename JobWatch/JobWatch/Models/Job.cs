using System.ComponentModel.DataAnnotations;

namespace JobWatch.Models;

public class Job
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public JobType Type { get; set; } = JobType.None;
}
