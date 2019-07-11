using System;
using System.Collections.Generic;

namespace InstaPicture.Models
{
	public class CurrentInstaStory
	{
		public string LinkText { get; set; }

		public int? CommentCount { get; set; }

		public DateTime? ExpiringAt { get; set; }

		public int? LikeCount { get; set; }

		public int? ViewerCount { get; set; }

		public List<string> Hashtags { get; set; }

		public List<string> Mentions { get; set; }

		public string Uri { get; set; }
	}
}
