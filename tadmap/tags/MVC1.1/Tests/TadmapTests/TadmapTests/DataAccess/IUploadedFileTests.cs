using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.DataAccess;

namespace TadmapTests.DataAccess
{
   [TestFixture]
   public class IUploadedFileTests
   {
      [Test]
      public void Has_InputStream_FileName_And_ContentLength()
      {
         IUploadedFile file = new TestUploadedFile();

         Assert.IsNotNull(file.InputStream);
         Assert.IsNotNull(file.FileName);
         Assert.IsNotNull(file.ContentLength);
      }
   }
}
