using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WriterManager : IWriterService
	{
		IWriterDal _writerDal;

		public WriterManager(IWriterDal writerDal)
		{
			_writerDal = writerDal;
		}

		public Writer GetByID(int id)
		{
			return _writerDal.Get(x => x.WriterID == id);
		}

		public List<Writer> GetList()
		{
			return _writerDal.List();
		}

		public void WriterAdd(Writer writer)
		{
			_writerDal.Insert(writer);
		}

		public void WriterDelete(Writer writer)
		{
			_writerDal.Delete(writer);
		}

		public void WriterUpdate(Writer writer)
		{
			if (string.IsNullOrEmpty(writer.WriterPassword))
			{
				Writer existingWriter = GetByID(writer.WriterID);
				if (existingWriter != null)
				{
					writer.WriterPassword = existingWriter.WriterPassword;
				}
			}
			else if (!string.IsNullOrEmpty(writer.WriterPassword))
			{
				using (SHA512 sha512Hash = SHA512.Create())
				{
					byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(writer.WriterPassword));
					StringBuilder builder = new StringBuilder();
					for (int i = 0; i < bytes.Length; i++)
					{
						builder.Append(bytes[i].ToString("x2"));
					}
					string hashedPassword = builder.ToString();
					writer.WriterPassword = hashedPassword;
				}
			}
			_writerDal.Update(writer);
		}
	}
}
