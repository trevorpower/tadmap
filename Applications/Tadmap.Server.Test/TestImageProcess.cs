using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Image;

namespace Tadmap.Server.Test
{
   [TestFixture]
   public class TestImageProcess
   {
      [Test]
      public void FullTest()
      {
         IBinaryRepository repository = new Tadmap.Amazon.BinaryRepository("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7", "tadmap");

         ProcessImage.Process("1e43ffaa-b2c5-4852-a0c7-90a5926b2bce.jpg", repository);
      }
   }
}
