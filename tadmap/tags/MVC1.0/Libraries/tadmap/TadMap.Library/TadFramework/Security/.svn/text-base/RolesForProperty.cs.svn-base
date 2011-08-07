using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace Tad.Security
{
    internal class RolesForProperty
    {
        private List<string> m_oReadAllowed = new List<string>();
        private List<string> m_oReadDenied = new List<string>();
        private List<string> m_oWriteAllowed = new List<string>();
        private List<string> m_oWriteDenied = new List<string>();

        public List<string> ReadAllowed
        {
            get { return m_oReadAllowed; }
        }
    
        public List<string> ReadDenied
        {
            get { return m_oReadDenied; }
        }

        public List<string> WriteAllowed
        {
            get { return m_oWriteAllowed; }
        }

        public List<string> WriteDenied
        {
            get { return m_oWriteDenied; }
        }

        public bool IsReadAllowed(IPrincipal oPrincipal)
        {
            foreach (string strRoles in ReadAllowed)
                if (oPrincipal.IsInRole(strRoles))
                    return true;
            return false;
        }

        public bool IsReadDenied(IPrincipal oPrincipal)
        {
            foreach (string strRoles in ReadDenied)
                if (oPrincipal.IsInRole(strRoles))
                    return true;
            return false;
        }

        public bool IsWriteAllowed(IPrincipal oPrincipal)
        {
            foreach (string strRoles in WriteAllowed)
                if (oPrincipal.IsInRole(strRoles))
                    return true;
            return false;
        }

        public bool IsWriteDenied(IPrincipal oPrincipal)
        {
            foreach (string strRoles in WriteDenied)
                if (oPrincipal.IsInRole(strRoles))
                    return true;
            return false;
        }

    }
}
