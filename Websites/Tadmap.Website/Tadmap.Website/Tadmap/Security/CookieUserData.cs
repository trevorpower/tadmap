using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Tadmap.Tadmap.Security
{
   public class CookieUserData
   {
      private const string NameSpace = "http://tadmap.com/schemas/cookiedata";

      public CookieUserData(int id, string name, string[] roles)
      {
         Id = id;
         DisplayName = name;
         Roles = roles;
      }

      public static CookieUserData Parse(string data)
      {
         XDocument document = XDocument.Load(new StringReader(data));
         XNamespace ns = NameSpace;

         var roles = from r in document.Descendants(ns + "roles").Descendants(ns + "role")
                     select r.Value;

         int id = int.Parse(document.Descendants(ns + "id").Single().Value);
         string name = document.Descendants(ns + "display").Single().Value;

         return new CookieUserData(id, name, roles.ToArray());
      }

      public override string ToString()
      {
         XNamespace ns = NameSpace;

         XDocument document = new XDocument(
            new XElement(ns + "userData",
               new XAttribute(XNamespace.Xmlns + "t", ns), 
               new XElement(ns + "id", Id),
               new XElement(ns + "display", DisplayName),
               new XElement(ns + "roles",
                  Array.ConvertAll(Roles, e => new XElement(ns + "role", e))
               )
            )
         );

         return document.ToString();
      }

      public int Id { get; private set; }

      public string DisplayName { get; private set; }

      public string[] Roles { get; private set; }
   }
}
