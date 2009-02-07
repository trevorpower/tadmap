using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using System.Security;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Index
   {
      [Test]
      public void Returns_ViewResult_For_Collector()
      {
         UploadController upload = new UploadController();

         ActionResult result = upload.Index();

         Assert.IsInstanceOfType(typeof(ViewResult), result); 
      }

      [Test]
      public void Returns_ViewResult_With_Empty_Name_And_Null_Model_With_No_Errors()
      {
         UploadController upload = new UploadController();

         ViewResult result = upload.Index() as ViewResult;

         Assert.IsEmpty(result.ViewName);
         Assert.IsNull(result.ViewData.Model);
         Assert.AreEqual(0, result.ViewData.ModelState.Count);
      }
   }
}
