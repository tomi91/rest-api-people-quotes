using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApi.Api.Controllers;
using RestApi.Core.DTOs;
using RestApi.Core.DTOs.Common;
using RestApi.Core.Services.Person;
using Xunit;

#nullable disable

namespace RestApi.UnitTests
{
    public class PersonControllerTests
    {
        private readonly PeopleController _controller;
        private readonly Mock<IPersonService> _iPersonServiceMock;

        public PersonControllerTests()
        {
            _iPersonServiceMock = new Mock<IPersonService>() { DefaultValue = DefaultValue.Mock };

            _controller = new PeopleController(_iPersonServiceMock.Object);
        }

        [Fact]
        public async void GetById_WhenCalled_ReturnsOkResult()
        {
            _iPersonServiceMock.Setup(service => service.GetById(1)).Returns(Task.FromResult(new ApiResult<PersonDto>(new PersonDto())));

            var result = await _controller.GetById(1);
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Fact]
        public async void GetById_WhenCalled_ReturnsNotFound()
        {
            _iPersonServiceMock.Setup(service => service.GetById(int.MaxValue)).Returns(Task.FromResult(new ApiResult<PersonDto>(null)));

            var result = await _controller.GetById(int.MaxValue);
            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public async void Insert_WhenCalled_ReturnedBadRequest()
        {
            _iPersonServiceMock.Setup(service => service.Insert(new PersonDto() { Id = 1 })).Returns(Task.FromResult(new ApiResult<PersonDto>(false, "Err")));

            var result = await _controller.Insert(new PersonDto() { Id = 1 });
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }

    }
}