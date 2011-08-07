using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tadmap.Model.Image
{
   [Serializable]
   public class ImageProcessingResult : ISerializable
   {
      public enum ResultType { Failed = 0, IconReady = 1, Complete = 2};

      public string Key { get; set; }
      public ResultType Result { get; set; }
      public int ZoomLevel { get; set; }
      public int TileSize { get; set; }

      public ImageProcessingResult()
      {
      }

      public ImageProcessingResult(SerializationInfo info, StreamingContext context)
      {
         Key = info.GetString("Key");
         Result = (ResultType)info.GetInt32("Result");
         ZoomLevel = info.GetInt32("ZoomLevel");
         TileSize = info.GetInt32("TileSize");
      }

      #region ISerializable Members

      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("Key", Key);
         info.AddValue("Result", (int)Result);
         info.AddValue("ZoomLevel", ZoomLevel);
         info.AddValue("TileSize", TileSize);
      }

      #endregion


   }
}
