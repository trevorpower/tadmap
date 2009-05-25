using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageManipulation;
using System.IO;
using Tadmap.DataAccess;
using Tadmap.Model.Image;
using System.Drawing;
using Affirma.ThreeSharp;
using System.Globalization;

namespace Tadmap.Model.Image
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
         using (System.Drawing.Bitmap oImage = Bitmap.FromStream(stream, true, true) as Bitmap)
         {
            using (System.Drawing.Image oSquare = ImageManipulator.ResizeAndCrop(oImage, 80, 80))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oSquare.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Square, "image/jpeg");
               }
            }

            using (System.Drawing.Image oThumb = ImageManipulator.FitToRectangle(oImage, 100, 100))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Thumb, "image/jpeg");
               }
            }

            using (System.Drawing.Image oLargeThumb = ImageManipulator.FitToRectangle(oImage, 200, 200))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oLargeThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, LargeThumb, "image/jpeg");
               }
            }

            using (System.Drawing.Image oLargeThumb = ImageManipulator.FitToRectangle(oImage, 560, 560))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oLargeThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  binaryRepository.Add(oMemoryStream, Preview, "image/jpeg");
               }
            }

            int zoomLevels;
            int tileSize;

            CreateTileSet(binaryRepository, oImage, Key, out zoomLevels, out tileSize);
         }



         stream.Position = 0;
      }

      #endregion

      public void CreateTileSet(
         IBinaryRepository binaryRepository,
         System.Drawing.Bitmap image,
         string key,
         out int zoomLevels,
         out int tileSize
      )
      {

         double iZoomLevel = 0;
         double iMaxDimension = Math.Max(image.Width, image.Height);
         int iTileSize = Convert.ToInt32(iMaxDimension);

         while (iTileSize > 359)
         {
            iZoomLevel++;
            iTileSize = Convert.ToInt32(iMaxDimension / Math.Pow(2, iZoomLevel));
         }

         zoomLevels = Convert.ToInt32(iZoomLevel);
         tileSize = iTileSize;

         int iImageSize = Convert.ToInt32(iMaxDimension);
         Bitmap oBaseImage = ImageManipulator.CenterImage(image, iImageSize, iImageSize);

         while (iZoomLevel >= 0)
         {
            iImageSize = Convert.ToInt32(iTileSize * Math.Pow(2, iZoomLevel));
            using (Bitmap oZoomedImage = ImageManipulator.Resize(oBaseImage, iImageSize, iImageSize))
            {
               List<List<Bitmap>> oTiles = ImageManipulator.CreateTiles(oZoomedImage, iTileSize);

               for (int i = 0; i < oTiles.Count; i++)
               {
                  for (int j = 0; j < oTiles[i].Count; j++)
                  {
                     bool bUploaded = false;
                     int tryCount = 0;

                     while (bUploaded == false && tryCount < 3)
                     {
                        try
                        {
                           using (MemoryStream oStream = new MemoryStream())
                           {

                              oTiles[i][j].Save(oStream, System.Drawing.Imaging.ImageFormat.Png);
                              oStream.Position = 0;
                              binaryRepository.Add(
                                 oStream,
                                 string.Format(CultureInfo.InvariantCulture, "Tile_{0}_{1}_{2}_{3}", j, i, iZoomLevel, key),
                                 "image.jpeg"
                              );

                              bUploaded = true;
                           }
                        }
                        catch (BinaryRepositoryException oException)
                        {
                           tryCount++;

                           if (tryCount > 3)
                           {
                              throw new BinaryRepositoryException("Failed to add tile on third attempt.", oException);
                           }
                        }
                     }
                  }
               }
            }

            iZoomLevel--;
         }
      }
   }
}
