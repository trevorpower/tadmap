using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tadmap.Model.Image;

namespace Tadmap.Views.Image
{
   public partial class PrivacyControl : System.Web.Mvc.ViewUserControl<ImageView>
   {
      protected static readonly string IsPublicDescription = "<b>" + ViewResources.Anyone + "</b> " + ViewResources.PrivacyStatusMessageSuffix;
      protected static readonly string IsPrivateDescription = "<b>" + ViewResources.OnlyYou + "</b> " + ViewResources.PrivacyStatusMessageSuffix;
   }
}
