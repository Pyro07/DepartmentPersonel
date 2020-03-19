using DepartmentPersonel.Entities.ApplicationModels;
using System.Collections.Generic;

namespace DepartmentPersonel.DataAccess.Abstract
{
    public interface IDepartmentDal
    {
        void Insert(Department entity);
        void Delete(int? id);
        void Update(Department entity);
        void Save();
        IEnumerable<Department> GetAll();
        Department GetById(int? id);

        int Count();
    }
}
