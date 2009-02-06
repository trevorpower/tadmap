using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using ImageManipulation;
using System.IO;
using Affirma.ThreeSharp;
using Tadmap.DataAccess;

namespace Tadmap.Models.ImageSets
{
   public class ImageSet1 : IImageSet
   {
      private string Key { get; set; }

      public ImageSet1(string key)
      {
         Key = key;
      }

      #region IImageSet Members

      public string Original
      {
         get { return Key; }
      }

      public string Square
      {
         get { return "Square_" + Key; }
      }
      
      public string Thumb
      {
         get { return "Thumb_" + Key; }
      }

      public string LargeThumb
      {
         get { return "LargeThumb_" + Key; }
      }

      public string Preview
      {
         get { return "Preview_" + Key; }
      }

      public void Create(System.IO.Stream stream, IBinaryRepository binaryRepository)
      {
         using (System.Drawing.Image oImage = Bitmap.FromStream(stream, true, true))
         {
            using (System.Drawing.Image oSquare = ImageManipulator.ResizeAndCrop(oImage as Bitmap, 80, 80))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oSquare.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Square, "image/jpeg");
               }
            }

            using (System.Drawing.Image oThumb = ImageManipulator.FitToRectangle(oImage as Bitmap, 100, 100))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Thumb, "image/jpeg");
               }
            }

            using (System.Drawing.Image oLargeThumb = ImageManipulator.FitToRectangle(oImage as Bitmap, 200, 200))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oLargeThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, LargeThumb, "image/jpeg");
               }
            }

            using (System.Drawing.Image oLargeThumb = ImageManipulator.FitToRectangle(oImage as Bitmap, 560, 560))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oLargeThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Preview, "image/jpeg");
               }
            }
         }

         stream.Position = 0;
         binaryRepository.Add(stream, Original, ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(Key)));

      }

      #endregion
   }
}
