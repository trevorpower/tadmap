using System;
using System.Collections.Generic;
using System.Linq;
using Tadmap.Model.Image;

namespace Tadmap.Mode.Test.Mock
{
   public sealed class ImageRepository : IImageRepository
   {
      List<TadmapImage> _images;

      public ImageRepository()
      {
         _images = new List<TadmapImage>();

         for (int i = 0; i < 10; i++)
            _images.Add(
               new TadmapImage
               {
                  Id = i,
                  Description = "description",
                  Key = "Key",
                  Title = "Title " + i,
                  IsPublic = i < 5,
                  OwnerName = "the owner",
                  ImageSet = new ImageSet1("Key")
               }
            );

         _images[0].IsOffensive = true;
         _images[9].IsOffensive = true;
      }
      
      #region IImageRepository Members

      public IQueryable<TadmapImage> GetAllImages()
      {
         return _images.AsQueryable();
      }

      public void MarkAsOffensive(int id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);
         
         if (image == null)
            throw new ImageNotFoundException();
         
         image.IsOffensive = true;
      }

      public void MarkAsUnOffensive(int id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = false;
      }

      public void MarkAsPublic(int id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = true;
      }

      public void MarkAsPrivate(int id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = false;
      }
      #endregion

      #region IImageRepository Members


      public void Save(TadmapImage image)
      {
         _images.Add(image);
      }

      #endregion
   }
}
