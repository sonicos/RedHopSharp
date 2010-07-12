using System;
using System.Web;

namespace RedHopSharp
{
	public static class Extension
	{
		public static void SendToHoptoad(this Exception exception)
		{
			HoptoadClient client = new HoptoadClient();
			client.Send(exception);
		}
        public static void SendToHoptoad(this Exception exception,  HttpApplication app )
        {
            HoptoadClient client = new HoptoadClient();
            client.Send(exception, app);
        }
	}
}