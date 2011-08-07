using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Model.User
{
   public class User
   {
      public int Id { get; set; }
      public string Name { get; set; }

      public List<string> OpenIds { get; set; }
      public List<string> Roles { get; set; }
   }
}
