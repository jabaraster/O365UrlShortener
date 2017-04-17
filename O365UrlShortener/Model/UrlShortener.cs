using System;
using System.Threading.Tasks;
using System.Net.Http;
using Foundation;
using System.Text;
using System.Json;

namespace O365UrlShortener.Model
{
	public static class UrlShortener
	{
		public static async Task<ShortenResult> Shorten(string longUrl, string googleApiKey)
		{
			var url = "https://www.googleapis.com/urlshortener/v1/url?key=" + googleApiKey;
			var content = new StringContent("{\"longUrl\": \"" + longUrl + "\"}", Encoding.UTF8, "application/json");
			var res = await new HttpClient().PostAsync(url, content);
			var json = await res.Content.ReadAsStringAsync();
			var o = JsonValue.Parse(json);
			if (res.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new ApplicationException(o["error"]["errors"][0]["reason"]);
			}

			return new ShortenResult
			{
				kind = o["kind"],
				id = o["id"],
				longUrl = o["longUrl"],
			};
		}
	}

	public class ShortenResult
	{
		public string kind { get; set; }
		public string id { get; set; }
		public string longUrl { get; set; }

		public override string ToString()
		{
			return string.Format("[ShortenResult: kind={0}, id={1}, longUrl={2}]", kind, id, longUrl);
		}
	}
}
