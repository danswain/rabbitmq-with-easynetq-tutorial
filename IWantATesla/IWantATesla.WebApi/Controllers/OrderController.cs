using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyNetQ;
using IWantATesla.Messages;

namespace IWantATesla.WebApi.Controllers
{
	public class OrderController : ApiController
	{
		// POST api/values
		public HttpResponseMessage Post([FromBody] TeslaOrder order)
		{
			var messageBus = RabbitHutch.CreateBus("host=localhost");

			using(var publishChannel = messageBus.OpenPublishChannel())
			{
				publishChannel.Publish(order);
			}
			return Request.CreateResponse(HttpStatusCode.Created);
		}		 
	}
}