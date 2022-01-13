using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using HeyDay.Models.Twitter;

namespace HeyDay.Controllers
{
	public class TwitterService
	{
		private string APIKey => "QrRNvK9Uy3PWC3SGijWMMW3Xs";
		private string APISecret => "G042lKasAswoCX6Uzbwbmcht8mBCgnGvozYLXBKu9aRWshCYWk";
		private string APIToken { get; }
		private HttpClient HttpClient { get;  }

		public TwitterService(IHttpClientFactory httpClientFactory)
		{
			HttpClient = httpClientFactory.CreateClient("Twitter");
		}
		
		public async Task<TwitterResponseObject> SearchRecent(string query)
		{
			var encodedQuery = HttpUtility.HtmlEncode(query);
			var httpResponseMessage = await HttpClient.GetAsync($"{HttpClient.BaseAddress?.AbsoluteUri}tweets/search/recent?query={encodedQuery}");
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				var result = await httpResponseMessage.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<TwitterResponseObject>(result, new JsonSerializerOptions { IgnoreNullValues = true });
			}

			return null;
		}
		public async Task<TwitterImageResponseObject> SearchRecentImage(string query)
		{

			var encodedQuery = HttpUtility.HtmlEncode(query);
			var hasMedia = "%20has%3Amedia";
			if (!encodedQuery.Contains(hasMedia))
				encodedQuery += hasMedia;

			var httpResponseMessage = await HttpClient.GetAsync($"{HttpClient.BaseAddress?.AbsoluteUri}tweets/search/recent?query={encodedQuery}&tweet.fields=created_at&expansions=attachments.media_keys&media.fields=url");

			if (httpResponseMessage.IsSuccessStatusCode)
			{
				var result = await httpResponseMessage.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<TwitterImageResponseObject>(result,new JsonSerializerOptions{IgnoreNullValues = true});
			}
			return null;
		}
	}
}