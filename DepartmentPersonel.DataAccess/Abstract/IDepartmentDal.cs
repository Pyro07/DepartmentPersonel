using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
