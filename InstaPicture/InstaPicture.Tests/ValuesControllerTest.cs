using InstaPicture.Controllers;
using Xunit;

namespace InstaPicture.Tests
{
	public class ValuesControllerTest
	{
		[Fact]
		public void Get_WhenCalled_ReturnsOkResult()
		{
			// Arrange
			var _controller = new ValuesController();

			// Act
			var okResult = _controller.Get();

			// Assert
			Assert.Equal("All is ok!", okResult.Value);
		}
	}
}
