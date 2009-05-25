using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Model.User
{
   public interface IUserRepository
   {
      IQueryable<User> GetAllUsers();

      void Save(User user);
   }
}
