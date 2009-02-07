using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using Tadmap.Views.Image;

namespace RepositoryIntegrationTests.Controllers
{
   [TestFixture]
   public class ImageControllerTests
   {
      [Test]
      public void Should_Return_View_Result()
      {
         ImageController controller = new ImageController();

         ActionResult result = controller.Index(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59"), Principals.Collector);

         Assert.IsInstanceOfType(typeof(ViewResult), result);
      }

      [Test]
      public void Result_Has_No_Name_And_Contains_A_Model()
      {
         ImageController controller = new ImageController();

         ActionResult result = controller.Index(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;

         Assert.IsEmpty(viewResult.ViewName);
         Assert.IsNotNull(viewResult.ViewData.Model);
      }

      [Test]
      public void Model_Is_ImageView()
      {
         ImageController controller = new ImageController();

         ActionResult result = controller.Index(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         Assert.IsInstanceOfType(typeof(ImageView), viewResult.ViewData.Model);
      }

      [Test]
      public void Is_Publid_Is_True_For_Public_Image()
      {
         ImageController controller = new ImageController();

         ActionResult result = controller.Index(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView image = viewResult.ViewData.Model as ImageView;
         Assert.IsTrue(image.IsPublic);
      }

   }
}
