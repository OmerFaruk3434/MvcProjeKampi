using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
	public class WriterController : Controller
	{

		WriterManager wm = new WriterManager(new EfWriterDal());
		WriterValidator writervalidator = new WriterValidator();
		public ActionResult Index(int? p)
		{
			int pageNumber = p ?? 1;
			int pageSize = 6;

			var writers = wm.GetList().OrderBy(w => w.WriterID).ToPagedList(pageNumber, pageSize);
			if (Request.IsAjaxRequest())
			{
				return PartialView("WriterPartialView", writers);
			}

			return View(writers);
		}
		public PartialViewResult WriterPartialView()
		{
			return PartialView();
		}
		[HttpGet]
		public ActionResult AddWriter()
		{
			return View();
		}
		[HttpPost]
		public ActionResult AddWriter(Writer p)
		{
			ValidationResult results = writervalidator.Validate(p);
			if (results.IsValid)
			{
				wm.WriterAdd(p);
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var item in results.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}
	}
}