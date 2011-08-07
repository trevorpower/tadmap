using System;
using System.Collections.Generic;
using System.Text;
using Tad;

namespace TadMap.Library
{
    [Serializable()]
    public class MapInfo : ReadOnlyBase<MapInfo>
    {
        #region Business Methods

        private Guid _id;
        private string _name;
        private string m_strPictureKey;

        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string PictureKey
        {
            get { return m_strPictureKey; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        public override string ToString()
        {
            return _name;
        }

        #endregion

        #region Constructors

        private MapInfo()
        { /* require use of factory methods */ }

        internal MapInfo(Guid id, string name, string strPictureKey)
        {
            _id = id;
            _name = name;
            m_strPictureKey = strPictureKey;
        }
        #endregion
    }
}
