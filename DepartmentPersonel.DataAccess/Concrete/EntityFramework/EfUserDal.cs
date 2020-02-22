using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:IUserDal
    {
        private DepartmentPersonelContext _context;
        public EfUserDal()
        {
            _context = new DepartmentPersonelContext();
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public void Delete(int? id)
        {
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int? id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.AddOrUpdate(entity);
        }
    }
}
