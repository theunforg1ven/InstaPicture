using InstaPicture.Controllers;
using InstaPicture.Interfaces;
using InstaPicture.Models;
using Moq;
using Xunit;

namespace InstaPicture.Tests
{
	public class UserControllerTest
	{
		[Fact]
		public void GetUserInfoTest()
		{
			// Arrange
			var mock = new Mock<IUserService>();
			mock.Setup(mq => mq.GetCurrentUserInfo("dfgdfg")).ReturnsAsync(new CurrentInstaUser { UserName = "valueUserTest" });
			var controller = new UserController(mock.Object);

			// Act
			var result = controller.ModelState.ValidationState.ToString();

			// Assert
			Assert.Equal("Valid", result);
		}
	}
}
