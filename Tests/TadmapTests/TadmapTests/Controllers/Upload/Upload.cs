using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using System.Security;
using System.Web;
using TadmapTests.DataAccess;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Upload
   {
      [Test]
      public void Does_Redirect_For_Guest()
      {
         UploadController upload = new UploadController();

         ActionResult result = upload.Upload("title", "description", Principals.Guest, new TestUploadedFile());

         Assert.IsInstanceOfType(typeof(RedirectResult), result);
      }

      [Test]
      public void Redirect_For_Guest_Is_TestURL()
      {
         UploadController upload = new UploadController();

         RedirectResult result = upload.Upload("title", "description", Principals.Guest, new TestUploadedFile()) as RedirectResult;

         Assert.AreEqual("/TestURL", result.Url);
      }


      [Test]
      public void Returns_Redirect_For_Collector()
      {
         UploadController upload = new UploadController();

         ActionResult result = upload.Upload("title", "description", Principals.Collector, new TestUploadedFile());

         Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
      }
   }
}
