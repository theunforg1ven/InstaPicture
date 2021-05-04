using InstagramApiSharp.Classes.Models;
using InstaPicture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaPicture.Interfaces
{
	public interface IMediaService
	{
		Task<IEnumerable<string>> GetUserPictureAsync(string pictureLink);

		Task<IEnumerable<CurrentInstaStory>> GetCurrentUserStoriesAsync(string username);

		Task<IEnumerable<SavedInstaStory>> GetSavedUserStoriesAsync(string username);

		Task<IEnumerable<InstaMedia>> GetUserPics(string username);
	}
}
