using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFAdmin.Models;
using LFAdmin.Models.Model;

namespace LFAdmin.Repositories
{ 
    public class SysUserRepository<TEntity> : ISysUserRepository<TEntity> where TEntity : class
    {
        //注入
        internal LFAdminEntities db; 
        public SysUserRepository(LFAdminEntities db)
        {
            this.db = db;
        }
        public void deleteUser(int id)
        {
            T_User user = db.T_User.Find(id);
            db.T_User.Remove(user);
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            
            throw new NotImplementedException();
        }

        public List<T_User> getUser()
        {
            return db.T_User.ToList();
            //throw new NotImplementedException();
        }

        public T_User getUserId(int id)
        {
            throw new NotImplementedException();
        }

        public void insertUser(User user)
        {
            throw new NotImplementedException();
        }

        public void updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
