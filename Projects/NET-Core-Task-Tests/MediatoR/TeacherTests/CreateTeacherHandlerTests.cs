using AutoMapper;
using Moq;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Core_Task_Tests.MediatoR.TeacherTests
{
    public class CreateTeacherHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;

        private readonly CreateTeacherHandler _handler;
        private readonly Teacher _entity;
        private readonly TeacherDTO _dto;
        private readonly CreateTeacherCommand _command;

        public CreateTeacherHandlerTests()
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
            _dto = new TeacherDTO
            {
                Age = 20,
                FirstName = "first",
                LastName = "last"
            };

            _command = new CreateTeacherCommand(_dto);
            _handler = new CreateTeacherHandler(_mockRepositoryWrapper.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenMapperReturnsDTO()
        {
            // Arrange
            _mockMapper
                .Setup(maper => maper.Map<Teacher>(_dto))
                .Returns(_entity);
            _mockMapper
                .Setup(maper => maper.Map<TeacherDTO>(_entity))
                .Returns(_dto);
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.CreateAsync(_entity))
                .Callback<Teacher>(content =>
                {
                    content.Id = 1;
                }).ReturnsAsync(_entity);
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
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository.CreateAsync(_entity))
               .Callback<Teacher>(content => content.Id = 1).ReturnsAsync(_entity);
            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var expectedMessage = "Teacher is not save";

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.Multiple(
               () => Assert.True(result.IsFailed),
               () => Assert.Equal(expectedMessage, result.Errors.FirstOrDefault()?.Message));
        }
    }
}
