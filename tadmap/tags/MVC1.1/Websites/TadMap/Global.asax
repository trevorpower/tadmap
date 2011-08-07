<%@ Application Language="C#" %>

<script RunAt="server">

   void Application_Start(object sender, EventArgs e)
   {
      // Code that runs on application startup

   }

   void Application_End(object sender, EventArgs e)
   {
      //  Code that runs on application shutdown

   }

   void Application_Error(object sender, EventArgs e)
   {
      // Get the actual error that brought us here
      Exception ex = Server.GetLastError().InnerException;
      StringBuilder message = new StringBuilder( "An error occurred in the application:");
      message.AppendLine(ex.Message);
      message.AppendLine("Stack trace:");
      message.AppendLine(ex.StackTrace);
      
      StringBuilder s = new StringBuilder(ex.StackTrace);
      message.Append(s.Replace(" at ", " at \n"));

      message.AppendLine("Source:");
      message.AppendLine(ex.Source);

      message.AppendLine("Querystring:");
      message.AppendLine(Request.QueryString.ToString());
      
      // Mail the message to the developer
      System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mail.tadmap.com");
      
      client.Send("error@tadmap.com", "trevor_power@yahoo.com", "Tadmap Error", message.ToString());
      
      Server.ClearError();
      Response.Write("We're sorry, but an unexpected error has occurred.");
      Response.End();
   }



   void Session_Start(object sender, EventArgs e)
   {
   }

   void Session_End(object sender, EventArgs e)
   {
   }

   protected void Application_AuthenticateRequest(object sender, EventArgs e)
   {
      //HttpContext currentContext = HttpContext.Current;

      //if (HttpContext.Current.User != null)
      //{
      //   if (HttpContext.Current.User.Identity.IsAuthenticated)
      //   {
      //      if (HttpContext.Current.User.Identity.AuthenticationType == "OpenId")
      //      {
      //         System.Security.Principal.IIdentity id = HttpContext.Current.User.Identity;
      //         AuthenticationTicket ticket = id.Ticket;
      //         string userData = ticket.UserData;
      //         // Roles is a helper class which places the roles of the
      //         // currently logged on user into a string array
      //         // accessable via the value property.
      //         Roles userRoles = new Roles(userData);
      //         HttpContext.Current.User = new GenericPrincipal(id, userRoles.Value);
      //      }
      //   }
      //}
   }
   
   protected void Application_AcquireRequestState(object sender, EventArgs e)
   {
      //System.Security.Principal.IPrincipal principal;

      //try
      //{
      //   principal = (System.Security.Principal.IPrincipal)HttpContext.Current.Session["TadMapPrincipal"];
      //}
      //catch
      //{
      //   principal = null;
      //}

      //if (principal == null)
      //{
      //   HttpContext.Current.User = TadMap.Security.TadMapAuthentication.UnauthenticatedPrincipal;
      //}
      //else
      //{
      //   HttpContext.Current.User = principal;
      //}
   }

</script>

