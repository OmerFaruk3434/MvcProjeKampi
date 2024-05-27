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
		Context c = new Context();
		// GET: Content
		ContentManager cm = new ContentManager(new EfContentDal());
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult GetAllContent(string p , int d = 1)
		{
			if (string.IsNullOrEmpty(p))
			{
				c.Contents.ToList();
			}
			
			var values = cm.GetList(p).ToPagedList(d, 10);
			return View("GetAllList",values);
		}
		public ActionResult GetAllList(int p = 1)
		{
			var values = cm.GetAllList().ToPagedList(p, 10);
			return View(values);
		}
		public ActionResult ContentByHeading(int id)
		{
			var contentvalues = cm.GetListByHeadingID(id);
			return View(contentvalues);
		}
	}
}