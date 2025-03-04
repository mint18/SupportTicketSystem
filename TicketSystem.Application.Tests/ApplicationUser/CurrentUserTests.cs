using Xunit;

namespace TicketSystem.Application.ApplicationUser.Tests;

using FluentAssertions;
using System.Collections.Generic;
using TicketSystem.Domain.Constants;

public class CurrentUserTests
{

    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        // arrange
        var currentUser = new CurrentUser("123", "test@example.com", [Roles.Admin, Roles.Manager, Roles.Customer]);
        // act
        var isInRole = currentUser.IsInRole("Admin");
        // assert
        isInRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNonMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("123", "test@example.com", [Roles.Manager, Roles.Customer]);
        // act
        var isInRole = currentUser.IsInRole("Admin");
        // assert
        isInRole.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_WithEmptyRoles_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("123", "test@example.com", []);
        // act
        var isInRole = currentUser.IsInRole("Admin");
        // assert
        isInRole.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_WithCaseSensitiveRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("123", "test@example.com", [Roles.Admin]);
        // act
        var isInRole = currentUser.IsInRole("admin");
        // assert
        isInRole.Should().BeFalse();
    }

}