using CargoApp.Services;
using Microsoft.Extensions.Localization;
using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CargoApp.Data;
using CargoApp.Models;
using CargoApp.Resources;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CargoAppTests
{
    [TestFixture]
    public class ReviewsServiceTests
    {
        private Mock<IHttpContextAccessor> _contextAccessorMock;
        private Mock<UserManager<User>> _userManagerMock;
        private CargoAppContext _context;
        private IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;
        private ReviewsService _reviewsService;
        private User user1;
        private User user2;

        [SetUp]
        public void SetUp()
        {
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            var options = new DbContextOptionsBuilder<CargoAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new CargoAppContext(options);
            _context.Database.EnsureCreated();

            user1 = new User { Id = "user1", Name = "user1" };
            user2 = new User { Id = "user2", Name = "user2" };

            _context.AddRange(user1, user2);
            _context.SaveChanges();

            _stringLocalizer = Mock.Of<IStringLocalizer<AnnotationsSharedResource>>();

            _reviewsService = new ReviewsService(
                _contextAccessorMock.Object,
                _userManagerMock.Object,
                _context,
                _stringLocalizer
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SetUserAuthentication(bool isAuthenticated)
        {
            var identityMock = new Mock<IIdentity>();
            identityMock.Setup(i => i.IsAuthenticated).Returns(isAuthenticated);

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(u => u.Identity).Returns(identityMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.User).Returns(userMock.Object);

            _contextAccessorMock.Setup(ca => ca.HttpContext).Returns(httpContextMock.Object);
        }

        [Test]
        public async Task CanCreateReviewAsync_ShouldReturnTrue_WhenNoReviewExists()
        {
            // Arrange
            var userId = user1.Id;
            var receiverId = user2.Id;

            // Act
            var result = await _reviewsService.CanCreateReviewAsync(userId, receiverId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CanCreateReviewAsync_ShouldReturnFalse_WhenReviewExists()
        {
            // Arrange
            var senderId = user1.Id;
            var receiverId = user2.Id;
            _context.Reviews.Add(new Review { SenderId = senderId, ReceiverId = receiverId });
            await _context.SaveChangesAsync();

            // Act
            var result = await _reviewsService.CanCreateReviewAsync(senderId, receiverId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task NoTrackingReceivedReviewsAsync_ShouldReturnReviews()
        {
            // Arrange
            _context.Reviews.Add(new Review { ReceiverId = user1.Id, SenderId = user2.Id });
            await _context.SaveChangesAsync();

            // Act
            var result = await _reviewsService.NoTrackingReceivedReviewsAsync(user1.Id);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().ReceiverId, Is.EqualTo(user1.Id));
        }

        [Test]
        public async Task NoTrackingSentReviewsAsync_ShouldReturnReviews()
        {
            // Arrange
            _context.Reviews.Add(new Review { ReceiverId = user1.Id, SenderId = user2.Id });
            await _context.SaveChangesAsync();

            // Act
            var result = await _reviewsService.NoTrackingSentReviewsAsync(user2.Id);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().SenderId, Is.EqualTo(user2.Id));
        }

        [Test]
        public async Task CreateReviewAsync_ShouldReturnFalse_WhenUserIdNotFound()
        {
            // Arrange
            var review = new Review { ReceiverId = user1.Id, SenderId = user2.Id };
            SetUserAuthentication(false);

            // Act
            var result = await _reviewsService.CreateReviewAsync(review);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CreateReviewAsync_ShouldReturnFalse_WhenReviewExists()
        {
            // Arrange
            var review = new Review { ReceiverId = user1.Id, SenderId = user2.Id };
            SetUserAuthentication(true);
            _context.Reviews.Add(new Review { ReceiverId = user1.Id, SenderId = user2.Id });
            await _context.SaveChangesAsync();

            // Act
            var result = await _reviewsService.CreateReviewAsync(review);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CreateReviewAsync_ShouldReturnTrue_WhenReviewCreated()
        {
            // Arrange
            var review = new Review { ReceiverId = user1.Id, SenderId = user2.Id };
            SetUserAuthentication(true);
            _userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(user2.Id);

            // Act
            var result = await _reviewsService.CreateReviewAsync(review);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldDeleteReview_WhenReviewExists()
        {
            // Arrange
            var senderId = user1.Id;
            var receiverId = user2.Id;
            var review = new Review { SenderId = senderId, ReceiverId = receiverId };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Act
            await _reviewsService.DeleteReviewAsync(senderId, receiverId);

            // Assert
            var deletedReview = await _context.Reviews.FindAsync(senderId, receiverId);
            Assert.That(deletedReview, Is.Null);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldNotDeleteReview_WhenReviewNotExists()
        {
            // Arrange
            var senderId = user1.Id;
            var receiverId = user2.Id;

            // Act
            await _reviewsService.DeleteReviewAsync(senderId, receiverId);

            // Assert
            var deletedReview = await _context.Reviews.FindAsync(senderId, receiverId);
            Assert.That(deletedReview, Is.Null);
        }
    }
}
