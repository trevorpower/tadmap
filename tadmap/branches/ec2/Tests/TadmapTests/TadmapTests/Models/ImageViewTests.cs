﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Views.Image;
using TadmapTests.Mocks;

namespace TadmapTests.Models
{
   [TestFixture]
   public class ImageViewTests
   {
      [Test]
      public void Has_Title_Description_IsEditable_IsPublic_And_PreviewUrl()
      {
         Guid id = Guid.NewGuid();

         ImageView view = new ImageView(id, "Title", "Description", MockUrls.Original);

         Assert.AreEqual(id, view.Id);
         Assert.AreEqual("Title", view.Title);
         Assert.AreEqual("Description", view.Description);
         Assert.IsNotNull(view.IsPublic);
         Assert.IsNotNull(view.IsEditable);
         Assert.IsNull(view.PreviewUrl);
         Assert.AreEqual(MockUrls.Original, view.OriginalUrl);
      }

      [Test]
      public void Has_Title_Description_IsEditable_With_False_Value_IsPublic_And_PreviewUrl()
      {
         Guid id = Guid.NewGuid();

         ImageView view = new ImageView(id, "TitleF", "DescriptionF", MockUrls.Original);

         Assert.AreEqual(id, view.Id);
         Assert.AreEqual("TitleF", view.Title);
         Assert.AreEqual("DescriptionF", view.Description);
         Assert.IsNotNull(view.IsEditable);
         Assert.IsNotNull(view.IsPublic);
         Assert.IsNull(view.PreviewUrl);
         Assert.AreEqual(MockUrls.Original, view.OriginalUrl);
      }

      [Test]
      public void Has_OffensiveCount_And_ShowOffensiveCount()
      {
         ImageView view = new ImageView { OffensiveCount = 0, ShowOffensiveCount = true };

         Assert.AreEqual(0, view.OffensiveCount);
         Assert.AreEqual(true, view.ShowOffensiveCount);
      }

      [Test]
      public void Has_CanMarkOffensive_And_CanMarkUnoffensive()
      {
         ImageView view = new ImageView { CanMarkOffensive = true, CanUnmarkOffensive = true };

         Assert.IsTrue(view.CanMarkOffensive);
         Assert.IsTrue(view.CanUnmarkOffensive);
      }
   }
}
