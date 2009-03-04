using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.DataAccess;
using System.IO;
using Tadmap.Model.Image;
using Tadmap.Model.Test.Mock;

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

         Uri url = repository.GetUrl("key1");
      }

      [Test]
      public void Key1_Returns_key1url()
      {
         IBinaryRepository repository = new TestBinaryRepository();

         Uri url = repository.GetUrl("key1");

         Assert.AreEqual(new Uri("http://key1.url"), url);
      }

   }
}
