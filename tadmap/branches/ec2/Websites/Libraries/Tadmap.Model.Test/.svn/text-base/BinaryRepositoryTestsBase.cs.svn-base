using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Image;
using System.IO;

namespace Tadmap.Model.Test
{
   public abstract class BinaryRepositoryTestsBase
   {
      protected abstract IBinaryRepository CreateBinaryRepository();

      private byte[] _smallBinary = { 100, 200 };

      private byte[] _mediumBinary;

      protected BinaryRepositoryTestsBase()
      {
         int length = 3000;

         _mediumBinary = new byte[length];

         for (int i = 0; i < length; i++)
            _mediumBinary[i] = (byte)(i % (byte.MaxValue + 1));
      }

      [Test]
      public void Can_Create()
      {
         IBinaryRepository repository = CreateBinaryRepository();
      }

      [Test]
      public void Can_Add_Empty_Stream()
      {
         IBinaryRepository repository = CreateBinaryRepository();

         using (Stream stream = new MemoryStream())
            repository.Add(stream, "key", "contenttype");
      }

      [Test]
      public void Can_Add_Small_Stream()
      {
         IBinaryRepository repository = CreateBinaryRepository();

         using (Stream stream = new MemoryStream(_smallBinary))
            repository.Add(stream, "key", "contenttype");
      }

      [Test]
      public void Can_Get_Url()
      {
         IBinaryRepository repository = CreateBinaryRepository();

         repository.GetUrl("key");
      }

      [Test]
      public void Can_Get_Url_For_Added_Binary()
      {
         IBinaryRepository repository = CreateBinaryRepository();
         using (Stream stream = new MemoryStream(_smallBinary))
            repository.Add(stream, "key", "contentType");
         repository.GetUrl("key");
      }

      [Test]
      public void Can_Get_Binary()
      {
         IBinaryRepository repository = CreateBinaryRepository();
         using (Stream stream = new MemoryStream(_smallBinary))
            repository.Add(stream, "key", "contentType");

         using (Stream stream = repository.GetBinary("key"))
            Assert.IsNotNull(stream);
      }

      [Test]
      public void Get_Binary_Returns_Correct_Data_For_Small_Data()
      {
         IBinaryRepository repository = CreateBinaryRepository();

         using (Stream stream = new MemoryStream(_smallBinary))
            repository.Add(stream, "key", "contentType");

         using (Stream stream = repository.GetBinary("key"))
         {
            byte[] returnedData = new byte[stream.Length];

            stream.Read(returnedData, 0, returnedData.Length);

            Assert.AreEqual(_smallBinary, returnedData);
         }
      }

      [Test]
      public void Get_Binary_Returns_Correct_Data_For_Medium_Data()
      {
         IBinaryRepository repository = CreateBinaryRepository();

         using (Stream stream = new MemoryStream(_mediumBinary))
            repository.Add(stream, "key", "contentType");

         using (Stream stream = repository.GetBinary("key"))
         {
            byte[] returnedData = new byte[stream.Length];

            stream.Read(returnedData, 0, returnedData.Length);

            Assert.AreEqual(_mediumBinary, returnedData);
         }
      }

      [Test]
      public void Binary_Persists_Past_Instances()
      {
         {
            IBinaryRepository repository = CreateBinaryRepository();

            using (Stream stream = new MemoryStream(_smallBinary))
               repository.Add(stream, "key", "contentType");
         }

         {
            IBinaryRepository repository = CreateBinaryRepository();

            using (Stream stream = repository.GetBinary("key"))
            {
               byte[] returnedData = new byte[stream.Length];

               stream.Read(returnedData, 0, returnedData.Length);

               Assert.AreEqual(_smallBinary, returnedData);
            }
         }
      }
   }
}
