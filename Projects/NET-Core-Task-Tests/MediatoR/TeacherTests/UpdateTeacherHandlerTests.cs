using AutoMapper;
using Moq;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers.Update;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task_Tests.MediatoR.TeacherTests
{
    public class UpdateTeacherHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;

        private readonly UpdateTeacherHandler _handler;
        private readonly Teacher _entity;
        private readonly TeacherUpdateDTO _dto;
        private readonly UpdateTeacherCommand _command;

        public UpdateTeacherHandlerTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerService>();
            _entity = new Teacher
            {
                Id = 1,
                Age = 20,
                FirstName = "first",
                LastName = "last"
            };
            _dto = new TeacherUpdateDTO
            {
                Id = 1,
                Age = 20,
                FirstName = "first",
                LastName = "last"
            };

            _command = new UpdateTeacherCommand(_dto);
            _handler = new UpdateTeacherHandler(_mockRepositoryWrapper.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenMapperReturnsDTO()
        {
            // Arrange
            _mockMapper
                .Setup(maper => maper.Map<Teacher>(_dto))
                .Returns(_entity);
            _mockMapper
                .Setup(maper => maper.Map<TeacherUpdateDTO>(_entity))
                .Returns(_dto);
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.Update(_entity));
            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsSuccess),
                () => Assert.Equal(_dto, result.Value));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenMapperReturnsNull()
        {
            // Arrange
            _mockMapper
                .Setup(maper => maper.Map<Teacher>(_dto))
                .Returns((Teacher)null!);

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
            _mockMapper
                .Setup(maper => maper.Map<Teacher>(_dto))
                .Returns(_entity);
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.Update(_entity));
            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var expectedMessage = "Teacher is not updated";

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.Multiple(
               () => Assert.True(result.IsFailed),
               () => Assert.Equal(expectedMessage, result.Errors.FirstOrDefault()?.Message));
        }
    }
}
