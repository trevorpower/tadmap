using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace Tadmap_MVC.Tadmap.Security
{
   [Serializable]
   public class TadmapIdentity : MarshalByRefObject, IIdentity
   {
      public TadmapIdentity(string name, string displayName, Guid id, string authenticationType)
      {
         Name = name;
         DisplayName = displayName;
         Id = id;
         AuthenticationType = authenticationType;
      }

      #region IIdentity Members

      public string AuthenticationType
      {
         get;
         private set;
      }

      public bool IsAuthenticated
      {
         get { return true; }
      }

      public string Name
      {
         get;
         private set;
      }

      public string DisplayName
      {
         get;
         private set;
      }

      #endregion

      public Guid Id
      {
         get;
         private set;
      }
   }
}
