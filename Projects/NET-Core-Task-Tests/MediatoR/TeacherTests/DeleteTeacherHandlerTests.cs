using AutoMapper;
using MediatR;
using Moq;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers.Delete;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task_Tests.MediatoR.TeacherTests
{
    public class DeleteTeacherHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<ILoggerService> _mockLogger;

        private readonly DeleteTeacherHandler _handler;
        private readonly Teacher _entity;
        private readonly Course _entityCourse;
        private readonly DeleteTeacherCommand _command;

        public DeleteTeacherHandlerTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockLogger = new Mock<ILoggerService>();
            _entityCourse = new Course { Id = 1, Title = "Title" };
            Teacher teacher = new Teacher
            {
                Id = 1,
                Age = 20,
                FirstName = "first",
                LastName = "last",
                Courses = new List<Course> { _entityCourse }
            };
            _entity = teacher;

            _command = new DeleteTeacherCommand(1);
            _handler = new DeleteTeacherHandler(_mockRepositoryWrapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenRepositoryReturnsEntity()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
            .GetFirstOrDefaultWithSpecAsync(It.IsAny<TeacherByIdSpec>())).ReturnsAsync(_entity);
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.Delete(_entity));
            _mockRepositoryWrapper.Setup(repo => repo.CoursesRepository.Delete(_entityCourse));
            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsSuccess),
                () => _mockRepositoryWrapper.Verify(repo => repo.CoursesRepository.Delete(_entityCourse)),
                () => Assert.Equal(Unit.Value, result.Value));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenMapperReturnsNull()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
            .GetFirstOrDefaultWithSpecAsync(It.IsAny<TeacherByIdSpec>()))
                .ReturnsAsync((Teacher)null!);
            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);
            string expected = "Teacher is not found";

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsFailed),
                () => Assert.Equal(expected, result.Errors.FirstOrDefault()?.Message));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenRepositorySaveChangesReturnsZero()
        {
            // Arrange

            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
             .GetFirstOrDefaultWithSpecAsync(It.IsAny<TeacherByIdSpec>())).ReturnsAsync(_entity);
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.Delete(_entity));
            _mockRepositoryWrapper.Setup(repo => repo.CoursesRepository.Delete(_entityCourse));
            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var expectedMessage = "Teacher is not deleted";

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.Multiple(
               () => Assert.True(result.IsFailed),
               () => Assert.Equal(expectedMessage, result.Errors.FirstOrDefault()?.Message));
        }
    }
}
