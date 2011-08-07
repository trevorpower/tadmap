using System;
using System.Collections.Generic;
using System.Text;

namespace Tad
{
    [Serializable]
    public abstract class BusinessBase<T> : Core.BusinessBase where T : BusinessBase<T>
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

        public virtual T Save()
        {
            if (IsChild)
                throw new NotSupportedException("Cannot save a child");
            if (!IsValid)
                throw new Validation.ValidationException("Cannot save in valid object");
            if (IsDirty)
                return (T)Tad.DataPortal.Client.DataPortal.Update(this);
            else
                return (T)this;
        }

        public T ForceSave()
        {
            if (IsNew)
            {
                MarkOld();
                MarkDirty();
            }

            return Save();
        }
    }
}
