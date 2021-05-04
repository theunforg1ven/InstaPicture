using InstagramApiSharp.Classes.Models;
using InstaPicture.Interfaces;
using InstaPicture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaPicture.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MediaController : ControllerBase
	{
		private readonly IMediaService _mediaService;

		public MediaController(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		[HttpGet]
		[Route("UserPics")]
		public async Task<IEnumerable<InstaMedia>> GetUserPicsAsync(string username)
		{
			var picResult = await _mediaService.GetUserPics(username);

			return picResult;
		}

		[HttpGet]
		[Route("Pictures")]
		public async Task<IEnumerable<string>> GetPicturesAsync(string link)
		{
			var picResult = await _mediaService.GetUserPictureAsync(link);

			return picResult;
		}

		[HttpGet]
		[Route("Stories")]
		public async Task<IEnumerable<CurrentInstaStory>> GetCurrentStoriesAsync(string username)
		{
			var picResult = await _mediaService.GetCurrentUserStoriesAsync(username);

			return picResult;
		}

		[HttpGet]
		[Route("SavedStories")]
		public async Task<IEnumerable<SavedInstaStory>> GetSavedStoriesAsync(string username)
		{
			var picResult = await _mediaService.GetSavedUserStoriesAsync(username);

			return picResult;
		}
	}
}
