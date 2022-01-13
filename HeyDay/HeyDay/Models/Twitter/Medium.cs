using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class Medium
	{
		[JsonPropertyName("media_key")]
		public string MediaKey { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}