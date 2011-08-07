using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;
using NUnit.Framework;

namespace Tadmap.Model.Test
{
   public abstract class ImageRepositoryTestsBase
   {
      protected abstract IImageRepository CreateImageRepository();

      [Test]
      public void Can_Save_Image()
      {
         IImageRepository repository = CreateImageRepository();

         var image = new TadmapImage()
         {
            Key = "key",
            TileSize = 10,
            ZoomLevel = 20
         };

         repository.Save(image);

         var savedImage = repository.GetAllImages().Where(i => i.Key == image.Key).Single();

         Assert.AreEqual(image.Key, savedImage.Key);
         Assert.AreEqual(image.TileSize, savedImage.TileSize);
         Assert.AreEqual(image.ZoomLevel, savedImage.ZoomLevel);
         Assert.AreEqual(image.HasIcon, savedImage.HasIcon);
      }
   }
}
