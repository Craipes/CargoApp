using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using CargoApp.Services;
using CargoApp.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using CargoApp.Data;
using CargoApp.Resources;
using Moq;

namespace CargoAppTests
{
    [TestFixture]
    public class RequestsServiceTests
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<User> _userManager;
        private CargoAppContext _context;
        private IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;
        private RequestsService _requestsService;

        private readonly Car carTest = new Car()
        {
            Id = 1,
            AvailableGPS = true,
            MaxMass = 1,
            MaxHeight = 1,
            MaxLength = 1,
            MaxWidth = 1,
            MaxVolume = 1,
            Type = CarType.Any,
            TrailerType = TrailerType.Any
        };

        private readonly Cargo cargoTest = new Cargo()
        {
            Mass = 1,
            Height = 1,
            Length = 1,
            Width = 1,
            Volume = 1,
            TrailerType = TrailerType.Any,
            Description = string.Empty
        };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<CargoAppContext>()
                .UseInMemoryDatabase(databaseName: "CargoAppTestDb")
                .Options;

            _context = new CargoAppContext(options);

            _httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var userStore = new Mock<IUserStore<User>>().Object;
            _userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);

            _stringLocalizer = new Mock<IStringLocalizer<AnnotationsSharedResource>>().Object;

            _requestsService = new RequestsService(
                _httpContextAccessor,
                _userManager,
                _context,
                _stringLocalizer
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _userManager.Dispose();
        }

        [Test]
        public async Task CreateCarRequestAsync_ShouldReturnTrue_WhenUserIdIsValid()
        {
            // Arrange
            var user = new User { Id = "userId", Name = "Username" };
            await _userManager.CreateAsync(user);
            var carRequest = new CarRequest
            {
                Id = 1,
                UserId = user.Id,
                ContactName = "Contact Name",
                ContactPhoneNumber = "123456789",
                DeparturePlace = "Place A",
                DestinationPlace = "Place B",
                Cargo = cargoTest,
                Price = 100
            };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }));
            _httpContextAccessor.HttpContext.User = claimsPrincipal;

            // Act
            var result = await _requestsService.CreateCarRequestAsync(carRequest);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(carRequest.UserId, Is.EqualTo(user.Id));
        }

        [Test]
        public async Task CreateCarRequestAsync_ShouldReturnFalse_WhenUserIdIsInvalid()
        {
            // Arrange
            var carRequest = new CarRequest
            {
                Id = 1,
                UserId = "invalidUserId",
                ContactName = "Contact Name",
                ContactPhoneNumber = "123456789",
                DeparturePlace = "Place A",
                DestinationPlace = "Place B",
                Cargo = cargoTest,
                Price = 100
            };

            // Act
            var result = await _requestsService.CreateCarRequestAsync(carRequest);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
