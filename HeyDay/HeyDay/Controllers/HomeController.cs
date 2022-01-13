using System;
using HeyDay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HeyDay.Models.Twitter;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;

namespace HeyDay.Controllers
{
	public class HomeController : Controller
	{
		public TwitterService TwitterSearchService { get; }
		private readonly IMemoryCache _memoryCache;

		public HomeController(TwitterService twitterSearchService, IMemoryCache memoryCache)
		{
			TwitterSearchService = twitterSearchService;
			_memoryCache = memoryCache;
		}
		public async Task<IActionResult> Media(string media)
		{
			media ??= HttpContext.Request.Path.Value?.Split("/").Last();
			if (!_memoryCache.TryGetValue(media, out byte[] image))
			{
				var httpClient = new HttpClient();
				httpClient.BaseAddress = new Uri("https://pbs.twimg.com");
				image = await httpClient.GetByteArrayAsync($"/media/{media}");
				_memoryCache.Set(media, image);
			}
			return File(image, "image/jpeg");
		}
		public async Task<TwitterResponseObject> Search(string query)
		{
			if (_memoryCache.TryGetValue($"search:{query}", out TwitterResponseObject cachedResult))
				return cachedResult;

			var result = await TwitterSearchService.SearchRecent(query);
			if (result != null)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600));
				_memoryCache.Set($"search:{query}", result, cacheEntryOptions);
			}
			return result;
		}
		public async Task<TwitterImageResponseObject> SearchImages(string query)
		{
			if (_memoryCache.TryGetValue($"searchImages:{query}", out TwitterImageResponseObject cachedResult))
				return cachedResult;
			var result = await TwitterSearchService.SearchRecentImage(query);
			if (result != null)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600));
				_memoryCache.Set($"searchImages:{query}", result, cacheEntryOptions);
			}
			return result;

		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
