using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class TwitterResponseObject 
	{
		[JsonPropertyName("data")]
		public List<Datum> Data { get; set; }

		[JsonPropertyName("meta")]
		public Meta Meta { get; set; }
	}
}