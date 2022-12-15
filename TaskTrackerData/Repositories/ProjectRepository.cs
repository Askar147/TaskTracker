using Microsoft.EntityFrameworkCore;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;   

namespace TaskTrackerData.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly TaskTrackerDataContext _context;
        public ProjectRepository(TaskTrackerDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Projects.Include(p => p.Tasks).ToListAsync();
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task Update(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Project project)
        {
            _context.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
