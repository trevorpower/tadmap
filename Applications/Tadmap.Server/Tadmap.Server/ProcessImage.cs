using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;
using System.Diagnostics;
using System.IO;

namespace Tadmap.Server
{
   public class ProcessImage
   {
      static public ImageProcessingResult Process(string imageName, IBinaryRepository binaryRepository)
      {
         var log = new EventLog("Application")
         {
            Source = "Tadmap"
         };

         log.WriteEntry("Processing image:" + imageName, EventLogEntryType.Information);

         Stream binary = binaryRepository.GetBinary(imageName);

         if (binary == null)
         {
            log.WriteEntry("No binary found:" + imageName, EventLogEntryType.Warning);
            return new ImageProcessingResult { Key = imageName, Result = ImageProcessingResult.ResultType.Failed }; // Image not available in the queue yet.
         }

         // If I have an image I should renew the message.

         IImageSet imageSet = new ImageSet1(imageName);

         int zoomLevels;
         int tileSize;
         imageSet.Create(binary, binaryRepository, out zoomLevels, out tileSize);
         log.WriteEntry("Processing finished.", EventLogEntryType.Information);

         return new ImageProcessingResult
         {
            Key = imageName,
            Result = ImageProcessingResult.ResultType.Complete,
            ZoomLevel = zoomLevels,
            TileSize = tileSize
         };
      }
   }
}
