﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
		public ActionResult Index(int p = 1)
		{
			var WriterValues = wm.GetList().ToPagedList(p, 6);
			return View(WriterValues);
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
		[HttpGet]
		public ActionResult EditWriter(int id)
		{
			var writervalue = wm.GetByID(id);
			return View(writervalue);
		}
		[HttpPost]
		public ActionResult EditWriter(Writer p)
		{
			ValidationResult results = writervalidator.Validate(p);
			if (results.IsValid)
			{
				wm.WriterUpdate(p);
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