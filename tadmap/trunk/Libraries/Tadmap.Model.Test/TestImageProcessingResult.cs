using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Image;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace Tadmap.Model.Test
{
   [TestFixture]
   public class TestImageProcessingResult
   {
      [Test]
      public void ContainsCorrectDataAfterSerializeDeserialize()
      {
         var source = new ImageProcessingResult
         {
            Key = "key",
            Result = ImageProcessingResult.ResultType.IconReady,
            TileSize = 10,
            ZoomLevel = 20
         };

         var serializer = new XmlSerializer(typeof(ImageProcessingResult));
         var builder = new StringBuilder();
   
         using (var writer = new StringWriter(builder))
            serializer.Serialize(writer, source);

         var actual = (ImageProcessingResult)serializer.Deserialize(new StringReader(builder.ToString()));

         Assert.AreEqual(source.Key, actual.Key);
         Assert.AreEqual(source.Result, actual.Result);
         Assert.AreEqual(source.TileSize, actual.TileSize);
         Assert.AreEqual(source.ZoomLevel, actual.ZoomLevel);
      }
   }
}
