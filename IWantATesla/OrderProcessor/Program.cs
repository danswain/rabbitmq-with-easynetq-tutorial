using System;
using EasyNetQ;
using IWantATesla.Messages;

namespace OrderProcessor
{
	class Program
	{
		static void Main(string[] args)
		{
		    var logger = new LogNothingLogger();
			var bus = RabbitHutch.CreateBus("host=localhost",reg=>reg.Register<IEasyNetQLogger>(log=>logger));

			bus.Subscribe<TeslaOrder>("my_subscription_id", msg =>
			{
				Console.WriteLine("Processing Order: {0} -- Model: {1} -- Color: {2}",
                    msg.CustomerEmail,
                    msg.Model,
                    msg.Color);				
			});
		}
	}

    public class LogNothingLogger : IEasyNetQLogger
    {
        public void DebugWrite(string format, params object[] args)
        {
        }

        public void InfoWrite(string format, params object[] args)
        {
        }

        public void ErrorWrite(string format, params object[] args)
        {
        }

        public void ErrorWrite(Exception exception)
        {
        }
    }
}
