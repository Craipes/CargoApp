using CargoApp.Controllers;
using CargoApp.Models;
using CargoApp.Services;
using CargoApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoAppTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<SignInManager<User>> _signInManagerMock;
        private Mock<IRequestsService> _requestsServiceMock;
        private Mock<IReviewsService> _reviewsServiceMock;
        private AccountController _controller;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);
            _signInManagerMock = new Mock<SignInManager<User>>(
                _userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);
            _requestsServiceMock = new Mock<IRequestsService>();
            _reviewsServiceMock = new Mock<IReviewsService>();

            _controller = new AccountController(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _requestsServiceMock.Object,
                _reviewsServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task CreateReview_Get_ShouldReturnView_WhenUserCanCreateReview()
        {
            // Arrange
            var userId = "user1";
            var receiverId = "user2";
            var receiver = new User { Id = receiverId, Name = "Receiver" };

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
            _userManagerMock.Setup(um => um.FindByIdAsync(receiverId)).ReturnsAsync(receiver);
            _reviewsServiceMock.Setup(rs => rs.CanCreateReviewAsync(userId, receiverId)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateReview(receiverId, null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.TypeOf<CreateUpdateReviewViewModel>());
        }

        [Test]
        public async Task CreateReview_Get_ShouldRedirectToProfile_WhenUserCannotCreateReview()
        {
            // Arrange
            var userId = "user1";
            var receiverId = "user2";
            var receiver = new User { Id = receiverId, Name = "Receiver" };

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
            _userManagerMock.Setup(um => um.FindByIdAsync(receiverId)).ReturnsAsync(receiver);
            _reviewsServiceMock.Setup(rs => rs.CanCreateReviewAsync(userId, receiverId)).ReturnsAsync(false);

            // Act
            var result = await _controller.CreateReview(receiverId, null);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult?.ActionName, Is.EqualTo("Profile"));
        }

        [Test]
        public async Task CreateReview_Post_ShouldRedirectToProfile_WhenReviewIsCreated()
        {
            // Arrange
            var userId = "user1";
            var receiverId = "user2";
            var review = new Review { ReceiverId = receiverId, SenderId = userId };
            var viewModel = new CreateUpdateReviewViewModel(review, receiverId, "Receiver");

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
            _reviewsServiceMock.Setup(rs => rs.CreateReviewAsync(review)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateReview(viewModel);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult?.ActionName, Is.EqualTo("Profile"));
        }

        [Test]
        public async Task CreateReview_Post_ShouldReturnView_WhenModelStateIsInvalid()
        {
            // Arrange
            var userId = "user1";
            var receiverId = "user2";
            var review = new Review { ReceiverId = receiverId, SenderId = userId };
            var viewModel = new CreateUpdateReviewViewModel(review, receiverId, "Receiver");

            _controller.ModelState.AddModelError("Error", "Model state is invalid");

            // Act
            var result = await _controller.CreateReview(viewModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.EqualTo(viewModel));
        }

        [Test]
        public async Task DeleteReview_ShouldRedirectToReceivedReviews_WhenReviewIsDeleted()
        {
            // Arrange
            var userId = "user1";
            var senderId = "user1";
            var receiverId = "user2";

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
            _reviewsServiceMock.Setup(rs => rs.DeleteReviewAsync(senderId, receiverId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteReview(senderId, receiverId, null);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult?.ActionName, Is.EqualTo("ReceivedReviews"));
        }

        [Test]
        public async Task CreateReview_Get_ShouldReturnForbid_WhenUserIsNotAuthorized()
        {
            // Arrange
            var receiverId = "user2";
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns((string?)null);

            // Act
            var result = await _controller.CreateReview(receiverId, null);

            // Assert
            Assert.That(result, Is.TypeOf<ForbidResult>());
        }

        [Test]
        public async Task CreateReview_Post_ShouldReturnForbid_WhenUserIsNotAuthorized()
        {
            // Arrange
            var userId = "user1";
            var receiverId = "user2";
            var review = new Review { ReceiverId = receiverId, SenderId = userId };
            var viewModel = new CreateUpdateReviewViewModel(review, receiverId, "Receiver");

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns((string?)null);

            // Act
            var result = await _controller.CreateReview(viewModel);

            // Assert
            Assert.That(result, Is.TypeOf<ForbidResult>());
        }

        [Test]
        public async Task DeleteReview_ShouldReturnForbid_WhenUserIsNotAuthorized()
        {
            // Arrange
            var senderId = "user1";
            var receiverId = "user2";

            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns((string?)null);

            // Act
            var result = await _controller.DeleteReview(senderId, receiverId, null);

            // Assert
            Assert.That(result, Is.TypeOf<ForbidResult>());
        }
    }
}
