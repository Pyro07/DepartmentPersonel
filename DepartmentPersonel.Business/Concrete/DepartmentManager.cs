using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities.ApplicationModels;
using System.Collections.Generic;

namespace DepartmentPersonel.Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public int Count()
        {
            return _departmentDal.Count();
        }

        public void Delete(int? id)
        {
            _departmentDal.Delete(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentDal.GetAll();
        }

        public Department GetById(int? id)
        {
            return _departmentDal.GetById(id);
        }

        public void Insert(Department entity)
        {
            _departmentDal.Insert(entity);
        }

        public void Save()
        {
            _departmentDal.Save();
        }

        public void Update(Department entity)
        {
            _departmentDal.Update(entity);
        }
    }
}
