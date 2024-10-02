using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
		public PartialViewResult RegisterFormPartialView()
		{
			return PartialView();
		}
	}
}