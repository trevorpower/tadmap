using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Image;
using Tadmap.Mode.Test.Mock;
using Tadmap.Model.Test.Mock;

namespace Tadmap.Model.Test
{
   [TestFixture]
   public class ImageRepositoryTests : ImageRepositoryTestsBase
   {
      private static List<BinaryRepository.Data> _storage;

      protected override Tadmap.Model.Image.IImageRepository CreateImageRepository()
      {
         IBinaryRepository binaries = new BinaryRepository(new List<BinaryRepository.Data>());
         return new ImageRepository();
      }

      [SetUp]
      public void SetUp()
      {
         _storage = new List<BinaryRepository.Data>();
      }
   }
}
