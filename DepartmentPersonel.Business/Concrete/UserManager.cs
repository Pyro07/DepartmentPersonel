using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public int Count()
        {
            return _userDal.Count();
        }

        public void Delete(int? id)
        {
            _userDal.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetById(int? id)
        {
            return _userDal.GetById(id);
        }

        public void Insert(User entity)
        {
            _userDal.Insert(entity);
        }

        public void Save()
        {
            _userDal.Save();
        }

        public void Update(User entity)
        {
            _userDal.Update(entity);
        }
    }
}
