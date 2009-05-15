using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenId.Provider;
using DotNetOpenId;
using DotNetOpenId.Extensions.SimpleRegistration;

namespace Tadmap.Website.OpenIdStub
{
   public class Util
   {
      public static string ExtractUserName(Uri url)
      {
         return url.Segments[url.Segments.Length - 1];
      }
      public static string ExtractUserName(Identifier identifier)
      {
         return ExtractUserName(new Uri(identifier.ToString()));
      }
      public static Identifier BuildIdentityUrl()
      {
         string username = HttpContext.Current.User.Identity.Name;
         // be sure to normalize case the way the user's identity page does.
         username = username.Substring(0, 1).ToUpperInvariant() + username.Substring(1).ToLowerInvariant();
         return new Uri(HttpContext.Current.Request.Url, "/user/" + username);
      }

      public static void ProcessAuthenticationChallenge(IAuthenticationRequest idrequest)
      {
         var sregRequest = ProviderEndpoint.PendingAuthenticationRequest.GetExtension<ClaimsRequest>();
         ClaimsResponse sregResponse = null;
         if (sregRequest != null)
         {
            //sregResponse = profileFields.GetOpenIdProfileFields(sregRequest);
         }
         ProviderEndpoint.PendingAuthenticationRequest.IsAuthenticated = true;
         if (sregResponse != null)
         {
            ProviderEndpoint.PendingAuthenticationRequest.AddResponseExtension(sregResponse);
         }
         //Debug.Assert(ProviderEndpoint.PendingAuthenticationRequest.IsResponseReady);
         ProviderEndpoint.PendingAuthenticationRequest.Response.Send();
         ProviderEndpoint.PendingAuthenticationRequest = null;


         //if (idrequest.Immediate)
         //{
         //   if (idrequest.IsDirectedIdentity)
         //   {
         //      idrequest.IsAuthenticated = true;
         //      //if (HttpContext.Current.User.Identity.IsAuthenticated)
         //      //{
         //      //   idrequest.LocalIdentifier = Util.BuildIdentityUrl();
         //      //   idrequest.IsAuthenticated = true;
         //      //}
         //      //else
         //      //{
         //      //   idrequest.IsAuthenticated = false;
         //      //}
         //   }
         //   else
         //   {
         //      //string userOwningOpenIdUrl = Util.ExtractUserName(idrequest.LocalIdentifier);
         //      // NOTE: in a production provider site, you may want to only 
         //      // respond affirmatively if the user has already authorized this consumer
         //      // to know the answer.
         //      //idrequest.IsAuthenticated = userOwningOpenIdUrl == HttpContext.Current.User.Identity.Name;
         //   }
         //}
         //else
         //{
         //   //HttpContext.Current.Response.Redirect("~/decide.aspx", true);
         //}
      }
   }
}
