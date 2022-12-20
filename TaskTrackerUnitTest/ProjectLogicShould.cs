using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerData.Repositories;
using TaskTrackerLogic;
using Xunit;

namespace TaskTrackerUnitTest
{
    public class ProjectLogicShould
    {
        private readonly TaskTrackerDataContext _context;
        private readonly ProjectRepository _repository;
        private readonly ProjectLogic _logic;
        public ProjectLogicShould()
        {
            _context = GetContext();
            _repository = new ProjectRepository(_context);
            _logic = new ProjectLogic(_repository);
        }

        private TaskTrackerDataContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TaskTrackerDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new TaskTrackerDataContext(options);
            context.Database.EnsureCreated();
            
            context.Projects.AddRange(
                new Project
                {
                    Id = 1,
                    StartDate = new DateTime(2022, 01, 01),
                    EndDate = new DateTime(2022, 02, 02),
                    Name = "first",
                    Priority = 1,
                    ProjectStatus = ProjectStatus.NotStarted,
                    Tasks = new List<ProjectTask>()
                },
                new Project
                {
                    Id = 2,
                    StartDate = new DateTime(2022, 02, 02),
                    EndDate = new DateTime(2023, 03, 03),
                    Name = "second",
                    Priority = 2,
                    ProjectStatus = ProjectStatus.Completed,
                    Tasks = new List<ProjectTask>()
                });

            context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task ReturnAllProjects()
        {
            //Arrange
            var expected = new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    StartDate = new DateTime(2022, 01, 01),
                    EndDate = new DateTime(2022, 02, 02),
                    Name = "first",
                    Priority = 1,
                    ProjectStatus = ProjectStatus.NotStarted,
                    Tasks = new List<ProjectTask>()
                },
                new Project
                {
                    Id = 2,
                    StartDate = new DateTime(2022, 02, 02),
                    EndDate = new DateTime(2023, 03, 03),
                    Name = "second",
                    Priority = 2,
                    ProjectStatus = ProjectStatus.Completed,
                    Tasks = new List<ProjectTask>()
                }
            };

            //Act
            var actual = await _logic.GetAllProjects();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ReturnSingleProject()
        {
            //Arrange
            var projectId = 1;
            var expected = new Project
            {
                Id = projectId,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 02, 02),
                Name = "first",
                Priority = 1,
                ProjectStatus = ProjectStatus.NotStarted,
                Tasks = new List<ProjectTask>()
            };

            //Act
            var actual = await _logic.GetSingleProject(projectId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateReturnProject()
        {
            //Arrange
            var projectId = 1;
            var expected = new Project
            {
                Id = projectId,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 03, 03),
                Name = "new",
                Priority = 2,
                ProjectStatus = ProjectStatus.Active,
                Tasks = new List<ProjectTask>()
            };

            var request = new ProjectRequest
            {
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 03, 03),
                Name = "new",
                Priority = 2,
                ProjectStatus = ProjectStatus.Active,
            };

            //Act
            var actual = await _logic.UpdateProject(projectId, request);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateReturnProject()
        {
            //Arrange
            var expected = new Project
            {
                Id = 3,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 02, 02),
                Name = "first",
                Priority = 1,
                ProjectStatus = ProjectStatus.NotStarted,
                Tasks = null
            };

            var request = new ProjectRequest
            {
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 02, 02),
                Name = "first",
                Priority = 1,
                ProjectStatus = ProjectStatus.NotStarted,
            };

            //Act
            var actual = await _logic.CreateProject(request);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteReturnProject()
        {
            //Arrange
            var projectId = 1;
            var expected = new Project
            {
                Id = 1,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 02, 02),
                Name = "first",
                Priority = 1,
                ProjectStatus = ProjectStatus.NotStarted,
                Tasks = new List<ProjectTask>()
            };

            //Act
            var actual = await _logic.DeleteProject(projectId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task SearchReturnProject()
        {
            //Arrange
            var name = "first";
            var priority = 1;
            var projectStatus = ProjectStatus.NotStarted;
            var startDate = new DateTime(2022, 01, 01);
            var endDate = new DateTime(2022, 02, 02);

            var expected = new Project
            {
                Id = 1,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 02, 02),
                Name = "first",
                Priority = 1,
                ProjectStatus = ProjectStatus.NotStarted,
                Tasks = new List<ProjectTask>()
            };

            //Act
            var actual = await _logic.SearchProject(name, priority, projectStatus, startDate, endDate);

            //Assert
            actual.Should().ContainEquivalentOf(expected);
        }
    }
}