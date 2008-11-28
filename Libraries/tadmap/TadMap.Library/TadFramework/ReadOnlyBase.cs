using System;
using System.Collections.Generic;
using System.Text;

namespace Tad
{
    public abstract class ReadOnlyBase<T> : Core.IBusinessObject
        where T : ReadOnlyBase<T>
    {
        protected abstract object GetIdValue();

        public override bool Equals(object oObject)
        {
            if (oObject is T)
            {
                object oId = GetIdValue();
                if (oId == null)
                    throw new ArgumentException("ID value can't be null");

                return ((T)oObject).GetIdValue().Equals(oId);
            }

            return false;
        }

        public override int GetHashCode()
        {
            object oId = GetIdValue();
            if (oId == null)
                throw new ArgumentException("ID value can't be null");

            return oId.GetHashCode();
        }

        public override string ToString()
        {
            object oId = GetIdValue();
            if (oId == null)
                throw new ArgumentException("ID value can't be null");

            return oId.ToString();
        }

        #region Authorization Rules

        private Security.AuthorizationRules m_oAuthorizationRules;

        protected internal Security.AuthorizationRules AuthorizationRules
        {
            get
            {
                if (m_oAuthorizationRules == null)
                    m_oAuthorizationRules = new Security.AuthorizationRules();

                return m_oAuthorizationRules;
            }
        }

        protected virtual void AddAuthorizationRules()
        {
        }

        public virtual bool CanReadProperty(string strPropertyName)
        {
            bool bResult = true;

            if (AuthorizationRules.HasReadAllowedRoles(strPropertyName))
            {
                if (!AuthorizationRules.IsReadAllowed(strPropertyName))
                    bResult = false;
            }
            else if (AuthorizationRules.HasReadDeniedRoles(strPropertyName))
            {
                if (AuthorizationRules.IsReadDenied(strPropertyName))
                    bResult = false;
            }

            return bResult;
        }

        public virtual bool CanWriteProperty(string strPropertyName)
        {
            bool bResult = true;

            if (AuthorizationRules.HasWriteAllowedRoles(strPropertyName))
            {
                if (!AuthorizationRules.IsWriteAllowed(strPropertyName))
                    bResult = false;
            }
            else if (AuthorizationRules.HasWriteDeniedRoles(strPropertyName))
            {
                if (AuthorizationRules.IsWriteDenied(strPropertyName))
                    bResult = false;
            }

            return bResult;
        }

        public bool CanReadThisProperty()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            return CanReadProperty(strPropertyName);
        }

        public bool CanWriteThisProperty()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            return CanWriteProperty(strPropertyName);
        }

        public void ThrowIfCantThisRead()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            if (!CanReadProperty(strPropertyName))
                throw new System.Security.SecurityException("Cannot read property (" + strPropertyName + ")");
        }

        public void ThrowIfCantThisWrite()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            if (!CanWriteProperty(strPropertyName))
                throw new System.Security.SecurityException("Cannot write property (" + strPropertyName + ")");
        }

        #endregion
    }
}
