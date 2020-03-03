using LFAdmin.Models;
using LFAdmin.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAdmin.Repositories
{
    public interface ISysUserRepository<TEntity>:IDisposable where TEntity:class
    {
        List<T_User> getUser();
        T_User getUserId(int id);
        void insertUser(User user);
        void updateUser(User user);
        void deleteUser(int id);

    }
}
