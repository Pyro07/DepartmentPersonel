using DepartmentPersonel.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Concrete
{
    public class Repository
    {
        //protected DbContext _context;
        //private DbSet<T> _dbSet;

        //public Repository(DbContext context)
        //{
        //    _context = context;
        //    _dbSet = _context.Set<T>();
        //}
        //public void Delete(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}

        //public void DeleteRange(IEnumerable<T> entity)
        //{
        //    _dbSet.RemoveRange(entity);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        //public T GetById(int id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public void Insert(T entity)
        //{
        //    _dbSet.Add(entity);
        //}

        //public void InsertRange(IEnumerable<T> entity)
        //{
        //    _dbSet.AddRange(entity);
        //}
    }
}
