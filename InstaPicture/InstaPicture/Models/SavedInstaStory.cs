using System.Collections.Generic;

namespace InstaPicture.Models
{
	public class SavedInstaStory
	{
		public string UnifyStoryName { get; set; }

		public List<CurrentInstaStory> Stories { get; set; }
	}
}
