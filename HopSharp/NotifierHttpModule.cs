using System;
using System.Web;

namespace RedHopSharp
{
	public class NotifierHttpModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.Error += new EventHandler(context_Error);
		}

		void context_Error(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			HoptoadClient client = new HoptoadClient();
			Exception exception = application.Server.GetLastError().GetBaseException();
            client.Send(exception, application);
		}

		public void Dispose()  { }
	}
}