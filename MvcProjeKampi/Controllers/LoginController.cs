using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());
		AdminManager am = new AdminManager(new EfAdminDal());

		// GET: Login
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(Admin p)
		{
			var adminuserinfo = am.GetByUser(p);
			if (adminuserinfo != null)
			{
				FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
				Session["AdminUserName"] = adminuserinfo.AdminUserName;
				return RedirectToAction("Index", "Category");
			}
			else
			{
				return RedirectToAction("Index");
			}
		}
		[HttpGet]
		public ActionResult WriterLogin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult WriterLogin(Writer p)
		{
			var writeruserinfo = wm.GetWriter(p.WriterMail, p.WriterID, p.WriterPassword);
			if (writeruserinfo != null)
			{
				FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
				Session["WriterMail"] = writeruserinfo.WriterMail;
				return RedirectToAction("MyContent", "WriterPanelContent");
			}
			else
			{
				return RedirectToAction("WriterLogin");
			}
		}

		[HttpPost]
		public ActionResult Register(Writer p)
		{
			if (wm.GetByMail(p.WriterMail) != null)
			{
				ViewBag.ErrorMessage = "Bu mail adresi ile daha önce kayıt olunmuş.";
			}
			else
			{
				wm.WriterAdd(p);
			}
			ModelState.Clear();
			return RedirectToAction("WriterLogin");
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("WriterLogin", "Login");
		}
	}
}