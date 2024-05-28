using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
	public class ContentController : Controller
	{
		// GET: Content
		ContentManager cm = new ContentManager(new EfContentDal());
		public ActionResult Index(int? p)
		{
			int pageNumber = p ?? 1;
			int pageSize = 10;

			var writers = cm.GetAllList().OrderBy(w => w.ContentID).ToPagedList(pageNumber, pageSize);
			if (Request.IsAjaxRequest())
			{
				return PartialView("GetAllListPartialView", writers);
			}

			return View(writers);
		}
		public PartialViewResult GetAllListPartialView()
		{
			return PartialView();
		}
	}
}