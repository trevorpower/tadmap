using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using System.Security;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Index
   {
      [Test]
      public void Does_Redirect_For_Guest()
      {
         UploadController upload = new UploadController();

         ActionResult result = upload.Index(Principals.Guest);

         Assert.IsInstanceOfType(typeof(RedirectResult), result);
      }

      [Test]
      public void Redirect_For_Guest_Is_TestURL()
      {
         UploadController upload = new UploadController();

         RedirectResult result = upload.Index(Principals.Guest) as RedirectResult;

         Assert.AreEqual("/TestURL", result.Url);
      }

      [Test]
      public void Returns_ViewResult_For_Collector()
      {
         UploadController upload = new UploadController();

         ActionResult result = upload.Index(Principals.Collector);

         Assert.IsInstanceOfType(typeof(ViewResult), result); 
      }

      [Test]
      public void Returns_ViewResult_With_Empty_Name_And_Null_Model_With_No_Errors()
      {
         UploadController upload = new UploadController();

         ViewResult result = upload.Index(Principals.Collector) as ViewResult;

         Assert.IsEmpty(result.ViewName);
         Assert.IsNull(result.ViewData.Model);
         Assert.AreEqual(0, result.ViewData.ModelState.Count);
      }
   }
}
