using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.Models.Images;

namespace TadmapTests.DataAccess
{
   internal class TestImageRepository : IImageRepository
   {
      List<TadmapImage> _images;

      public TestImageRepository()
      {
         _images = new List<TadmapImage>();

         for (int i = 0; i < 10; i++)
            _images.Add(new TadmapImage { Id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf" + i), Description = "description", Key = "Key", Title = "Title " + i, IsPublic = i < 5 });

         _images[0].IsOffensive = true;
         _images[9].IsOffensive = true;
      }
      
      #region IImageRepository Members

      public IQueryable<Tadmap_MVC.Models.Images.TadmapImage> GetAllImages()
      {
         return _images.AsQueryable();
      }

      public void MarkAsOffensive(Guid id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);
         
         if (image == null)
            throw new ImageNotFound();
         
         image.IsOffensive = true;
      }

      public void MarkAsUnOffensive(Guid id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFound();

         image.IsOffensive = false;
      }

      public void MarkAsPublic(Guid id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFound();

         image.IsPublic = true;
      }

      public void MarkAsPrivate(Guid id)
      {
         TadmapImage image = _images.Find(i => i.Id == id);

         if (image == null)
            throw new ImageNotFound();

         image.IsPublic = false;
      }
      #endregion

      #region IImageRepository Members


      public void Save(TadmapImage image)
      {
         
      }

      #endregion
   }
}
