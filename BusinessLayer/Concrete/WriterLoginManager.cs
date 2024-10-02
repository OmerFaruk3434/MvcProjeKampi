using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BusinessLayer.Concrete
{
	public class WriterLoginManager : IWriterLoginService
	{
		IWriterDal _writerDal;

		public WriterLoginManager(IWriterDal writerDal)
		{
			_writerDal = writerDal;
		}
		public Writer GetWriter(string usernameOrMail, int id, string password)
		{
			using (SHA512 sha512Hash = SHA512.Create())
			{
				byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				string hashedPassword = builder.ToString();

				return _writerDal.Get(x =>
					(x.WriterMail == usernameOrMail || x.WriterID == id) &&
					x.WriterPassword == hashedPassword);
			}
		}

		public Writer GetWriter(string usernameOrMail, string password, string mail, int id)
		{
			throw new NotImplementedException();
		}
		public Writer GetByMail(string mail)
		{
			return _writerDal.Get(x => x.WriterMail == mail);
		}

		public void WriterAdd(Writer writer)
		{
			writer.WriterPassword = HashPassword(writer.WriterPassword);
			_writerDal.Insert(writer);
		}
		private string HashPassword(string password)
		{
			using (SHA512 sha512Hash = SHA512.Create())
			{
				byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
