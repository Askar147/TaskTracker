using Microsoft.EntityFrameworkCore;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;
using TaskTrackerData.Repositories;
using TaskTrackerLogic;

namespace TaskTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TaskTrackerDataContext>(
                t => t.UseNpgsql(builder.Configuration.GetConnectionString("TaskTrackerDb"))
                );
            builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
            builder.Services.AddScoped<IRepository<ProjectTask>, ProjectTaskRepository>();
            builder.Services.AddScoped<IProjectLogic, ProjectLogic>();
            builder.Services.AddScoped<IProjectTaskLogic, ProjectTaskLogic>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}