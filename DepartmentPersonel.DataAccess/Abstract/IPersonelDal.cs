using DepartmentPersonel.Entities.ApplicationModels;
using System.Collections.Generic;

namespace DepartmentPersonel.DataAccess.Abstract
{
    public interface IPersonelDal
    {
        void Insert(Personel entity);
        void Delete(int? id);
        void Update(Personel entity);
        void Save();
        IEnumerable<Personel> GetAll();
        Personel GetById(int? id);

        IEnumerable<Personel> GetPersonelWithDepartments();
        int Count();
    }
}
