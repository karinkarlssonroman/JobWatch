using JobWatch.Data;
using JobWatch.Models;

namespace JobWatch.Services;

public class JobService
{
    private readonly JobRepository _repository;

    public JobService(JobRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Job>> GetAllJobsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Job?> GetJobByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateJobAsync(Job job)
    {
        await _repository.AddAsync(job);
    }
}
