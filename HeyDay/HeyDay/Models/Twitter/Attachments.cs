using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class Attachments
	{
		[JsonPropertyName("media_keys")]
		public List<string> MediaKeys { get; set; }
	}
}