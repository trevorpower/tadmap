using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Tadmap.Security;

namespace TadmapTests.Tadmap.Security
{
   [TestFixture]
   public class CookieUserData_Tests
   {
      static readonly string _roleOne = "Role 1";
      static readonly string _roleTwo = "Role 2";

      static readonly Guid _id = Guid.NewGuid();
      static readonly string _displayName = "Display Name";
      static readonly string[] _twoRoles = { _roleOne, _roleTwo };

      [Test]
      public void Has_Id_Name_And_Roles()
      {
         CookieUserData data = new CookieUserData(_id, _displayName, _twoRoles);

         Assert.AreEqual(_id, data.Id);
         Assert.AreEqual(_displayName, data.DisplayName);
         Assert.IsNotEmpty(data.Roles);
      }

      [Test]
      public void Has_ToString()
      {
         CookieUserData data = new CookieUserData(_id, _displayName, _twoRoles);
         string asString = data.ToString();
         Assert.IsNotNull(asString);
      }

      [Test]
      public void HasSameValuesAfterToStringAndParse()
      {
         CookieUserData data = new CookieUserData(_id, _displayName, _twoRoles);
         string asString = data.ToString();

         CookieUserData parsedData = CookieUserData.Parse(asString);

         Assert.AreEqual(_id, parsedData.Id);
         Assert.AreEqual(_displayName, parsedData.DisplayName);
         Assert.AreEqual(2, parsedData.Roles.Count());

         Assert.Contains(_roleOne, parsedData.Roles);
         Assert.Contains(_roleTwo, parsedData.Roles);
      }
   }
}
