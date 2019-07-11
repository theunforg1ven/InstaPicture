using InstaPicture.Controllers;
using InstaPicture.Interfaces;
using InstaPicture.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace InstaPicture.Tests
{
	public class MediaControllerTest
	{
		[Fact]
		public void GetUserPictureTest()
		{
			// Arrange
			var mock = new Mock<IMediaService>();
			mock.Setup(mq => mq.GetUserPictureAsync("dfgdfg")).ReturnsAsync(new List<string> {"value1", "value2" });
			var controller = new MediaController(mock.Object);

			// Act
			var result = controller.ModelState.ValidationState.ToString();

			// Assert
			Assert.Equal("Valid", result);
		}

		[Fact]
		public void GetSavedUserStoriesTest()
		{
			// Arrange
			var mock = new Mock<IMediaService>();
			mock.Setup(mq => mq.GetSavedUserStoriesAsync("dfgdfg")).ReturnsAsync(new List<SavedInstaStory> { new SavedInstaStory { UnifyStoryName = "valueTest" } });
			var controller = new MediaController(mock.Object);

			// Act
			var result = controller.ModelState.ValidationState.ToString();

			// Assert
			Assert.Equal("Valid", result);
		}

		[Fact]
		public void GetCurrentUserStoriesTest()
		{
			// Arrange
			var mock = new Mock<IMediaService>();
			mock.Setup(mq => mq.GetCurrentUserStoriesAsync("dfgdfg")).ReturnsAsync(new List<CurrentInstaStory> { new CurrentInstaStory {  Uri = "valueTest" } });
			var controller = new MediaController(mock.Object);

			// Act
			var result = controller.ModelState.ValidationState.ToString();

			// Assert
			Assert.Equal("Valid", result);
		}
	}
}
