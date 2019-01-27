using RestSharp;
using System.Threading.Tasks;
using TakeCare.Droid.Models;

namespace TakeCare.Droid
{
	public class HttpServerConnection
	{

		public async Task PostRequest(Event _event)
		{
			var client = new RestClient("");
			var request = new RestRequest("/EventNotification", Method.POST, DataFormat.Json);
			request.AddBody(_event);
			var response = await client.ExecuteTaskAsync(request);
		}
	}
}
