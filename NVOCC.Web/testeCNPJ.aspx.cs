using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ABAINFRA.Web
{
	public partial class testeCNPJ : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

	    public string ActionResult()
		{
			return JsonConvert.SerializeObject("oi");
		}

	}
}