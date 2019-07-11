namespace InstaPicture.Models
{
	public class CurrentInstaUser
	{
		public string FullName { get; set; }

		public string Biography { get; set; }

		public string ExternalUrl { get; set; }

		public string UserName { get; set; }

		public long? FollowersCount { get; set; }

		public long? FollowingCount { get; set; }

		public long? MediaCount { get; set; }

		public bool? IsPrivate { get; set; }

		public bool? IsVerified { get; set; }

		public string ProfilePicUrl { get; set; }
	}
}
