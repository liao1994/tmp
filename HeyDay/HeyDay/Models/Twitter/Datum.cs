using System;
using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class Datum
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }
	
		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonPropertyName("attachments")]
		public Attachments Attachments { get; set; }
	}
}