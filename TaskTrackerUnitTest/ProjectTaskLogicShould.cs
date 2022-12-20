using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskTracker.RequestModels;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerData.Repositories;
using TaskTrackerLogic;
using Xunit;

namespace TaskTrackerUnitTest
{
    public class ProjectTaskLogicShould
    {
        private readonly TaskTrackerDataContext _context;
        private readonly ProjectTaskRepository _repository;
        private readonly ProjectTaskLogic _logic;

        public ProjectTaskLogicShould()
        {
            _context = GetContext();
            _repository = new ProjectTaskRepository(_context);
            _logic = new ProjectTaskLogic(_repository);
        }

        private TaskTrackerDataContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TaskTrackerDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new TaskTrackerDataContext(options);
            context.Database.EnsureCreated();

            context.Tasks.AddRange(
                new ProjectTask
                {
                    Id = 1,
                    Name = "first",
                    Description = "first description",
                    Priority = 1,
                    ProjectId = 1,
                    TaskStatus = ProjectTaskStatus.ToDO
                },
                new ProjectTask
                {
                    Id = 2,
                    Name = "second",
                    Description = "second description",
                    Priority = 2,
                    ProjectId = 1,
                    TaskStatus = ProjectTaskStatus.InProgress
                });

            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task ReturnSingleTask()
        {
            //Arrange
            var taskId = 1;
            var expected = new ProjectTask
            {
                Id = taskId,
                Name = "first",
                Description = "first description",
                Priority = 1,
                ProjectId = 1,
                TaskStatus = ProjectTaskStatus.ToDO
            };

            //Act
            var actual = await _logic.GetSingleProjectTask(taskId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ReturnAllTasks()
        {
            //Arrange
            var expected = new List<ProjectTask>
            {
                new ProjectTask
                {
                    Id = 1,
                    Name = "first",
                    Description = "first description",
                    Priority = 1,
                    ProjectId = 1,
                    TaskStatus = ProjectTaskStatus.ToDO
                },
                new ProjectTask
                {
                    Id = 2,
                    Name = "second",
                    Description = "second description",
                    Priority = 2,
                    ProjectId = 1,
                    TaskStatus = ProjectTaskStatus.InProgress
                }
            };

            //Act
            var actual = await _logic.GetAllProjectTasks();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateReturnTask()
        {
            //Arrange
            var expected = new ProjectTask
            {
                Id = 3,
                Name = "new",
                Description = "new description",
                Priority = 1,
                ProjectId = null,
                TaskStatus = ProjectTaskStatus.InProgress
            };
            var request = new ProjectTaskRequest
            {
                Name = "new",
                Description = "new description",
                Priority = 1,
                TaskStatus = ProjectTaskStatus.InProgress
            };

            //Act
            var actual = await _logic.CreateProjectTask(request);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateReturnTask()
        {
            //Arrange
            var taskId = 1;
            var expected = new ProjectTask
            {
                Id = taskId,
                Name = "new",
                Description = "new description",
                Priority = 2,
                ProjectId = 1,
                TaskStatus = ProjectTaskStatus.Done
            };
            var request = new ProjectTaskRequest
            {
                Name = "new",
                Description = "new description",
                Priority = 2,
                TaskStatus = ProjectTaskStatus.Done
            };

            //Act
            var actual = await _logic.UpdateProjectTask(taskId, request);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteReturnTask()
        {
            //Arrange
            var taskId = 1;
            var expected = new ProjectTask
            {
                Id = taskId,
                Name = "first",
                Description = "first description",
                Priority = 1,
                ProjectId = 1,
                TaskStatus = ProjectTaskStatus.ToDO
            };

            //Act
            var actual = await _logic.DeleteProjectTask(taskId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AddToProjectReturnTask()
        {
            //Arrange
            var projectId = 1;
            var expected = new ProjectTask
            {
                Id = 3,
                Name = "new",
                Description = "new description",
                Priority = 2,
                ProjectId = 1,
                TaskStatus = ProjectTaskStatus.ToDO
            };
            var request = new ProjectTaskRequest
            {
                Name = "new",
                Description = "new description",
                Priority = 2,
                TaskStatus = ProjectTaskStatus.ToDO
            };

            //Act
            var actual = await _logic.AddTaskToProject(projectId, request);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task RemoveFromProjectReturnTask()
        {
            //Arrange
            var taskId = 1;

            //Act
            var actual = await _logic.RemoveTaskFromProject(taskId);

            //Assert
            actual.ProjectId.Should().BeNull();
        }

        [Fact]
        public async Task SearchReturnTask()
        {
            //Arrange
            var name = "first";
            var description = "first description";
            var taskStatus = ProjectTaskStatus.ToDO;
            var startPriority = 0;
            var endPriority = 2;

            var expected = new ProjectTask
            {
                Id = 1,
                Name = "first",
                Description = "first description",
                Priority = 1,
                ProjectId = 1,
                TaskStatus = ProjectTaskStatus.ToDO
            };

            //Act
            var actual = await _logic.SearchTask(name, description, taskStatus, startPriority, endPriority);

            //Assert
            actual.Should().ContainEquivalentOf(expected);
        }
    }
}
