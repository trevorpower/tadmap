using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Tad.Core
{
    [Serializable]
    public abstract class BusinessBase        
    {
        #region Private State

        private bool m_bIsNew = true;
        private bool m_bIsDeleted = false;
        private bool m_bIsDirty = true;
        private bool m_bIsChild = false; 

        #endregion

        #region State Manipulators

        protected void MarkOld()
        {
            m_bIsNew = false;
            MarkClean();
        }

        protected void MarkNew()
        {
            m_bIsNew = true;
            m_bIsDeleted = false;
            MarkDirty();
        }

        protected void MarkClean()
        {
            m_bIsDirty = false;
        }

        protected void MarkDirty()
        {
            m_bIsDirty = true;
        }

        protected void MarkDeleted()
        {
            m_bIsDeleted = true;
            MarkDirty();
        }

        protected void MarkAsChild()
        {
            m_bIsChild = true;
        }

        public void Delete()
        {
            if (IsChild)
                throw new NotSupportedException("Cannot delete a child object");

            MarkDeleted();
        }

        internal void DeleteChild()
        {
            if (!IsChild)
                throw new NotSupportedException("Cannot delete non-child object");

            MarkDeleted();
        }

        #endregion

        #region State Accessors
        
        public virtual bool IsDirty
        {
            get { return m_bIsDirty; }
        }

        public bool IsNew
        {
            get { return m_bIsNew; }
        }

        public virtual bool IsValid
        {
            get { return ValidationRules.IsValid; }
        }

        public bool IsSavable
        {
            get { return (IsDirty && IsValid); }
        }

        protected internal bool IsChild
        {
            get { return m_bIsChild; }
        }

        public bool IsDeleted
        {
            get { return m_bIsDeleted; }
        }

        #endregion       

        #region Private Rules
        
        private Validation.ValidationRules m_oValidationRules;
        private Security.AuthorizationRules m_oAuthorizationRules;

        #endregion

        #region Business Rules

        protected Validation.ValidationRules ValidationRules
        {
            get
            {
                if (m_oValidationRules == null)
                    m_oValidationRules = new Tad.Validation.ValidationRules(this);

                return m_oValidationRules;
            }
        }

        
        protected virtual void AddBusinessRules()
        {
        }

        public virtual Validation.BrokenRuleCollection BrokenRuleCollection
        {
            get { return ValidationRules.BrokenRules; }
        }

        #endregion

        #region Authorization Rules
        
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

        public void ThrowIfCantReadThis()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            if (!CanReadProperty(strPropertyName))
                throw new System.Security.SecurityException("Cannot read property (" + strPropertyName + ")");
        }

        public void ThrowIfCantWriteThis()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            if (!CanWriteProperty(strPropertyName))
                throw new System.Security.SecurityException("Cannot write property (" + strPropertyName + ")");
        }

        protected virtual void PropertyHasChanged(string strPropertyName)
        {
            ValidationRules.CheckRules(strPropertyName);
            MarkDirty();
        }

        protected void ThisHasChanged()
        {
            string strPropertyName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);
            PropertyHasChanged(strPropertyName);
        }

        #endregion

        #region Constructor
        
        protected BusinessBase()
        {
            AddBusinessRules();
            AddAuthorizationRules();
        }

        #endregion

        #region Serialization
        
        [OnDeserialized]
        private void OnDeserializedHandler(StreamingContext oContext)
        {
            ValidationRules.SetTarget(this);
            AddBusinessRules();
            OnDeserialized(oContext);
        }

        protected virtual void OnDeserialized(StreamingContext oContext)
        {
        }

        #endregion
    
        
    }
}
