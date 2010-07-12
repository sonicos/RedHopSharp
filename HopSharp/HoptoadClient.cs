using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using RedHopSharp.Serialization;

namespace RedHopSharp
{
	public class HoptoadClient
	{
		private HoptoadNoticeBuilder builder;

		public HoptoadClient()
		{
			this.builder = new HoptoadNoticeBuilder();	
		}

        public string Send(Exception e)
        {
            var notice = this.builder.Notice(e);
            //var m = e;
            //TODO: set up request, session and server headers

            // Send the notice
            return this.Send(notice);
        }
        public string Send(Exception e, HttpApplication app)
        {
            var x = app;
            HoptoadNotice notice = new HoptoadNotice();
            try
            {
                notice = this.builder.Notice(e, app.Request, app.Server.MachineName, app.Session.SessionID);
            }
            catch
            {
                notice = this.builder.Notice(e, app.Request, app.Server.MachineName, null);
            }

            //TODO: set up request, session and server headers

            // Send the notice
           return this.Send(notice);
        }

		public string Send(HoptoadNotice notice)
		{
            string message;
			try
			{
				// If no API key, get it from the appSettings
				if (string.IsNullOrEmpty(notice.ApiKey))
				{
                   
					// If none is set, just return... throwing an exception is pointless, since one was already thrown!
					if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Hoptoad:ApiKey"]) && string.IsNullOrEmpty(ConfigurationManager.AppSettings["Hoptoad:RedMineAPIKey"]))
						return "RedHopToad Error: No API Key is defined";

					notice.ApiKey = this.builder.Configuration.ApiKey;
				}

				// Create the web request
                string HopToadURL;
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Hoptoad:HopToadServerURL"]))
                    HopToadURL = "http://hoptoadapp.com/notifier_api/v2/notices";
                else
                    HopToadURL = ConfigurationManager.AppSettings["Hoptoad:HopToadServerURL"];
                
				HttpWebRequest request = WebRequest.Create(HopToadURL) as HttpWebRequest;
				if (request == null)
					return "RedHopToad Error: Invalid Web Request";
                

				// Set the basic headers
				request.ContentType = "text/xml";
				request.Accept = "text/xml";
				request.KeepAlive = false;

				// It is important to set the method late... .NET quirk, it will interfere with headers set after
				request.Method = "POST";

				// Go populate the body
				message = SetRequestBody(request, notice);

				// Begin the request, yay async
				request.BeginGetResponse(RequestCallback, null);
			}
			catch (Exception e)
			{
				// Since an exception was already thrown, allowing another one to bubble up is pointless
				// But we should log it or something
				// TODO this could be better
                return "RedHopToad Error: " + e.Message;
			}
            return message;
		}

		private void RequestCallback(IAsyncResult ar)
		{
			// Get it back
			var request = ar.AsyncState as HttpWebRequest;
            if (request == null)
                return;

			// We want to swallow any error responses
			try
			{
				request.EndGetResponse(ar);
                return;
			}
			catch (WebException e)
			{
				// Since an exception was already thrown, allowing another one to bubble up is pointless
				// But we should log it or something
				// TODO this could be better
				Console.WriteLine("." + e.Message + ".");
				var sr = new StreamReader(e.Response.GetResponseStream());
				Console.WriteLine(sr.ReadToEnd());
				sr.Close();
                return;
			}
		}

		private string SetRequestBody(HttpWebRequest request, HoptoadNotice notice)
		{
			var serializer = new CleanXmlSerializer<HoptoadNotice>();
            var xml = "";
			if (this.builder.Configuration.UseRedMine == true)
                 xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + serializer.ToXml(notice); 
            else
                xml = serializer.ToXml(notice);

			var payload = Encoding.UTF8.GetBytes(xml);
			request.ContentLength = payload.Length;
            //HttpWebResponse HttpWResp = (HttpWebResponse)request.GetResponse();
            // Insert code that uses the response object.
           // HttpWResp.Close();

            using (var stream = request.GetRequestStream())
            {
                stream.Write(payload, 0, payload.Length);
                stream.Close();
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string contents = reader.ReadToEnd();
                reader.Close();
                return contents;
            }
            catch (Exception e)
            {
                return string.Format("RedHopToad Error {0}: Bug not reported", e.Message);
            }
		}
	}
}