using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Test;
using Tadmap.Model.Image;
using NUnit.Framework;

namespace Tadmap.Amazon.Test
{
   [TestFixture]
   [Category("Requires Internet")]
   public class BinaryRepositoryTests : BinaryRepositoryTestsBase
   {
      protected override IBinaryRepository CreateBinaryRepository()
      {
         return new BinaryRepository("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7", "unit-test.tadmap.com");
      }
   }
}
