using System;
using System.Diagnostics;
using System.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace O365UrlShortener.Model
{
	public static class UrlShortener
	{
		public static async Task<ShortenResult> Shorten(string longUrl, string googleApiKey)
		{
			Debug.WriteLine(longUrl);

			var targetUrl = extractUrl(longUrl);
			var url = "https://www.googleapis.com/urlshortener/v1/url?key=" + googleApiKey;
			var content = new StringContent("{\"longUrl\": \"" + targetUrl + "\"}", Encoding.UTF8, "application/json");
			var res = await new HttpClient().PostAsync(url, content);
			var json = await res.Content.ReadAsStringAsync();
			var o = JsonValue.Parse(json);
			if (res.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new ApplicationException(o["error"]["message"]);
			}

			return new ShortenResult
			{
				kind = o["kind"],
				id = o["id"],
				longUrl = o["longUrl"],
			};
		}

		/**
         * OneNoteでページのリンク先のURLをコピーすると２行の文字列がペーストボードに送られる.
         * これをそのままGoogle Shortener APIに送るとパースエラーとなる（当然）ので、１行のみ抽出する.
         * ２行目はonenote:で始まるカスタムプロトコルであり、これまたGoogle Shortener APIに送るとエラーになる.
         * よって１行目のhttpsで始まるURLを抽出する.
		 */
		static string extractUrl(string source)
		{
			foreach (var line in source.Split(new char[] { '\n', '\r' }))
			{
				if (line.StartsWith("http")) return line;
			}
			throw new ApplicationException("短縮対象のURLが不正(httpで始まる行がない) ---> " + source);
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
