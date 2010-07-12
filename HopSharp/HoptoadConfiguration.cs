using System;
using System.Configuration;
using System.Web;

namespace RedHopSharp
{
	public class HoptoadConfiguration
	{
		public HoptoadConfiguration()
		{
            if (ConfigurationManager.AppSettings["Hoptoad:RedMineActive"] != "true")
                this.ApiKey = ConfigurationManager.AppSettings["Hoptoad:ApiKey"];
            else
            {
                this.ApiKey = RedMineAPIString();
                this.UseRedMine = true;
            }

            if (ConfigurationManager.AppSettings["Hoptoad:VerboseMethods"] == "true")
                this.VerboseMethods = true;
            else
                this.VerboseMethods=false;

            this.EnvironmentName = ConfigurationManager.AppSettings["Hoptoad:Environment"];

			if (HttpContext.Current != null)
			{
				this.ProjectRoot = HttpContext.Current.Request.ApplicationPath;
			}
			else
			{
				this.ProjectRoot = Environment.CurrentDirectory;
			}
		}


		public string ProjectRoot { get; set; }
		public string ApiKey { get; set; }
        public string EnvironmentName { get; set; }
        public bool UseRedMine { get; set;  }
        public bool VerboseMethods { get; set; }
       
        private string RedMineAPIString()
        {
            if (ConfigurationManager.AppSettings["Hoptoad:RedMinePriority"] != null)
                 return string.Format("---\n:tracker: {0}\n:category: {1}\n:project: {2}\n:api_key: {3}\n:priority: {4}\n", ConfigurationManager.AppSettings["Hoptoad:RedMineTracker"], ConfigurationManager.AppSettings["Hoptoad:RedMineCategory"], ConfigurationManager.AppSettings["Hoptoad:RedMineProject"], ConfigurationManager.AppSettings["Hoptoad:RedMineAPIKey"], ConfigurationManager.AppSettings["Hoptoad:RedMinePriority"]);
            else
                return string.Format("---\n:tracker: {0}\n:category: {1}\n:project: {2}\n:api_key: {3}\n", ConfigurationManager.AppSettings["Hoptoad:RedMineTracker"], ConfigurationManager.AppSettings["Hoptoad:RedMineCategory"], ConfigurationManager.AppSettings["Hoptoad:RedMineProject"], ConfigurationManager.AppSettings["Hoptoad:RedMineAPIKey"]);
        }
	}


}