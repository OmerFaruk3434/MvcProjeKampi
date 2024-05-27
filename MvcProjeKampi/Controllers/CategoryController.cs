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
	
	public class CategoryController : Controller
	{
		ContentManager contentManager = new ContentManager(new EfContentDal());
		CategoryManager cm = new CategoryManager(new EfCategoryDal());
		[Authorize(Roles = "B")]
		public ActionResult Index()
		{
			var categoryvalues = cm.GetList();
			return View(categoryvalues);
		}
		[HttpGet]
		public ActionResult AddCategory()
		{
			return View();
		}
		[HttpPost]
		public ActionResult AddCategory(Category p)
		{
			CategoryValidatior categoryvalidator = new CategoryValidatior();
			ValidationResult results = categoryvalidator.Validate(p);
			if (results.IsValid)
			{
				cm.CategoryAdd(p);
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
		public ActionResult DeleteCategory(int id)
		{
			var categoryvalue=cm.GetByID(id);
			cm.CategoryDelete(categoryvalue);
			return RedirectToAction("Index");
		}
		public ActionResult StatusCategory(int id)
		{
			var HeadingValue = cm.GetByID(id);
			if (HeadingValue.CategoryStatus == false)
			{
				HeadingValue.CategoryStatus = true;
			}
			else
			{
				HeadingValue.CategoryStatus = false;
			}
			cm.CategoryUpdate(HeadingValue);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public ActionResult EditCategory(int id)
		{
			var categoryvalue = cm.GetByID(id);
			return View(categoryvalue);
		}
		[HttpPost]
		public ActionResult EditCategory(Category p)
		{
			cm.CategoryUpdate(p);
			return RedirectToAction("Index");
		}
		public ActionResult CategoryContentDetail(int id)
		{
			var values = contentManager.CategoryList(id);
			return View(values);
		}
	}
}