using InstaPicture.Interfaces;
using InstaPicture.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InstaPicture.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("InstaUser")]
		public async Task<CurrentInstaUser> GetUserInfoAsync(string username)
		{
			var infoResult = await _userService.GetCurrentUserInfo(username);

			return infoResult;
		}
	}
}
