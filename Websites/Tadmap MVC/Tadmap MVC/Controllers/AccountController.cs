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
using TadMap.Configuration;
using System.Data;
using TadMap.Security;
using Tadmap_MVC.Tadmap.Security;

namespace Tadmap_MVC.Controllers
{

   [HandleError]
   [OutputCache(Location = OutputCacheLocation.None)]
   public class AccountController : Controller
   {


      // This constructor is used by the MVC framework to instantiate the controller using
      // the default forms authentication and membership providers.

      public AccountController()
         : this(null, null)
      {
      }

      // This constructor is not used by the MVC framework but is instead provided for ease
      // of unit testing this type. See the comments at the end of this file for more
      // information.

      public AccountController(IFormsAuthentication formsAuth, MembershipProvider provider)
      {
         FormsAuth = formsAuth ?? new FormsAuthenticationWrapper();
         Provider = provider ?? Membership.Provider;
      }

      public IFormsAuthentication FormsAuth
      {
         get;
         private set;
      }

      public MembershipProvider Provider
      {
         get;
         private set;
      }

      [Authorize]
      public ActionResult ChangePassword()
      {

         ViewData["Title"] = "Change Password";
         ViewData["PasswordLength"] = Provider.MinRequiredPasswordLength;

         return View();
      }

      [Authorize]
      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
      {

         ViewData["Title"] = "Change Password";
         ViewData["PasswordLength"] = Provider.MinRequiredPasswordLength;

         // Basic parameter validation
         if (String.IsNullOrEmpty(currentPassword))
         {
            ModelState.AddModelError("currentPassword", "You must specify a current password.");
         }
         if (newPassword == null || newPassword.Length < Provider.MinRequiredPasswordLength)
         {
            ModelState.AddModelError("newPassword",
                String.Format(CultureInfo.CurrentCulture,
                     "You must specify a new password of {0} or more characters.",
                     Provider.MinRequiredPasswordLength));
         }
         if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
         {
            ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
         }

         if (ModelState.IsValid)
         {
            // Attempt to change password
            MembershipUser currentUser = Provider.GetUser(User.Identity.Name, true /* userIsOnline */);
            bool changeSuccessful = false;
            try
            {
               changeSuccessful = currentUser.ChangePassword(currentPassword, newPassword);
            }
            catch
            {
               // An exception is thrown if the new password does not meet the provider's requirements
            }

            if (changeSuccessful)
            {
               return RedirectToAction("ChangePasswordSuccess");
            }
            else
            {
               ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
            }
         }

         // If we got this far, something failed, redisplay form
         return View();
      }

      public ActionResult ChangePasswordSuccess()
      {

         ViewData["Title"] = "Change Password";

         return View();
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
         //FormsAuth.SetAuthCookie(openid_url, false /* createPersistentCookie */);
         //return RedirectToAction("Index", "Home");

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
               ModelState.AddModelError("OpenIdException", exception);
            }
            catch (Exception exception)
            {
               ModelState.AddModelError("Authentication", exception);
            }
         }
         else
         {
            ModelState.AddModelError("openid_url", "The OpenID you provided is not in the correct format.");
         }

