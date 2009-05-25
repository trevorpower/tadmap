using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Tadmap.DataAccess;
using Tadmap.Model;

namespace Tadmap.Model.Image
{
   public class TadmapImage
   {
      IBinaryRepository _binaryRepository;

      public TadmapImage(IBinaryRepository binaryRepository)
      {
         _binaryRepository = binaryRepository;
      }

      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string Key { get; set; }
      public bool IsPublic { get; set; }
      public bool IsOffensive { get; set; }
      public int UserId { get; set; }
      public string OwnerName { get; set; }
      public int ImageSetVersion { get; set; }

      public Uri SquareUrl { get { return _binaryRepository.GetUrl(ImageSet.Square); } }

      public IImageSet ImageSet { get; set; }

      public static bool CanUserMarkAsOffensive(IPrincipal principal)
      {
         return principal.Identity.IsAuthenticated && principal.IsInRole(TadmapRoles.Administrator);
      }
   }
}
