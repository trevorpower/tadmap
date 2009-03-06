using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ImageManipulation;
using System.Drawing;
using System.IO;

namespace UnitTests
{
   [TestFixture]
   public class Thumbnail
   {
      [Test]
      public void Can_Resize_And_Crop_Small_File()
      {
         Image source = Bitmap.FromFile("../../TestFiles/TestImageTwo.png", true);
         Bitmap result = ImageManipulator.ResizeAndCrop((source as Bitmap), 100, 100);

         Assert.AreEqual(100, result.Width);
         Assert.AreEqual(100, result.Height);
      }

      [Test]
      public void Can_Resize_And_Crop_Large_File()
      {
         using (Image source = Bitmap.FromFile("../../TestFiles/Large.tif", true))
         {
            Image result = ImageManipulator.ResizeAndCrop((source as Bitmap), 80, 80);

            Assert.AreEqual(80, result.Width);
            Assert.AreEqual(80, result.Height);
         }
      }

      [Test]
      public void Can_Resize_And_Crop_Large_File_From_Stream()
      {
         using (FileStream file = new FileStream("../../TestFiles/Large.tif", FileMode.Open))
         {
            using (Image source = Bitmap.FromStream(file, true))
            {
               Image result = ImageManipulator.ResizeAndCrop((source as Bitmap), 80, 80);

               Assert.AreEqual(80, result.Width);
               Assert.AreEqual(80, result.Height);
            }
         }
      }
   }
}
