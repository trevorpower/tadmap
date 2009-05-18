using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Test.Mock;

namespace Tadmap.Model.Test
{
   [TestFixture]
   public class BinaryRepositoryTests : BinaryRepositoryTestsBase
   {
      private static List<BinaryRepository.Data> _storage;

      protected override Tadmap.Model.Image.IBinaryRepository CreateBinaryRepository()
      {
         return new BinaryRepository(_storage);
      }

      [SetUp]
      public void SetUp()
      {
         _storage = new List<BinaryRepository.Data>();
      }
   }
}
