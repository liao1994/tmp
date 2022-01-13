using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeyDay.Models.Twitter
{
	public class Includes
	{
		[JsonPropertyName("media")]
		public List<Medium> Media { get; set; }
	}
}