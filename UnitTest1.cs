using LMSproj.Controllers;
using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LMSproj.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private UserController _controller;
        private Mock<LibraryContext> _mockContext;
        private Mock<IConfiguration> _mockConfiguration;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<LibraryContext>(options);
            _mockConfiguration = new Mock<IConfiguration>();

            _controller = new UserController(options, _mockConfiguration.Object);
        }

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
            var actionResult = NUnit.Framework.Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var returnValue = NUnit.Framework.Assert.IsInstanceOf<UserDto>(actionResult.Value);
            NUnit.Framework.Assert.AreEqual(registerDto.FullName, returnValue.FullName);
            NUnit.Framework.Assert.AreEqual(registerDto.Email, returnValue.Email);
            NUnit.Framework.Assert.AreEqual(registerDto.Role, returnValue.Role);
        }

        [Test]
        public async Task Login_ValidUser_ReturnsToken()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var user = new User
            {
                UserId = 1,
                FullName = "Test User",
                Email = loginDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(loginDto.Password),
                Role = "User"
            };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var actionResult = NUnit.Framework.Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var returnValue = NUnit.Framework.Assert.IsInstanceOf<Dictionary<string, string>>(actionResult.Value);
            NUnit.Framework.Assert.IsTrue(returnValue.ContainsKey("token"));
        }

        [Test]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                UserId = userId,
                FullName = "Test User",
                Email = "test@example.com",
                Role = "User"
            };

            _mockContext.Setup(c => c.Users.FindAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var actionResult = NUnit.Framework.Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var returnValue = NUnit.Framework.Assert.IsInstanceOf<UserDto>(actionResult.Value);
            NUnit.Framework.Assert.AreEqual(user.FullName, returnValue.FullName);
            NUnit.Framework.Assert.AreEqual(user.Email, returnValue.Email);
            NUnit.Framework.Assert.AreEqual(user.Role, returnValue.Role);
        }
    }
}
