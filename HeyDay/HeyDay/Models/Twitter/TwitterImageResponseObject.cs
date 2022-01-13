using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class TwitterImageResponseObject : TwitterResponseObject
	{
		[JsonPropertyName("includes")]
		public Includes Includes { get; set; }
	}
}