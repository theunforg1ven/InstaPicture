using InstaPicture.Models;
using System.Threading.Tasks;

namespace InstaPicture.Interfaces
{
	public interface IUserService
	{
		Task<CurrentInstaUser> GetCurrentUserInfo(string username);
	}
}
