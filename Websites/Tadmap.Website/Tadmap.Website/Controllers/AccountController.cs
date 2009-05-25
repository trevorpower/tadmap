using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using DotNetOpenId.RelyingParty;
using DotNetOpenId;
using System.Data.SqlClient;
using System.Data;
using Tadmap.Tadmap.Security;
using Tadmap.Configuration;
using Tadmap.Security;
using Tadmap.Model;
using Tadmap.Website;
using Tadmap.Model.User;

namespace Tadmap.Controllers
{
   [HandleError]
   [OutputCache(Location = OutputCacheLocation.None)]
   public class AccountController : Controller
   {
      IUserRepository UserRepository { get; set; }

      public AccountController(IUserRepository userRepository)
      {
         UserRepository = userRepository;
      }

      public ActionResult Login()
      {
         return View();
      }

      public ActionResult OpenIdReturn()
      {
         OpenIdRelyingParty rp = new OpenIdRelyingParty();

         string error = string.Empty;

         if (rp.Response != null)
         {
            switch (rp.Response.Status)
            {
               case AuthenticationStatus.Authenticated:
                  {
                     return AuthenticateUser(rp.Response);
                  }
               case AuthenticationStatus.Canceled:
                  {
                     error = "Authentication canceled.";
                     break;
                  }
               case AuthenticationStatus.Failed:
                  {
                     error = "Authentication failed.";

                     if (rp.Response.Exception != null)
                        error += " (" + rp.Response.Exception.Message + ")";

                     break;
                  }
               case AuthenticationStatus.SetupRequired:
                  {
                     error = "Authentication failed. (Setup required?)";
                     break;
                  }
            }
         }

         ViewData["LoginErrorMessage"] = error;

         return View("Login");
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Login(string openid_url)
      {
         if (Identifier.IsValid(openid_url))
         {
            try
            {
               Identifier openId = Identifier.Parse(openid_url);

               OpenIdRelyingParty rp = new OpenIdRelyingParty();
               IAuthenticationRequest request = rp.CreateRequest(openId, new Realm(OpenId.Realm), new Uri(OpenId.LoginUrl));
               request.RedirectToProvider();
            }
            catch (DotNetOpenId.OpenIdException exception)
            {
               ViewData["LoginErrorMessage"] = exception.Message;
               ModelState.AddModelError("LoginError", exception);
            }
         }
         else
         {
            ViewData["LoginErrorMessage"] = "The OpenID you provided is not in the correct format.";
            ModelState.AddModelError("LoginError", "The OpenID you provided is not in the correct format.");
         }

         // if we got here then something went wrong so we go back to the login view.
         return View();
      }

      public ActionResult Logout()
      {
         FormsAuthentication.SignOut();
    
         return RedirectToAction("Index", "Home");
      }

      protected override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (filterContext.HttpContext.User.Identity is WindowsIdentity)
         {
            throw new InvalidOperationException("Windows authentication is not supported.");
         }
      }

      /// <summary>
      /// This authenticates a user based on a response from an openid provider.
      /// </summary>
      private ActionResult AuthenticateUser(IAuthenticationResponse response)
      {
         if (response.Status != AuthenticationStatus.Authenticated)
            throw new ArgumentException("The response status must be 'Authenticated'. (" + response.Status.ToString() + ")", "response");

         var user = UserRepository.GetAllUsers().Where(u => u.OpenIds.Contains(response.ClaimedIdentifier.ToString())).SingleOrDefault();

         if (user == null)
         {
            user = new Model.User.User
            {
               Name = response.ClaimedIdentifier.ToString(),
               OpenIds = new List<string>{ response.ClaimedIdentifier.ToString() },
               Roles = new List<string>{ TadmapRoles.Collector }
            };

            UserRepository.Save(user);
         }

         CookieUserData userData = new CookieUserData(
            user.Id,
            response.FriendlyIdentifierForDisplay.ToString(),
            user.Roles.ToArray()
         );
         
         FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
           1,
           response.ClaimedIdentifier.ToString(),
           DateTime.Now,
           DateTime.Now.AddHours(5),
           false,
           userData.ToString()
         );

         // Encrypt the ticket.
         string encTicket = FormsAuthentication.Encrypt(ticket);

         // Create the cookie.
         Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

         return RedirectToAction("Index", new { controller = "Home" });
      }

      //private int CreateNewUser(string openIdUrl)
      //{
      //   //TadmapDb db = new TadmapDb(Database.TadmapConnection);

      //   //User newUser = new User();
      //   //newUser.Name = string.Empty;

      //   //UserRole newUserRole = new UserRole();
      //   //newUserRole.UserId = newUser.Id;
      //   //newUserRole.Role = TadmapRoles.Collector;

      //   //UserOpenId newOpenId = new UserOpenId();
      //   //newOpenId.UserId = newUser.Id;
      //   //newOpenId.OpenIdUrl = openIdUrl;

      //   //db.Users.InsertOnSubmit(newUser);
      //   //db.UserRoles.InsertOnSubmit(newUserRole);
      //   //db.UserOpenIds.InsertOnSubmit(newOpenId);

      //   //db.SubmitChanges();

      //   //return newUser.Id;
      //}
   }
}
