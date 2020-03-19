using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities.ApplicationModels;
using System.Collections.Generic;

namespace DepartmentPersonel.Business.Concrete
{
    public class PersonelManager : IPersonelService
    {
        private IPersonelDal _personel;
        public PersonelManager(IPersonelDal personel)
        {
            _personel = personel;
        }

        public int Count()
        {
            return _personel.Count();
        }

        public void Delete(int? id)
        {
            _personel.Delete(id);
        }

        public IEnumerable<Personel> GetAll()
        {
            return _personel.GetAll();
        }

        public Personel GetById(int? id)
        {
            return _personel.GetById(id);
        }

        public IEnumerable<Personel> GetPersonelWithDepartments()
        {
            return _personel.GetPersonelWithDepartments();
        }

        public void Insert(Personel entity)
        {
            _personel.Insert(entity);
        }

        public void Save()
        {
            _personel.Save();
        }

        public void Update(Personel entity)
        {
            _personel.Update(entity);
        }
    }
}
