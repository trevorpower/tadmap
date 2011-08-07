using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Security
{
    public class AuthorizationRules
    {
        private Dictionary<string, RolesForProperty> m_oRules;

        private Dictionary<string, RolesForProperty> Rules
        {
            get
            {
                if (m_oRules == null)
                    m_oRules = new Dictionary<string, RolesForProperty>();
                return m_oRules;
            }
        }

        private RolesForProperty GetRolesForProperty(string strPropertyName)
        {
            RolesForProperty oRoles;

            if (Rules.ContainsKey(strPropertyName))
            {
                oRoles = Rules[strPropertyName];
            }
            else
            {
                oRoles = new RolesForProperty();
                Rules.Add(strPropertyName, oRoles);
            }

            return oRoles;
        }

        public string[] GetRolesForProperty(string strPropertyName, AccessType eAccessType)
        {
            RolesForProperty oRoles = GetRolesForProperty(strPropertyName);

            switch (eAccessType)
            {
                case AccessType.ReadAllowed:
                    return oRoles.ReadAllowed.ToArray();
                case AccessType.ReadDenied:
                    return oRoles.ReadDenied.ToArray();
                case AccessType.WriteAllowed:
                    return oRoles.WriteAllowed.ToArray();
                case AccessType.WriteDenied:
                    return oRoles.WriteDenied.ToArray();
            }

            return null;
        }

        public void AllowRead(string strPropertyName, params string[] oRoles)
        {
            RolesForProperty oRolesForProperty = GetRolesForProperty(strPropertyName);
            foreach(string strRole in oRoles)
                oRolesForProperty.ReadAllowed.Add(strRole);
        }

        public void DenyRead(string strPropertyName, params string[] oRoles)
        {
            RolesForProperty oRolesForProperty = GetRolesForProperty(strPropertyName);
            foreach(string strRole in oRoles)
                oRolesForProperty.ReadDenied.Add(strRole);
        }
        
        public void AllowWrite(string strPropertyName, params string[] oRoles)
        {
            RolesForProperty oRolesForProperty = GetRolesForProperty(strPropertyName);
            foreach(string strRole in oRoles)
                oRolesForProperty.WriteAllowed.Add(strRole);
        }

        public void DenyWrite(string strPropertyName, params string[] oRoles)
        {
            RolesForProperty oRolesForProperty = GetRolesForProperty(strPropertyName);
            foreach(string strRole in oRoles)
                oRolesForProperty.WriteDenied.Add(strRole);
        }

        public bool HasReadAllowedRoles(string strPropertyName)
        {
            return GetRolesForProperty(strPropertyName).ReadAllowed.Count > 0;
        }

        public bool HasReadDeniedRoles(string strPropertyName)
        {
            return GetRolesForProperty(strPropertyName).ReadDenied.Count > 0;
        }

        public bool HasWriteAllowedRoles(string strPropertyName)
        {
            return GetRolesForProperty(strPropertyName).WriteAllowed.Count > 0;
        }

        public bool HasWriteDeniedRoles(string strPropertyName)
        {
            return GetRolesForProperty(strPropertyName).WriteDenied.Count > 0;
        }

        public bool IsReadAllowed(string strPropertyName)
        {
            System.Security.Principal.IPrincipal oUser = ApplicationContext.User;
            return GetRolesForProperty(strPropertyName).IsReadAllowed(oUser);
        }

        public bool IsReadDenied(string strPropertyName)
        {
            System.Security.Principal.IPrincipal oUser = ApplicationContext.User;
            return GetRolesForProperty(strPropertyName).IsReadDenied(oUser);
        }

        public bool IsWriteAllowed(string strPropertyName)
        {
            System.Security.Principal.IPrincipal oUser = ApplicationContext.User;
            return GetRolesForProperty(strPropertyName).IsWriteAllowed(oUser);
        }

        public bool IsWriteDenied(string strPropertyName)
        {
            System.Security.Principal.IPrincipal oUser = ApplicationContext.User;
            return GetRolesForProperty(strPropertyName).IsWriteDenied(oUser);
        }
    }
}
