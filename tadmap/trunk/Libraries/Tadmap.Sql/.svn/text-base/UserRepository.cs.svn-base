using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tadmap.Model.User;

namespace Tadmap.Sql
{
   public class UserRepository : IUserRepository
   {
      private string ConnectionString {get; set;}

      public UserRepository(string connectionString)
      {
         ConnectionString = connectionString;
      }

      #region IImageRepository Members

      public IQueryable<Model.User.User> GetAllUsers()
      {
         TadmapDb db = new TadmapDb(ConnectionString);

         return from i in db.Users
                select new Model.User.User
                {
                   Id = i.Id,
                   OpenIds = i.OpenIds.Select(id => id.OpenIdUrl).ToList(),
                   Roles = i.UserRoles.Select(role => role.Role).ToList()
                };
      }

      public void Save(Model.User.User user)
      {
         bool isNew = false;

         TadmapDb db = new TadmapDb(ConnectionString);
         User dbUser = db.Users.SingleOrDefault(i => i.Id == user.Id);

         if (dbUser == null)
         {
            dbUser = new User();

            dbUser.Name = user.Name;

            var roles = user.Roles.Select(r => new UserRole { Role = r });
            dbUser.UserRoles.AddRange(roles);

            var openIds = user.OpenIds.Select(id => new OpenId { OpenIdUrl = id });
            dbUser.OpenIds.AddRange(openIds);

            isNew = true;
         }
         else
         {
            isNew = false;
         }

         if (isNew)
            db.Users.InsertOnSubmit(dbUser);

         db.SubmitChanges();
      }

      #endregion
   }
}
