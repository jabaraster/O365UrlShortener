using System;
using System.Threading.Tasks;
using System.Net.Http;
using Foundation;
using System.Text;
namespace O365UrlShortener.Model
{
	public static class HttpAccessor
	{
		public static async Task<T> Get<T>(string url)
		{
			var client = new HttpClient();
			var content = new StringContent("{\"longUrl\": \"http://www.google.com/\"}", Encoding.UTF8, "application/json");
			var res = await client.PostAsync(url, content);
			var json = await res.Content.ReadAsStringAsync();

			System.Diagnostics.Debug.WriteLine(json);

			throw new NotImplementedException();
		}
	}
}
