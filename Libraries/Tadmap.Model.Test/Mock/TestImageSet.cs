using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;

namespace Tadmap.Model.Test.Mock
{
   public class TestImageSet : IImageSet
   {
      public TestImageSet(string key)
      {
         throw new NotImplementedException();
      }

      #region IImageSet Members

      public string Original
      {
         get { throw new NotImplementedException(); }
      }

      public string Preview
      {
         get { throw new NotImplementedException(); }
      }

      public string Square
      {
         get { throw new NotImplementedException(); }
      }

      public void Create(System.IO.Stream stream, IBinaryRepository binaryRepository)
      {
         throw new NotImplementedException();
      }

      #endregion
   }
}
