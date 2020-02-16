using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Business.Abstract
{
    public interface IPersonelService
    {
        void Insert(Personel entity);
        void Delete(int id);
        void Update(Personel entity);
        void Save();
        IEnumerable<Personel> GetAll();
        Personel GetById(int id);
        IEnumerable<Personel> GetPersonelWithDepartments();
        int Count();
    }
}
