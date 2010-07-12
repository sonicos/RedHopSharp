using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using RedHopSharp.Serialization;

namespace RedHopSharp
{
	public class HoptoadNoticeBuilder
	{
		private readonly HoptoadConfiguration configuration;

		public HoptoadNoticeBuilder() : this(new HoptoadConfiguration())
		{	
		}

		public HoptoadNoticeBuilder(HoptoadConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public HoptoadConfiguration Configuration
		{
			get { return this.configuration; }
		}

		public HoptoadServerEnvironment ServerEnvironment()
		{
			var environment = new HoptoadServerEnvironment { 
				EnvironmentName = this.Configuration.EnvironmentName,
				ProjectRoot = this.Configuration.ProjectRoot
			};
			return environment;
		}

        public HoptoadRequest Request()
        {
            var request = new HoptoadRequest
            {

                Url = "NOT A WEB APP",
                Vars = new[] { 
                    new Vars { Key="HopToadSender", Value=string.Format("{0} v{1}",typeof(HoptoadNotice).Assembly.GetName().Name,typeof(HoptoadNotice).Assembly.GetName().Version)}
				}
            };
            return request;
        }

        public HoptoadRequest Request(HttpRequest r, string m, string session)
        {
            var request = new HoptoadRequest
            {
                Url = r.Url.ToString(),
                Vars = this.BuildVars(r, m, session).ToArray(),
                Action= "",
                Component="",
            };
            return request;
        }
        
		public HoptoadNotifier Notifier()
		{
			var notifer = new HoptoadNotifier { 
				Name = "RedHopSharp",
                Url = "http://github.com/neosonic/RedHopSharp",
				Version = typeof(HoptoadNotice).Assembly.GetName().Version.ToString()
			};
			return notifer;
		}

		public HoptoadNotice Notice(HoptoadError error)
		{
			var notice = new HoptoadNotice { 
				ApiKey = this.Configuration.ApiKey,
				Error = error,
				Notifier = this.Notifier(),
				ServerEnvironment = this.ServerEnvironment(),
			};
			return notice;
		}

        public HoptoadNotice Notice(Exception exception, HttpRequest req, string ServerName, string Session)
        {
            var notice = new HoptoadNotice
            {
                ApiKey = this.Configuration.ApiKey,
                Error = this.ErrorFromException(exception),
                Request = this.Request(req, ServerName, Session),
                Notifier = this.Notifier(),
                ServerEnvironment = this.ServerEnvironment(),
            };
            return notice;
        }
        

		public HoptoadNotice Notice(Exception exception)
		{
			var notice = new HoptoadNotice { 
				ApiKey = this.Configuration.ApiKey,
				Error = this.ErrorFromException(exception),
                Request = this.Request(),
				Notifier = this.Notifier(),
				ServerEnvironment = this.ServerEnvironment(),
			};
			return notice;
		}

		public HoptoadError ErrorFromException(Exception exception)
		{
			var error = new HoptoadError { 
				Class = exception.GetType().FullName,
				Message = exception.GetType().Name + ": " + exception.Message,
				Backtrace = this.BuildBacktrace(exception).ToArray(),
			};
			return error;
		}

        private IEnumerable<Vars> BuildVars(HttpRequest r, string server, string session)
        {
            if (this.configuration.UseRedMine)
                yield return new Vars { Key = "HopToadSender", Value = string.Format("{0} v{1}", typeof(HoptoadNotice).Assembly.GetName().Name, typeof(HoptoadNotice).Assembly.GetName().Version) };
            yield return new Vars { Key = "Server", Value= server };
            yield return new Vars { Key = "RequestType", Value = r.RequestType };
            if (r.UrlReferrer != null)
                yield return new Vars { Key = "Referrer", Value = r.UrlReferrer.ToString() };
            if (r.UserAgent != null)
                yield return new Vars { Key = "UserAgent", Value = r.UserAgent };
            if (r.RawUrl != null)
                yield return new Vars { Key = "RawURLRequest", Value = r.RawUrl.ToString() };
            if (r.UserHostName != null)
                yield return new Vars { Key = "UserHostName", Value = r.UserHostName };
            if (r.UserHostAddress != null)
                yield return new Vars { Key = "UserHostAddress", Value = r.UserHostAddress };
            if (r.HttpMethod != null)
                yield return new Vars { Key = "RequestMethod", Value = r.HttpMethod };
            if (r.RequestType != null)
                yield return new Vars { Key = "RequestType", Value = r.RequestType };
            if (r.QueryString != null)
                yield return new Vars { Key = "QueryString", Value = r.QueryString.ToString() };
            if (r.UserLanguages != null)
            {
                foreach (string l in r.UserLanguages)
                {
                    yield return new Vars { Key = "UserLanguage", Value = l };
                }

            }
            yield return new Vars { Key = "SessionID", Value = session };
        }



		private IEnumerable<TraceLine> BuildBacktrace(Exception exception)
		{
			var stackTrace = new StackTrace(exception,true);
			var frames = stackTrace.GetFrames();
            if (frames == null)
            {
                yield return null;
            }
            else
            {
                foreach (var frame in frames)
                {
                    var method = frame.GetMethod();

                    var lineNumber = frame.GetFileLineNumber();
                    if (lineNumber == 0)
                        lineNumber = frame.GetILOffset();

                    var file = frame.GetFileName();
                    if (string.IsNullOrEmpty(file))
                        file = method.ReflectedType.FullName;

                    var methodString = "";

                    if (this.Configuration.VerboseMethods)
                        methodString = method.ToString();
                    else
                        methodString = method.Name;

                    yield return new TraceLine
                    {
                        File = file,
                        LineNumber = lineNumber,
                        Method=methodString
                    };
                }
            }
            
		}
	}
}