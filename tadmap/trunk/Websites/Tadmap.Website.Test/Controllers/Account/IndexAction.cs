using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using Tadmap.Controllers;
using Tadmap.Model.Image;
using System.Reflection;
using Rhino.Mocks;
using Tadmap.Model.User;

namespace TadmapTests.Controllers.Account
{
   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void Login_Returns_View_With_No_Name()
      {
         AccountController account = new AccountController(MockRepository.GenerateMock<IUserRepository>());

         ViewResult result = (ViewResult)account.Login();

         Assert.AreEqual("", result.ViewName);
      }

   }
}
