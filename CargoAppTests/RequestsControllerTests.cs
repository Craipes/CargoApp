using CargoApp.Controllers;
using CargoApp.Models;
using CargoApp.Services;
using CargoApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CargoAppTests
{
    [TestFixture]
    public class RequestsControllerTests
    {
        private Mock<UserManager<User>> userManagerMock;
        private Mock<IRequestsService> requestsServiceMock;
        private Mock<IResponsesService> responsesServiceMock;
        private RequestsController controller;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<User>>();
            userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            requestsServiceMock = new Mock<IRequestsService>();
            responsesServiceMock = new Mock<IResponsesService>();
            controller = new RequestsController(userManagerMock.Object, requestsServiceMock.Object, responsesServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            controller.Dispose();
        }

        [Test]
        public async Task AllCarRequestsAdmin_ReturnsViewResult()
        {
            // Arrange
            requestsServiceMock.Setup(s => s.CarRequestsCountAsync(true, null)).ReturnsAsync(10);
            requestsServiceMock.Setup(s => s.PaginatedCarRequestsAsync(It.IsAny<int>(), true, null)).ReturnsAsync(new List<CarRequest>());

            // Act
            var result = await controller.AllCarRequestsAdmin();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.TypeOf<AllCarRequestsViewModel>());
            requestsServiceMock.Verify(s => s.CarRequestsCountAsync(true, null), Times.Once);
            requestsServiceMock.Verify(s => s.PaginatedCarRequestsAsync(It.IsAny<int>(), true, null), Times.Once);
        }

        [Test]
        public async Task AllCargoRequestsAdmin_ReturnsViewResult()
        {
            // Arrange
            requestsServiceMock.Setup(s => s.CargoRequestsCountAsync(true, null)).ReturnsAsync(10);
            requestsServiceMock.Setup(s => s.PaginatedCargoRequestsAsync(It.IsAny<int>(), true, null)).ReturnsAsync(new List<CargoRequest>());

            // Act
            var result = await controller.AllCargoRequestsAdmin();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.TypeOf<AllCargoRequestsViewModel>());
            requestsServiceMock.Verify(s => s.CargoRequestsCountAsync(true, null), Times.Once);
            requestsServiceMock.Verify(s => s.PaginatedCargoRequestsAsync(It.IsAny<int>(), true, null), Times.Once);
        }

        [Test]
        public async Task CarRequestDetails_ReturnsViewResult_WhenRequestExists()
        {
            // Arrange
            requestsServiceMock.Setup(s => s.NoTrackingCarDetailsAsync(It.IsAny<int>())).ReturnsAsync(new CarRequest
            {
                UserId = "test",
                ContactName = "test",
                ContactPhoneNumber = "test",
                DeparturePlace = "test",
                DestinationPlace = "test"
            });
            responsesServiceMock.Setup(s => s.NoTrackingCarFindAsync(It.IsAny<int>())).ReturnsAsync(new CarResponse
            {
                UserId = "test",
                ContactName = "test",
                ContactPhoneNumber = "test"
            });

            // Act
            var result = await controller.CarRequestDetails(1);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.TypeOf<CarRequestViewModel>());
            requestsServiceMock.Verify(s => s.NoTrackingCarDetailsAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task CargoRequestDetails_ReturnsViewResult_WhenRequestExists()
        {
            // Arrange
            requestsServiceMock.Setup(s => s.NoTrackingCargoDetailsAsync(It.IsAny<int>())).ReturnsAsync(new CargoRequest
            {
                UserId = "test",
                ContactName = "test",
                ContactPhoneNumber = "test",
                DeparturePlace = "test",
                DestinationPlace = "test",
                DepartureTime = DateTime.Now
            });
            responsesServiceMock.Setup(s => s.NoTrackingCargoFindAsync(It.IsAny<int>())).ReturnsAsync(new CargoResponse
            {
                UserId = "test",
                ContactName = "test",
                ContactPhoneNumber = "test"
            });

            // Act
            var result = await controller.CargoRequestDetails(1);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.TypeOf<CargoRequestViewModel>());
            requestsServiceMock.Verify(s => s.NoTrackingCargoDetailsAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
