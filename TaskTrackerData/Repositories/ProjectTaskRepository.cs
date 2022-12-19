using Microsoft.EntityFrameworkCore;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;

namespace TaskTrackerData.Repositories
{
    public class ProjectTaskRepository : IRepository<ProjectTask>
    {
        private readonly TaskTrackerDataContext _context;
        public ProjectTaskRepository(TaskTrackerDataContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<ProjectTask?> GetById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ProjectTask> Create(ProjectTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task Update(ProjectTask task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ProjectTask task)
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
