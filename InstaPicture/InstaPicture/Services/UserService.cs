using InstagramApiSharp.API;
using InstaPicture.Helpers;
using InstaPicture.Interfaces;
using InstaPicture.Models;
using System.Threading.Tasks;

namespace InstaPicture.Services
{
	public class UserService : IUserService
	{
		private readonly IInstaApi _api;

		public UserService()
		{
			_api = Finder.InstaApi;
		}

		public async Task<CurrentInstaUser> GetCurrentUserInfo(string username)
		{
			var user = await _api.UserProcessor.GetUserAsync(username);
			var userInfo = await _api.UserProcessor.GetUserInfoByIdAsync(user.Value.Pk);

			if (user.Succeeded)
			{
				var instaUser = new CurrentInstaUser
				{
					UserName = userInfo.Value.Username,
					Biography = userInfo.Value.Biography,
					ExternalUrl = userInfo.Value.ExternalUrl,
					FullName = userInfo.Value.FullName,
					FollowersCount = userInfo.Value.FollowerCount,
					FollowingCount = userInfo.Value.FollowingCount,
					MediaCount = userInfo.Value.MediaCount,
					IsPrivate = userInfo.Value.IsPrivate,
					IsVerified = userInfo.Value.IsVerified,
					ProfilePicUrl = userInfo.Value.ProfilePicUrl,
				}; 

				return instaUser;
			}
			else
			{
				var instaUser = new CurrentInstaUser
				{
					UserName = "No such user",
				};

				return instaUser;
			}
		}
	}
}
