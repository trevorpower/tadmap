using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using ImageManipulation;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Security.Principal;
using System.Security;
using Affirma.ThreeSharp.Wrapper;
using TadMap.Configuration;
using TadMap.Security;

namespace TadMap
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
      /// Creates a new image based on a stream and storage key.
      /// </summary>
      /// 
      /// <param name="identity">The identity of the user doing the add.</param>
      /// <param name="oStream">The stream containing the content of the master image.</param>
      /// <param name="strKey">
      /// The storage key to be used for storeing the master image and to be used as the base when forming
      /// derived images.
      /// </param>
      public static TadImage NewTadImage(Stream oStream, string strStorageKey)
      {
         TadImage newImage = new TadImage();
         
         newImage.mId = Guid.NewGuid();
         newImage.mStorageKey = strStorageKey;
         newImage.UploadBaseImages(oStream);

         return newImage;
      }

      private void UploadBaseImages(Stream oStream)
      {
         ThreeSharpConfig config;
         IThreeSharp service;

         config = new ThreeSharpConfig();
         config.AwsAccessKeyID = S3Storage.AccessKey;
         config.AwsSecretAccessKey = S3Storage.SecretAccessKey;
         config.ConnectionLimit = 40;
         config.IsSecure = true;

         // It is necessary to use the SUBDOMAIN CallingFormat for accessing EU buckets
         config.Format = CallingFormat.SUBDOMAIN;

         service = new ThreeSharpQuery(config);

         string strSquareKey = "Square_" + mStorageKey;
         string strThumbKey = "Thumb_" + mStorageKey;
         string strLargeThumbKey = "LargeThumb_" + mStorageKey;

         using (System.Drawing.Image oImage = Bitmap.FromStream(oStream, true, true))
         {
            using (System.Drawing.Image oSquare = ImageManipulator.ResizeAndCrop(oImage as Bitmap, 80, 80))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oSquare.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  UploadImage(service, oMemoryStream, strSquareKey, "image/jpeg");
               }
            }

            using (System.Drawing.Image oThumb = ImageManipulator.FitToRectangle(oImage as Bitmap, 100, 100))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  UploadImage(service, oMemoryStream, strThumbKey, "image/jpeg");
               }
            }

            using (System.Drawing.Image oLargeThumb = ImageManipulator.FitToRectangle(oImage as Bitmap, 200, 200))
            {
               using (MemoryStream oMemoryStream = new MemoryStream())
               {
                  oLargeThumb.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                  oMemoryStream.Position = 0;
                  UploadImage(service, oMemoryStream, strLargeThumbKey, "image/jpeg");
               }
            }
         }

         oStream.Position = 0;
         UploadImage(service, oStream, mStorageKey, ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(mStorageKey)));
      }

      private void UploadImage(IThreeSharp service, Stream oStream, string strKey, string contentType)
      {
         using (ObjectAddRequest objectAddRequest = new ObjectAddRequest(S3Storage.BucketName, strKey))
         {
            byte[] buffer = new byte[oStream.Length];

            oStream.Read(buffer, 0, (int)oStream.Length);

            objectAddRequest.LoadStreamWithBytes(buffer, contentType);

            using (ObjectAddResponse objectAddResponse = service.ObjectAdd(objectAddRequest))
            { } 
         }


         //// The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
         //// minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to upload
         //// the file to the redirect URL
         //String redirectUrl = null;
         //using (BucketListRequest testRequest = new BucketListRequest(S3Storage.BucketName))
         //{
         //   testRequest.Method = "HEAD";
         //   using (BucketListResponse testResponse = service.BucketList(testRequest))
         //   {
         //      if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
         //      {
         //         redirectUrl = testResponse.Headers["Location"].ToString() + strKey;
         //      }
         //   }
         //}

         //using (ObjectAddRequest request = new ObjectAddRequest(S3Storage.BucketName, strKey))
         //{
         //   request.Headers.Add("x-amz-acl", "public-read-write");
         //   //request.ContentType = ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(strKey));
         //   request.DataStream = oStream;
         //   request.BytesTotal = (request.DataStream).Length;
         //   //request.Timeout = iTimeout;

         //   if (redirectUrl != null)
         //   {
         //      request.RedirectUrl = redirectUrl;
         //   }

         //   using (ObjectAddResponse response = service.ObjectAdd(request))
         //   { }
         //}
      }


      internal void Insert(TadImage oMap)
      {
         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "AddTadImage";
               cm.Parameters.AddWithValue("@MapId", oMap.mId);
               cm.Parameters.AddWithValue("@Key", mStorageKey);
               DoInsertUpdate(cm);
            }
         }
      }

      internal void Update(TadImage oMap)
      {
         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "UpdatePicture";
               cm.Parameters.AddWithValue("@ZoomLevels", mZoomLevels);
               cm.Parameters.AddWithValue("@TileSize", mTileSize);
               DoInsertUpdate(cm);
            }
         }
      }

      private void DoInsertUpdate(SqlCommand cm)
      {
         cm.CommandType = CommandType.StoredProcedure;
         cm.Parameters.AddWithValue("@Id", mId);
         cm.Parameters.AddWithValue("@Description", mDescription);

         cm.ExecuteNonQuery();
      }

      internal static TadImage GetPicture(SqlDataReader oDataReader)
      {
         return new TadImage(oDataReader);
      }

      private TadImage()
      {
      }

      internal TadImage(SqlDataReader oDataReader)
      {
         Fetch(oDataReader);
      }

      // called to load data from the database
      private void Fetch(SqlDataReader oDataReader)
      {
         mId = (Guid)oDataReader["Id"];
         mTitle = (string)oDataReader["Title"];
         mDescription = (string)oDataReader["Description"];
         mStorageKey = (string)oDataReader["Key"];
         mZoomLevels = oDataReader["ZoomLevels"] == DBNull.Value ? 0 : Convert.ToInt32(oDataReader["ZoomLevels"]);
         mTileSize = oDataReader["TileSize"] == DBNull.Value ? 0 : Convert.ToInt32(oDataReader["TileSize"]);
         mHasTileSet = oDataReader["TileSize"] != DBNull.Value && oDataReader["ZoomLevels"] != DBNull.Value;
      }

      public string GetLargeThumbUrl()
      {
         ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
         return s3.GetUrl(S3Storage.BucketName, "LargeThumb_" + StorageKey);
      }

      public string GetOriginalUrl()
      {
         ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
         return s3.GetUrl(S3Storage.BucketName, StorageKey);
      }

      public void CreateTileset()
      {
         HttpWebRequest oWebRequest = (HttpWebRequest)WebRequest.Create(GetOriginalUrl());
         HttpWebResponse oWebResponse = (HttpWebResponse)oWebRequest.GetResponse();
         Stream oWebStream = oWebResponse.GetResponseStream();

         Image oImage = Bitmap.FromStream(oWebStream);


         ThreeSharpConfig config;
         IThreeSharp service;

         config = new ThreeSharpConfig();
         config.AwsAccessKeyID = S3Storage.AccessKey;
         config.AwsSecretAccessKey = S3Storage.SecretAccessKey;
         config.ConnectionLimit = 40;
         config.IsSecure = true;

         // It is necessary to use the SUBDOMAIN CallingFormat for accessing EU buckets
         config.Format = CallingFormat.SUBDOMAIN;

         service = new ThreeSharpQuery(config);


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
                              UploadImage(
                                  service,
                                  oStream,
                                  string.Format("Tile_{0}_{1}_{2}_{3}", j, i, iZoomLevel, mStorageKey),
                                  "image/png"
                              );

                              bUploaded = true;
                           }
                        }
                        catch (ThreeSharpException oException)
                        {
                           tryCount++;

                           if (tryCount > 3)
                           {
                              throw new Exception("Failed to upload tile on third attempt.", oException);
                           }
                        }
                     }
                  }
               }
            }

            iZoomLevel--;
         }
      }

      public void Save(IIdentity identity)
      {
         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "AddImage";

               try
               {
                  cm.CommandType = CommandType.StoredProcedure;

                  cm.Parameters.AddWithValue("@OpenIdUrl", identity.Name);
                  cm.Parameters.AddWithValue("@Id", Id);
                  cm.Parameters.AddWithValue("@Title", Title);
                  cm.Parameters.AddWithValue("@Description", Description);
                  cm.Parameters.AddWithValue("@StorageKey", StorageKey);
                  cm.Parameters.AddWithValue("@ZoomLevels", HasTileSet ? (object)ZoomLevels : DBNull.Value);
                  cm.Parameters.AddWithValue("@TileSize", HasTileSet ? (object)TileSize : DBNull.Value);

                  cm.ExecuteNonQuery();
               }
               catch (SqlException oException)
               {
                  throw;
               }
            }
         }
      }

      public static TadImage Get(Guid imageId, IPrincipal principal)
      {
         if (!principal.Identity.IsAuthenticated)
            throw new SecurityException("User not authorized to view an image.");

         if (principal.IsInRole(TadMapRoles.Collector) || principal.IsInRole(TadMapRoles.Administrator))
         {
            using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
            {
               cn.Open();

               using (SqlCommand cm = cn.CreateCommand())
               {
                  cm.CommandType = CommandType.StoredProcedure;
                  cm.CommandText = "GetImage";
                  cm.Parameters.AddWithValue("@ImageId", imageId);
                  using (SqlDataReader dr = cm.ExecuteReader())
                  {
                     if (dr.Read())
                     {
                        return new TadImage(dr);
                     }
                     else
                     {
                        throw new Exception("Image not found");
                     }
                  }
               }
            }
         }
         else
         {
            throw new SecurityException("User not authoriazed to view an image.");
         }
      }

      internal static int UpdateTitle(Guid id, string title, IPrincipal iPrincipal)
      {
         if (!iPrincipal.Identity.IsAuthenticated)
            throw new SecurityException("User must be authenticated before changing image title");

         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "UpdateImageTitle";

               try
               {
                  cm.CommandType = CommandType.StoredProcedure;

                  cm.Parameters.AddWithValue("@openIdUrl", iPrincipal.Identity.Name);
                  cm.Parameters.AddWithValue("@id", id);
                  cm.Parameters.AddWithValue("@title", title);

                  return cm.ExecuteNonQuery();
               }
               catch (SqlException oException)
               {
                  throw;
               }
            }
         }
      }

      internal static int UpdateDescription(Guid id, string description, IPrincipal principal)
      {
         if (!principal.Identity.IsAuthenticated)
            throw new SecurityException("User must be authenticated before changing image description");

         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "UpdateImageDescription";

               try
               {
                  cm.CommandType = CommandType.StoredProcedure;

                  cm.Parameters.AddWithValue("@openIdUrl", principal.Identity.Name);
                  cm.Parameters.AddWithValue("@id", id);
                  cm.Parameters.AddWithValue("@description", description);

                  return cm.ExecuteNonQuery();
               }
               catch (SqlException oException)
               {
                  throw;
               }
            }
         }
      }
   }
}
