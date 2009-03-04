using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.DataAccess;
using Tadmap.DataAccess.S3;
using Tadmap.Model.Image;

namespace RepositoryIntegrationTests.S3
{
   [TestFixture]
   public class S3BinaryRepositoryTests
   {
      [Test]
      public void Can_Call_Get_URL()
      {
         IBinaryRepository repository = new S3BinaryRepository();

         repository.GetUrl("test");
      }
   }
}
