using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.SQL;
using Tadmap_MVC.DataAccess.S3;
using Tadmap_MVC.Models.ImageSets;
using Tadmap.Security;

namespace Tadmap_MVC.Models.Images
{
   public class TadmapImage
   {
      IImageRepository _imageRepository;
      IBinaryRepository _binaryRepository;

      public TadmapImage()
      {
         _imageRepository = new SqlImageRepository();
         _binaryRepository = new S3BinaryRepository();
      }

      public TadmapImage(
         Guid id,
         string title,
         string description,
         string key,
         bool isPublic,
         bool isOffensive,
         Guid userId,
         IImageRepository imageRepository,
         IBinaryRepository binaryRepository
      )
      {
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;

         Id = id;
         Title = title;
         Description = description;
         Key = key;
         IsPublic = isPublic;
         IsOffensive = isOffensive;
         UserId = userId;
      }

      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string Key { get; set; }
      public bool IsPublic { get; set; }
      public bool IsOffensive { get; set; }
      public Guid UserId { get; set; }
      public string OwnerName { get; set; }

      public IImageSet ImageSet { get; set; }

      public bool CanUserMarkAsOffensive(IPrincipal principal)
      {
         return principal.Identity.IsAuthenticated && principal.IsInRole(TadmapRoles.Administrator);
      }
   }
}
