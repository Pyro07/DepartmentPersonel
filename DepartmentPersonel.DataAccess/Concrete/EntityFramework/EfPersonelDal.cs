using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Concrete.EntityFramework
{
    public class EfPersonelDal : IPersonelDal
    {
        private DepartmentPersonelContext _context;
        public EfPersonelDal()
        {
            _context = new DepartmentPersonelContext();
        }

        public int Count()
        {
            return _context.Personels.Count();
        }

        public void Delete(int id)
        {
            Personel personel = _context.Personels.Find(id);
            _context.Personels.Remove(personel);
        }

        public IEnumerable<Personel> GetAll()
        {
            return _context.Personels.ToList();
        }

        public Personel GetById(int id)
        {
            return _context.Personels.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Personel> GetPersonelWithDepartments()
        {
            return _context.Personels.Include("Department").ToList();
        }

        public void Insert(Personel entity)
        {
            _context.Personels.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Personel entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Personels.AddOrUpdate(entity);
        }
    }
}
