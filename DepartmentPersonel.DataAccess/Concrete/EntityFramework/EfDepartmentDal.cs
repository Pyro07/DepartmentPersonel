using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DepartmentPersonel.DataAccess.Concrete.EntityFramework
{
    public class EfDepartmentDal : IDepartmentDal
    {
        private DepartmentPersonelContext _context;

        public EfDepartmentDal()
        {
            _context = new DepartmentPersonelContext();
        }

        public int Count()
        {
            return _context.Departments.Count();
        }

        public void Delete(int? id)
        {
            //_context.Departments.Remove(_context.Departments.FirstOrDefault(x => x.Id == id));
            Department department = _context.Departments.Find(id);
            _context.Departments.Remove(department);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int? id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Department entity)
        {
            _context.Departments.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Department entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Departments.AddOrUpdate(entity);
        }
    }
}
