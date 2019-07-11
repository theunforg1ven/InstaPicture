using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstaPicture.Helpers;
using InstaPicture.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstaPicture.Models;

namespace InstaPicture.Services
{
	public class MediaService : IMediaService
	{
		private readonly IInstaApi _api;

		public MediaService()
		{
			_api = Finder.InstaApi;
		}

		public async Task<IEnumerable<string>> GetUserPictureAsync(string pictureLink)
		{
			//var uri = new Uri($"{InstaPic}{pictureLink}/");

			var uri = new Uri(pictureLink);

			var imageList = new List<string>();

			var mediaId = await _api.MediaProcessor.GetMediaIdFromUrlAsync(uri);

			if (mediaId.Succeeded)
			{
				var photo = await _api.MediaProcessor.GetMediaByIdAsync(mediaId.Value);

				if (photo.Succeeded)
				{
					foreach (var pic in photo.Value.Images)
					{
						if (pic.Height > 500 && pic.Width > 500)
						{
							imageList.Add(pic.Uri);
						}
					}

					if (imageList.Count != 0)
						return imageList;
					else
					{
						var imgResult = GetUserCarousel(photo);

						return imgResult;
					}
				}
				else
				{
					imageList.Add("There are no link on media!");
					return imageList;
				}
			}
			else
			{
				imageList.Add("There are no media id!");
				return imageList;
			}
		}

		public async Task<IEnumerable<SavedInstaStory>> GetSavedUserStoriesAsync(string username)
		{
			var user = await _api.UserProcessor.GetUserAsync(username);

			var storiesUrls = new List<string>();

			var highlightIdList = new List<string>();

			var highlightList = new List<IResult<InstaHighlightSingleFeed>>();

			var savedInstaStories = new List<SavedInstaStory>();

			if (user.Succeeded)
			{
				var stories = await _api.StoryProcessor.GetHighlightFeedsAsync(user.Value.Pk);

				var res = stories.Value.Items;

				if (res.Count != 0)
				{
					foreach (var item in res)
					{
						highlightIdList.Add(item.HighlightId);
					}

					foreach (var highlight in highlightIdList)
					{
						var currentHighlight = await _api.StoryProcessor.GetHighlightMediasAsync(highlight);
						highlightList.Add(currentHighlight);
					}

					foreach (var highlight in highlightList)
					{
						var highlightTempList = new List<CurrentInstaStory>();

						foreach (var highlightItem in highlight.Value.Items)
						{
							if (highlightItem.ImageList[0].Width > 240 && highlightItem.ImageList[0].Height > 240 && highlightItem.VideoList.Count == 0)
							{
								var hashtagList = highlightItem.StoryHashtags
														.Select(ht => ht.Hashtag.Name)
														.ToList();

								var mentionsList = highlightItem.ReelMentions
														.Select(stuser => $"@{stuser.User.UserName}")
														.ToList();

								var instastory = new CurrentInstaStory()
								{
									CommentCount = (int)highlightItem.CommentCount,
									ExpiringAt = highlightItem.TakenAt,
									Hashtags = hashtagList,
									Mentions = mentionsList,
									LikeCount = (int)highlightItem.LikeCount,
									LinkText = highlightItem.LinkText,
									ViewerCount = (int)highlightItem.ViewerCount,
									Uri = highlightItem.ImageList[0].Uri,
								};

								highlightTempList.Add(instastory);
							}

							else if (highlightItem.VideoList.Count != 0 && highlightItem.VideoList[0].Height > 240 && highlightItem.VideoList[0].Height > 240)
							{
								storiesUrls.Add(highlightItem.VideoList[0].Uri);

								var hashtagList = highlightItem.StoryHashtags
														.Select(ht => ht.Hashtag.Name)
														.ToList();

								var mentionsList = highlightItem.ReelMentions
														.Select(stuser => $"@{stuser.User.UserName}")
														.ToList();

								var instastory = new CurrentInstaStory()
								{
									CommentCount = (int)highlightItem.CommentCount,
									ExpiringAt = highlightItem.TakenAt,
									Hashtags = hashtagList,
									Mentions = mentionsList,
									LikeCount = (int)highlightItem.LikeCount,
									LinkText = highlightItem.LinkText,
									ViewerCount = (int)highlightItem.ViewerCount,
									Uri = highlightItem.VideoList[0].Uri,
								};

								highlightTempList.Add(instastory);
							}
						}

						savedInstaStories.Add(new SavedInstaStory
						{
							 UnifyStoryName = highlight.Value.Title,
							 Stories = highlightTempList
						});
					}

					return savedInstaStories;
				}
				else
				{
					savedInstaStories.Add(new SavedInstaStory
					{
						UnifyStoryName = "Error",
						Stories = new List<CurrentInstaStory>() { new CurrentInstaStory() {  Uri = "There are no stories" } }
					});
					return savedInstaStories;
				}
			}
			else
			{
				savedInstaStories.Add(new SavedInstaStory
				{
					UnifyStoryName = "Error",
					Stories = new List<CurrentInstaStory>() { new CurrentInstaStory() { Uri = "There are no stories" } }
				});
				return savedInstaStories;
			}

		}

