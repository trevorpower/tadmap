using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using ImageManipulation;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Security.Principal;
using System.Security;
using Tadmap.Model.Image;
using Tadmap.Model;
using System.Globalization;

namespace Tadmap.Model
{
   public class TadImage
   {
      Guid mId;

      string mTitle;
      string mDescription;

      string mStorageKey;

      int mZoomLevels;
      int mTileSize;

      bool mHasTileSet;

      int _imageSet;

      public Guid Id
      {
         get { return mId; }
         set { mId = value; }
      }

      /// <summary>
      /// The title of this image.
      /// </summary>
      public string Title
      {
         get { return mTitle; }
         set { mTitle = value; }
      }

      /// <summary>
      /// The description associated with the image.
      /// </summary>
      public string Description
      {
         get { return mDescription; }
         set { mDescription = value; }
      }

      /// <summary>
      /// The storage key of the base image, also the base key of other versions/parts of the same image.
      /// </summary>
      public string StorageKey
      {
         get { return mStorageKey; }
         set { mStorageKey = value; }
      }

      /// <summary>
      /// The number of zoom levels that this image has been divided into.
      /// </summary>
      public int ZoomLevels
      {
         get { return mZoomLevels; }
         set { mZoomLevels = value; }
      }

      /// <summary>
      /// The size of the tiles that this image has been divided into.
      /// </summary>
      public int TileSize
      {
         get { return mTileSize; }
         set { mTileSize = value; }
      }

      /// <summary>
      /// Indicates if a tile set has been generated for this image.
      /// </summary>
      public bool HasTileSet
      {
         get { return mHasTileSet; }
         set { mHasTileSet = value; }
      }

      /// <summary>
      /// The version of the imageset created for this image.
      /// </summary>
      public int ImageSet
      {
         get { return _imageSet; }
         set { _imageSet = value; }
      }

      private TadImage()
      {
      }

      public void CreateTileSet(IBinaryRepository binaryRepository)
      {
         System.Drawing.Image oImage = Bitmap.FromStream(binaryRepository.GetBinary(StorageKey));

         double iZoomLevel = 0;
         double iMaxDimension = Math.Max(oImage.Width, oImage.Height);
         int iTileSize = Convert.ToInt32(iMaxDimension);

         while (iTileSize > 359)
         {
            iZoomLevel++;
            iTileSize = Convert.ToInt32(iMaxDimension / Math.Pow(2, iZoomLevel));
         }

         ZoomLevels = Convert.ToInt32(iZoomLevel);
         TileSize = iTileSize;

         int iImageSize = Convert.ToInt32(iMaxDimension);
         Bitmap oBaseImage = ImageManipulator.CenterImage(oImage as Bitmap, iImageSize, iImageSize);

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
                                 string.Format(CultureInfo.InvariantCulture, "Tile_{0}_{1}_{2}_{3}", j, i, iZoomLevel, mStorageKey),
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
