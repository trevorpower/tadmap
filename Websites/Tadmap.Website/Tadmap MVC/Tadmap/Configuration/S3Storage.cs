using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace Tadmap.Configuration
{
   public static class S3Storage
   {
      public static string AccessKey
      {
         get
         {
            return ConfigurationSettings.AppSettings["S3AccessKey"];
         }
      }

      public static string SecretAccessKey
      {
         get
         {
            return ConfigurationSettings.AppSettings["S3SecretAccessKey"];
         }
      }

      public static string BucketName
      {
         get
         {
            return ConfigurationSettings.AppSettings["S3BucketName"];
         }
      }

      public static int LargeFileTimeout
      {
         get
         {
            return int.Parse(ConfigurationSettings.AppSettings["S3LargeFileTimeout"], CultureInfo.InvariantCulture);
         }
      }

      public static int SmallFileTimeout
      {
         get
         {
            return int.Parse(ConfigurationSettings.AppSettings["S3SmallFileTimeout"], CultureInfo.InvariantCulture);
         }
      }

   }
}
