using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Tadmap.Model.Test;

namespace Tadmap.Local.Test
{
   [TestFixture]
   public class BinaryRepositoryTests : BinaryRepositoryTestsBase
   {
      private const string _folder = "../../Temp_TestBinaryRepository/";

      protected override Tadmap.Model.Image.IBinaryRepository CreateBinaryRepository()
      {
         return new BinaryRepository(_folder);
      }

      [SetUp]
      public void SetUp()
      {
         Directory.CreateDirectory(_folder);
      }

      [TearDown]
      public void TearDown()
      {
         Directory.Delete(_folder, true);
      }

   }
}
