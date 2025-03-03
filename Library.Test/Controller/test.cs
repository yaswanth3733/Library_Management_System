using LMSproj.Controllers;
using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Test.Controller
{
    [TestFixture]
    public class UserControllerTests
    {
        private LibraryContext _context;
        private Mock<IConfiguration> _mockConfiguration;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "LibraryTest")
                .Options;

            _context = new LibraryContext(options);
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new UserController(_context, _mockConfiguration.Object);

            ClearDatabase();
            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        private void ClearDatabase()
        {
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        private void SeedDatabase()
        {
            var user = new User
            {
                UserId = 1,
                FullName = "Test User",
                Email = "testuser@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        //[Test]
        //public async Task Register_ValidUser_ReturnsCreatedAtAction()
        //{
        //    // Arrange
        //    var registerDto = new RegisterUserDto
        //    {
        //        FullName = "Test User",
        //        Email = "test@example.com",
        //        Password = "Password123",
        //        Role = "User"
        //    };

        //    // Act
        //    var result = await _controller.Register(registerDto);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        //    var actionResult = result.Result as CreatedAtActionResult;
        //    Assert.That(actionResult.Value, Is.InstanceOf<UserDto>());
        //    var returnValue = actionResult.Value as UserDto;
        //    Assert.That(returnValue.FullName, Is.EqualTo(registerDto.FullName));
        //    Assert.That(returnValue.Email, Is.EqualTo(registerDto.Email));
        //    Assert.That(returnValue.Role, Is.EqualTo(registerDto.Role));
        //}

        //[Test]
        //public async Task Login_ValidUser_ReturnsToken()
        //{
        //    // Arrange
        //    var loginDto = new LoginDto
        //    {
        //        Email = "testuser@example.com",
        //        Password = "Password123"
        //    };

        //    // Act
        //    var result = await _controller.Login(loginDto);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        //    var actionResult = result.Result as OkObjectResult;
        //    Assert.That(actionResult.Value, Is.InstanceOf<Dictionary<string, string>>());
        //    var returnValue = actionResult.Value as Dictionary<string, string>;
        //    Assert.That(returnValue.ContainsKey("token"));
        //}

        //[Test]
        //public async Task GetUserById_ValidId_ReturnsUser()
        //{
        //    // Arrange
        //    var userId = 1;

        //    // Act
        //    var result = await _controller.GetUserById(userId);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        //    var actionResult = result.Result as OkObjectResult;
        //    Assert.That(actionResult.Value, Is.InstanceOf<UserDto>());
        //    var returnValue = actionResult.Value as UserDto;
        //    Assert.That(returnValue.FullName, Is.EqualTo("Test User"));
        //    Assert.That(returnValue.Email, Is.EqualTo("testuser@example.com"));
        //    Assert.That(returnValue.Role, Is.EqualTo("User"));
        //}

        //[Test]
        //public async Task GetUserById_ReturnsNotFound_WhenUserNotFound()
        //{
        //    // Arrange
        //    var userId = 99; // User ID that does not exist

        //    // Act
        //    var result = await _controller.GetUserById(userId);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        //}

        //[Test]
        //public async Task Register_ReturnsBadRequest_WhenModelStateIsInvalid()
        //{
        //    // Arrange
        //    var registerDto = new RegisterUserDto
        //    {
        //        FullName = "Test User",
        //        Email = "invalid-email",
        //        Password = "123",
        //        Role = "User"
        //    };
        //    _controller.ModelState.AddModelError("Email", "Invalid email format");

        //    // Act
        //    var result = await _controller.Register(registerDto);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        //}

        //[Test]
        //public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials()
        //{
        //    // Arrange
        //    var loginDto = new LoginDto
        //    {
        //        Email = "testuser@example.com",
        //        Password = "WrongPassword"
        //    };

        //    // Act
        //    var result = await _controller.Login(loginDto);

        //    // Assert
        //    Assert.That(result.Result, Is.InstanceOf<UnauthorizedResult>());
        //}
        [Test]
        public async Task Register_ValidUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var registerDto = new RegisterUserDto
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "Password123",
                Role = "User"
            };

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var actionResult = result.Result as CreatedAtActionResult;
            var returnValue = actionResult.Value as UserDto;
            Assert.That(returnValue.FullName, Is.EqualTo(registerDto.FullName));
            Assert.That(returnValue.Email, Is.EqualTo(registerDto.Email));
            Assert.That(returnValue.Role, Is.EqualTo(registerDto.Role));
        }


    }
}