		public async Task<IEnumerable<CurrentInstaStory>> GetCurrentUserStoriesAsync(string username)
		{
			var user = await _api.UserProcessor.GetUserAsync(username);

			var storiesList = new List<InstaImage>();

			var storiesUrls = new List<string>();

			var userStories = new List<CurrentInstaStory>();

			if (user.Succeeded)
			{
				var stories =  await _api.StoryProcessor.GetUserStoryFeedAsync(user.Value.Pk);

				var res = stories.Value.Items;

				if (res.Count != 0)
				{				
					foreach (var storyurl in res)
					{
						if (storyurl.ImageList[0].Height > 240 && storyurl.ImageList[0].Width > 240 && storyurl.VideoList.Count == 0)
						{
							storiesUrls.Add(storyurl.ImageList[0].Uri);

							var hashtagList = storyurl.StoryHashtags
													.Select(ht => ht.Hashtag.Name)
													.ToList();

							var mentionsList = storyurl.ReelMentions
													.Select(stuser => $"@{stuser.User.UserName}" )
													.ToList();

							var instastory = new CurrentInstaStory()
							{
								CommentCount = (int)storyurl.CommentCount,
								ExpiringAt = storyurl.ExpiringAt,
								Hashtags = hashtagList,
								Mentions = mentionsList,
								LikeCount = (int)storyurl.LikeCount,
								LinkText = storyurl.LinkText,
							    ViewerCount = (int)storyurl.ViewerCount,
								Uri = storyurl.ImageList[0].Uri,
							};

							userStories.Add(instastory);
						}
						else if (storyurl.VideoList.Count != 0 && storyurl.VideoList[0].Height > 240 && storyurl.VideoList[0].Height > 240)
						{
							storiesUrls.Add(storyurl.VideoList[0].Uri);

							var hashtagList = storyurl.StoryHashtags
													.Select(ht => ht.Hashtag.Name)
													.ToList();

							var mentionsList = storyurl.ReelMentions
													.Select(stuser => $"@{stuser.User.UserName}")
													.ToList();

							var instastory = new CurrentInstaStory()
							{
								CommentCount = (int)storyurl.CommentCount,
								ExpiringAt = storyurl.ExpiringAt,
								Hashtags = hashtagList,
								Mentions = mentionsList,
								LikeCount = (int)storyurl.LikeCount,
								LinkText = storyurl.LinkText,
								ViewerCount = (int)storyurl.ViewerCount,
								Uri = storyurl.VideoList[0].Uri,
							};

							userStories.Add(instastory);
						}
					}
						
					return userStories;
				}
				else
				{
					userStories.Add(new CurrentInstaStory { Uri = "There are no stories" });
					return userStories;
				}
			}
			else
			{
				userStories.Add(new CurrentInstaStory { Uri = "There are no such user" });
				return userStories;
			}
				
		}

		private IEnumerable<string> GetUserCarousel(IResult<InstaMedia> picture)
		{
			var images = picture.Value.Carousel;

			var imageList = images.Select(img => img.Images[0].Uri);

			return imageList;
		}
	}
}
