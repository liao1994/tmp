﻿using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class Meta
	{
		[JsonPropertyName("newest_id")]
		public string NewestId { get; set; }

		[JsonPropertyName("oldest_id")]
		public string OldestId { get; set; }

		[JsonPropertyName("result_count")]
		public int ResultCount { get; set; }

		[JsonPropertyName("next_token")]
		public string NextToken { get; set; }
	}
}