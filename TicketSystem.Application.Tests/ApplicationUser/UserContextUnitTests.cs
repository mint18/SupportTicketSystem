using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TicketSystem.Application.ApplicationUser;
using TicketSystem.Domain.Constants;
using Xunit;

namespace TicketSystem.Application.Tests.ApplicationUser;

public class UserContextUnitTests
{
    [Fact]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, Roles.Admin),
            new(ClaimTypes.Role, Roles.Manager)
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        var currentUser = userContext.GetCurrentUser();

        // assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(Roles.Admin, Roles.Manager);
    }

    [Fact]
    public void GetCurrentUser_WithNoHttpContext_ThrowsInvalidOperationException()
    {
        // arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }

    [Fact]
    public void GetCurrentUser_WithUnauthenticatedUser_ThrowsInvalidOperationException()
    {
        // arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>();
        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User is not authenticated");
    }

    [Fact]
    public void GetCurrentUser_WithMissingClaims_ThrowsInvalidOperationException()
    {
        // arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        // Brak wymaganych claimów (NameIdentifier, Email)
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Role, Roles.Admin)
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User identifier or email claim is missing");
    }
}