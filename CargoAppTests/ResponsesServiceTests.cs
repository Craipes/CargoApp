using CargoApp.Services;
using CargoApp.Models;
using CargoApp.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CargoApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CargoAppTests;

[TestFixture]
class ResponsesServiceTests
{
    private Mock<IHttpContextAccessor> _contextAccessorMock;
    private Mock<UserManager<User>> _userManagerMock;
    private CargoAppContext _context;
    private IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;
    private ResponsesService _responsesService;
    private User user1;
    private User user2;

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
        _contextAccessorMock = new Mock<IHttpContextAccessor>();
        _userManagerMock = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
        var options = new DbContextOptionsBuilder<CargoAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        _context = new CargoAppContext(options);
        _context.Database.EnsureCreated();
        _stringLocalizer = new Mock<IStringLocalizer<AnnotationsSharedResource>>().Object;
        _responsesService = new ResponsesService(_contextAccessorMock.Object, _userManagerMock.Object, _context, _stringLocalizer);
        user1 = new User { Id = "1", UserName = "user1", Name = "User One" };
        user2 = new User { Id = "2", UserName = "user2", Name = "User Two" };
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    private void SetUserAuthentication(bool isAuthenticated, string? userId = null)
    {
        var claims = new List<Claim>();
        if (isAuthenticated && userId != null)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
        }
        var identity = new ClaimsIdentity(claims, isAuthenticated ? "TestAuthType" : null);
        var principal = new ClaimsPrincipal(identity);
        _contextAccessorMock.Setup(x => x.HttpContext!.User).Returns(principal);
    }

    [Test]
    public async Task CreateCarResponseAsync_ShouldReturnFalse_WhenUserNotAuthenticated()
    {
        SetUserAuthentication(false);

        var carResponse = new CarResponse
        {
            CarRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Car = carTest
        };

        var result = await _responsesService.CreateCarResponseAsync(carResponse);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CreateCarResponseAsync_ShouldReturnFalse_WhenRequestNotFound()
    {
        SetUserAuthentication(true, user1.Id);

        var carResponse = new CarResponse
        {
            CarRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Car = carTest
        };

        var result = await _responsesService.CreateCarResponseAsync(carResponse);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CreateCarResponseAsync_ShouldReturnFalse_WhenUserIsRequestOwner()
    {
        SetUserAuthentication(true, user1.Id);

        var carRequest = new CarRequest
        {
            Id = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            DeparturePlace = "Place A",
            DestinationPlace = "Place B",
            Cargo = cargoTest,
            Price = 100
        };
        _context.CarRequests.Add(carRequest);
        await _context.SaveChangesAsync();

        var carResponse = new CarResponse
        {
            CarRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Car = carTest,
            CarRequest = carRequest
        };

        var result = await _responsesService.CreateCarResponseAsync(carResponse);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CreateCargoResponseAsync_ShouldReturnFalse_WhenUserNotAuthenticated()
    {
        SetUserAuthentication(false);

        var cargoResponse = new CargoResponse
        {
            CargoRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Cargo = cargoTest,
        };

        var result = await _responsesService.CreateCargoResponseAsync(cargoResponse);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CreateCargoResponseAsync_ShouldReturnFalse_WhenRequestNotFound()
    {
        SetUserAuthentication(true, user1.Id);

        var cargoResponse = new CargoResponse
        {
            CargoRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Cargo = cargoTest,
        };

        var result = await _responsesService.CreateCargoResponseAsync(cargoResponse);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CreateCargoResponseAsync_ShouldReturnFalse_WhenUserIsRequestOwner()
    {
        SetUserAuthentication(true, user1.Id);

        var cargoRequest = new CargoRequest
        {
            Id = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            DeparturePlace = "Place A",
            DestinationPlace = "Place B",
            DepartureTime = DateTime.Now,
            Car = carTest,
            Price = 100
        };
        _context.CargoRequests.Add(cargoRequest);
        await _context.SaveChangesAsync();

        var cargoResponse = new CargoResponse
        {
            CargoRequestId = 1,
            UserId = user1.Id,
            ContactName = "Contact Name",
            ContactPhoneNumber = "123456789",
            Cargo = cargoTest,
            CargoRequest = cargoRequest
        };

        var result = await _responsesService.CreateCargoResponseAsync(cargoResponse);

        Assert.That(result, Is.False);
    }
}
