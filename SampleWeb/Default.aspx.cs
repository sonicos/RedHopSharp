using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using RedHopSharp;
namespace SampleWeb
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
        protected void blinkin()
        {
            terror();
        }

        protected void terror()
        {
            clown();
        }
        protected void clown()
        {
            yeti();
        }
        protected void yeti()
        {
            nest();
        }
        protected void nest()
        {
            thermopole("x");
            
        }
        // I did this to demonstrate which one of the overloads gets shown in the backtrace
        protected void thermopole()
        {
            Exception ex = null;
            string s = ex.Message;
        }

        protected void thermopole(string m)
        {
            int n = 0;
            int i = 3 / n;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            blinkin();
        }
	}

}