         // if we got here then something went wrong so we go back to the login view.
         return View();
      }

      public ActionResult Logout()
      {
         FormsAuth.SignOut();

         return RedirectToAction("Index", "Home");
      }

      protected override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (filterContext.HttpContext.User.Identity is WindowsIdentity)
         {
            throw new InvalidOperationException("Windows authentication is not supported.");
         }
      }

      public ActionResult Register()
      {

         ViewData["Title"] = "Register";
         ViewData["PasswordLength"] = Provider.MinRequiredPasswordLength;

         return View();
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Register(string username, string email, string password, string confirmPassword)
      {

         ViewData["Title"] = "Register";
         ViewData["PasswordLength"] = Provider.MinRequiredPasswordLength;

         // Basic parameter validation
         if (String.IsNullOrEmpty(username))
         {
            ModelState.AddModelError("username", "You must specify a username.");
         }
         if (String.IsNullOrEmpty(email))
         {
            ModelState.AddModelError("email", "You must specify an email address.");
         }
         if (password == null || password.Length < Provider.MinRequiredPasswordLength)
         {
            ModelState.AddModelError("password",
                String.Format(CultureInfo.CurrentCulture,
                     "You must specify a password of {0} or more characters.",
                     Provider.MinRequiredPasswordLength));
         }
         if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
         {
            ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
         }

         if (ViewData.ModelState.IsValid)
         {
            // Attempt to register the user
            MembershipCreateStatus createStatus;
            MembershipUser newUser = Provider.CreateUser(username, password, email, null, null, true, null, out createStatus);

            if (newUser != null)
            {
               FormsAuth.SetAuthCookie(username, false /* createPersistentCookie */);
               return RedirectToAction("Index", "Home");
            }
            else
            {
               ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
            }
         }

         // If we got this far, something failed, redisplay form
         return View();
      }

      private static string ErrorCodeToString(MembershipCreateStatus createStatus)
      {
         // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
         // a full list of status codes.
         switch (createStatus)
         {
            case MembershipCreateStatus.DuplicateUserName:
               return "Username already exists. Please enter a different user name.";

            case MembershipCreateStatus.DuplicateEmail:
               return "A username for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
               return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
               return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
               return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
               return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
               return "The user name provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
               return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
               return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
               return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
         }
      }

      /// <summary>
      /// This authenticates a user based on a response from an openid provider.
      /// </summary>
      private ActionResult AuthenticateUser(IAuthenticationResponse response)
      {
         if (response.Status != AuthenticationStatus.Authenticated)
            throw new ArgumentException("The response status must be 'Authenticated'. (" + response.Status.ToString() + ")", "response");

         TadmapDb db = new TadmapDb();

         var user = db.UserOpenIds.Where(u => u.OpenIdUrl == response.ClaimedIdentifier.ToString()).SingleOrDefault();
         Guid userId;

         if (user != null)
         {
            userId = user.UserId;
         }
         else
         {
            userId = CreateNewUser(response.ClaimedIdentifier.ToString());
         }

         var roles = from role in db.UserRoles
                     where role.UserId == userId
                     select role.Role;

         CookieUserData userData = new CookieUserData(
            userId,
            response.FriendlyIdentifierForDisplay.ToString(),
            roles.ToArray()
         );
         
         FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
           1,
           response.ClaimedIdentifier.ToString(),
           DateTime.Now,
           DateTime.Now.AddMinutes(30),
           false,
           userData.ToString()
         );

         // Encrypt the ticket.
         string encTicket = FormsAuthentication.Encrypt(ticket);

         // Create the cookie.
         Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

         return RedirectToAction("Index", new { controller = "Home" });
      }

      private Guid CreateNewUser(string openIdUrl)
      {
         TadmapDb db = new TadmapDb(Database.TadMapConnection);

         User newUser = new User();
         newUser.Id = Guid.NewGuid();
         newUser.Name = string.Empty;

         UserRole newUserRole = new UserRole();
         newUserRole.UserId = newUser.Id;
         newUserRole.Role = TadMapRoles.Collector;

         UserOpenId newOpenId = new UserOpenId();
         newOpenId.UserId = newUser.Id;
         newOpenId.OpenIdUrl = openIdUrl;

         db.Users.InsertOnSubmit(newUser);
         db.UserRoles.InsertOnSubmit(newUserRole);
         db.UserOpenIds.InsertOnSubmit(newOpenId);

         db.SubmitChanges();

         return newUser.Id;
      }
   }

   // The FormsAuthentication type is sealed and contains static members, so it is difficult to
   // unit test code that calls its members. The interface and helper class below demonstrate
   // how to create an abstract wrapper around such a type in order to make the AccountController
   // code unit testable.

   public interface IFormsAuthentication
   {
      void SetAuthCookie(string userName, bool createPersistentCookie);
      void SignOut();
   }

   public class FormsAuthenticationWrapper : IFormsAuthentication
   {
      public void SetAuthCookie(string userName, bool createPersistentCookie)
      {
         FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
      }
      public void SignOut()
      {
         FormsAuthentication.SignOut();
      }
   }


}
