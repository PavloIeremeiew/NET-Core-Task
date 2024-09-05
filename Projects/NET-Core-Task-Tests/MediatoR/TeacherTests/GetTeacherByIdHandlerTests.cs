using AutoMapper;
using Moq;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers.GetById;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Core_Task_Tests.MediatoR.TeacherTests
{
    public class GetTeacherByIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Teacher _entity;
        private readonly TeacherDTO _dto;

        private readonly GetTeacherByIdHandler _handler;
        private readonly GetTeacherByIdQuery _query;

        public GetTeacherByIdHandlerTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerService>();

            _handler = new GetTeacherByIdHandler(_mockRepositoryWrapper.Object, _mockMapper.Object, _mockLogger.Object);
            _query = new GetTeacherByIdQuery(1);

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

            _mockMapper.Setup(mapper => mapper.Map<TeacherDTO>(_entity))
                .Returns(_dto);
        }

        [Fact]
        public async Task Handle_Should_ReturnMappedTeacherDTO_WhenRepositoryReturnsData()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
                .GetFirstOrDefaultWithSpecAsync(It.IsAny<TeacherByIdSpec>()))
                .ReturnsAsync(_entity);

            // Act
            var result = await _handler.Handle(_query, CancellationToken.None);

            // Assert
            Assert.Multiple(
           () => Assert.True(result.IsSuccess),
           () => Assert.Equal(_dto, result.Value));
        }

        [Fact]
        public async Task Handle_Should_ReturnErrorMessage_WhenRepositoryReturnsNull()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
              .GetFirstOrDefaultWithSpecAsync(It.IsAny<TeacherByIdSpec>()))
                .ReturnsAsync((Teacher)null!);

            // Act
            var expectedErrorMessage = "Teacher is not found";
            var result = await _handler.Handle(_query, CancellationToken.None);

            // Assert
            Assert.Multiple(
           () => Assert.True(result.IsFailed),
           () => Assert.Equal(expectedErrorMessage, result.Errors.FirstOrDefault()?.Message));
        }
    }
}
