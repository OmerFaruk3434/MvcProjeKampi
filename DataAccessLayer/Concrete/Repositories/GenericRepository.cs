using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataAccessLayer.Concrete.Repositories
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		Context c = new Context();
		DbSet<T> _object;

		public GenericRepository()
		{
			_object = c.Set<T>();
		}
		public void Delete(T p)
		{
			var deletedEntity = c.Entry(p);
			deletedEntity.State = EntityState.Deleted;
			//_object.Remove(p);
			c.SaveChanges();
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			return _object.SingleOrDefault(filter);
		}

		public void Insert(T p)
		{
			var addedEntity = c.Entry(p);
			addedEntity.State = EntityState.Added;
			//_object.Add(p);
			c.SaveChanges();
		}

		public List<T> List()
		{
			return _object.ToList();
		}

		public List<T> List(Expression<Func<T, bool>> filter)
		{
			return _object.Where(filter).ToList();
		}
		public void Update(T p)
		{
			var keyProperty = typeof(T).GetProperties()
				.FirstOrDefault(prop => prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "ID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "ContactID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "AboutID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "AdminID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "CategoryID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "ContentID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "HeadingID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "ImageID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "WriterID", StringComparison.OrdinalIgnoreCase)
				|| prop.Name.Equals(typeof(T).Name + "MigrationId", StringComparison.OrdinalIgnoreCase)
			);
			if (keyProperty == null)
			{
				throw new Exception("Primary key property not found.");
			}
			var primaryKeyValue = keyProperty.GetValue(p);
			var existingEntity = _object.Find(primaryKeyValue);
			if (existingEntity != null)
			{
				c.Entry(existingEntity).CurrentValues.SetValues(p);
			}
			else
			{
				_object.Add(p);
			}
			c.SaveChanges();
		}
	}
}
