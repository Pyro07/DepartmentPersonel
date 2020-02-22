using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Abstract
{
    public interface IUserDal
    {
        void Insert(User entity);
        void Delete(int? id);
        void Update(User entity);
        void Save();
        IEnumerable<User> GetAll();
        User GetById(int? id);
        int Count();
    }
}
