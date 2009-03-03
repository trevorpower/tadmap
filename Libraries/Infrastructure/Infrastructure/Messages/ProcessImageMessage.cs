using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Infrastructure.Messages
{
   public class ProcessImageMessage
   {
      private static readonly string NameSpace = "http://tadmap.com/schemas/cookiedata";

      public ProcessImageMessage(Guid id, string name, string[] roles)
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

         Guid id = new Guid(document.Descendants(ns + "id").Single().Value);
         string name = document.Descendants(ns + "display").Single().Value;

         return new ProcessImageMessage(id, name, roles.ToArray());
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

      public Guid Id { get; private set; }

      public string DisplayName { get; private set; }

      public string[] Roles { get; private set; }
   }
}
