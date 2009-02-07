using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Models.Images;
using Tadmap.Models.ImageSets;

namespace TadmapTests.Models
{
   [TestFixture]
   public class ImageSetTests
   {
      [Test]
      public void Image_Set_Has_Original_And_Preview_Strings()
      {
         IImageSet set = new ImageSet1("");

         Assert.IsNotNull(set.Original);
         Assert.IsNotNull(set.Preview);

      }
   }
}
