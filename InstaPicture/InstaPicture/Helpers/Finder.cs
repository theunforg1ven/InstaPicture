using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using System.Threading.Tasks;

namespace InstaPicture.Helpers
{
	public static class Finder
	{
		private static UserSessionData _user;

		private static IInstaApi _api;

		public static IInstaApi InstaApi { get; set; }

		public static async Task<IInstaApi> GetInstaClientAsync()
		{
			_user = new UserSessionData
			{
				UserName = FinderSettings.Username,
				Password = FinderSettings.Password
			};

			_api = InstaApiBuilder.CreateBuilder()
				.SetUser(_user)
				.UseLogger(new DebugLogger(LogLevel.Exceptions))
				.SetRequestDelay(RequestDelay.FromSeconds(1, 2))
				.Build();

			var loginRequest = await _api.LoginAsync();

			return _api;
		}
	}
}
