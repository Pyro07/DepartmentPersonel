using DepartmentPersonel.Entities.ApplicationModels;
using System.Collections.Generic;

namespace DepartmentPersonel.Business.Abstract
{
    public interface IDepartmentService
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
