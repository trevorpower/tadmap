﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using Tadmap.Controllers;
using Tadmap.Model.Image;
using System.Reflection;

namespace TadmapTests.Controllers.Account
{
   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void Login_Returns_View_With_No_Name()
      {
         AccountController account = new AccountController();

         ViewResult result = (ViewResult)account.Login();

         Assert.AreEqual("", result.ViewName);
      }

   }
}
