using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;
using Tadmap.Controllers;
using NUnit.Framework;
using System.Web.Mvc;
using Tadmap.Model;
using Tadmap.Mode.Test.Mock;
using Tadmap.Model.Test.Mock;

namespace TadmapTests.Controllers.Home
{

   [TestFixture]
   public class IndexAction
   {
      static HomeController _controller;

      private static ViewResult GetViewResult()
      {
         ActionResult actionResult = _controller.Index();
         ViewResult viewResult = actionResult as ViewResult;
         return viewResult;
      }

      private static HomeView GetHomeView()
      {
         ActionResult actionResult = _controller.Index();
         ViewResult viewResult = actionResult as ViewResult;
         return viewResult.ViewData.Model as HomeView;
      }

      [SetUp]
      public void ConstructController()
      {
         List<BinaryRepository.Data> storage = new List<BinaryRepository.Data>();
         _controller = new HomeController(new ImageRepository(), new BinaryRepository(storage));
      }

      [TearDown]
      public void DestructController()
      {
         if (_controller != null)
            _controller.Dispose();
   
         _controller = null;
      }

      [Test]
      public void Returns_HomeView()
      {
         ViewResult viewResult = GetViewResult();

         Assert.IsInstanceOfType(typeof(HomeView), viewResult.ViewData.Model);
      }

      [Test]
      public void Returns_EmptyViewName()
      {
         ViewResult viewResult = GetViewResult();

         Assert.AreEqual("", viewResult.ViewName);
      }

      [Test]
      public void Returns_No_ViewData_Values()
      {
         ViewResult viewResult = GetViewResult();

         Assert.AreEqual(0, viewResult.ViewData.Count);
      }

      [Test]
      public void Returns_No_ModelState_Values()
      {
         ViewResult viewResult = GetViewResult();

         Assert.AreEqual(0, viewResult.ViewData.ModelState.Count);
      }


      [Test]
      public void Returns_4_Images()
      {
         HomeView view = GetHomeView();

         Assert.AreEqual(4, view.Images.Count);
      }
   }

}
