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
    public class GetAllTeachersHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly IEnumerable<Teacher> _list;
        private readonly IEnumerable<TeacherDTO> _mappedList;

        private readonly GetAllTeachersHandler _handler;
        private readonly GetAllTeachersQuery _query;

        public GetAllTeachersHandlerTests()
        {
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerService>();

            _handler = new GetAllTeachersHandler(_mockRepositoryWrapper.Object, _mockMapper.Object, _mockLogger.Object);
            _query = new GetAllTeachersQuery();
            _list = new List<Teacher> { new Teacher
            {
                Id = 1,
                Age = 50,
                FirstName = "TestName",
                LastName = "TestLastName"
            } };
            _mappedList = new List<TeacherDTO> { new TeacherDTO
            {
                Age = 50,
                FirstName = "TestName",
                LastName = "TestLastName"
            } };

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<TeacherDTO>>(_list))
                .Returns(_mappedList);
        }

        [Fact]
        public async Task Handle_Should_ReturnMappedTeacherDTO_WhenRepositoryReturnsData()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
                .GetAllAsync(default, default))
                .ReturnsAsync(_list);

            // Act
            var result = await _handler.Handle(_query, CancellationToken.None);

            // Assert
            Assert.Multiple(
           () => Assert.True(result.IsSuccess),
           () => Assert.Equal(_mappedList, result.Value));
        }

        [Fact]
        public async Task Handle_Should_ReturnErrorMessage_WhenRepositoryReturnsNull()
        {
            // Arrange
            _mockRepositoryWrapper.Setup(repo => repo.TeachersRepository
               .GetAllAsync(default, default))
                .ReturnsAsync((IEnumerable<Teacher>)null!);

            // Act
            var expectedErrorMessage = "Teachers are not found";
            var result = await _handler.Handle(_query, CancellationToken.None);

            // Assert
            Assert.Multiple(
           () => Assert.True(result.IsFailed),
           () => Assert.Equal(expectedErrorMessage, result.Errors.FirstOrDefault()?.Message));
        }
    }
}
