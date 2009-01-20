using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.DataAccess;
using System.IO;

namespace TadmapTests.DataAccess
{
   [TestFixture]
   public class BinaryRepositoryTests
   {
      [Test]
      public void Contains_Add_Function()
      {
         IBinaryRepository repository = new TestBinaryRepository();

         repository.Add(new MemoryStream(), "", "");
      }

      [Test]
      public void Contains_GetUrl_Function()
      {
         IBinaryRepository repository = new TestBinaryRepository();

         string url = repository.GetUrl("key1");
      }

      [Test]
      public void Key1_Returns_key1url()
      {
         IBinaryRepository repository = new TestBinaryRepository();

         string url = repository.GetUrl("key1");

         Assert.AreEqual("key1url", url);
      }

   }
}
