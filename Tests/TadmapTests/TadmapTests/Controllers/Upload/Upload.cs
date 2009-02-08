using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
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
      public void Returns_Redirect_For_Collector()
      {
         UploadController upload = new UploadController(new TestImageRepository(), new TestBinaryRepository());

         ActionResult result = upload.Upload("title", "description", Principals.Collector, new TestEmptyFile());

         Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
      }
   }
}
