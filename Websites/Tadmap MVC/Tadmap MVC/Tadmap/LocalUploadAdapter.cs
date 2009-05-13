using System;
using System.Web;
using System.Collections;
using Tadmap.Model.Image;
using Tadmap.Local;
using Affirma.ThreeSharp;
using System.IO;
using com.flajaxian;
using Tadmap.Model;

namespace Tadmap
{
   public class LocalUploadAdapter : FileUploaderAdapter, IUploadAdapter
   {
      public override void ProcessFile(HttpPostedFile file)
      {
         var repository = new BinaryRepository("F:/TadmapLocalData/LocalBinaryFolder");
         var args = new FileNameDeterminingEventArgs(file);

         OnFileNameDetermining(args);

         string newName = args.FileName;
         string mimeType = ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(newName));
         
         repository.Add(file.InputStream, newName, mimeType);

         OnConfirmUpload(new ConfirmUploadEventArgs(file.FileName, args.FileName, file.InputStream.Length, 0, false, true, 0));
      }

      public event EventHandler<FileNameDeterminingEventArgs> FileNameDetermining;

      public event EventHandler<ConfirmUploadEventArgs> ConfirmUpload;

      protected virtual void OnFileNameDetermining(FileNameDeterminingEventArgs oArgs)
      {
         if (FileNameDetermining != null)
         {
            FileNameDetermining(this, oArgs);
         }
      }

      protected virtual void OnConfirmUpload(ConfirmUploadEventArgs oArgs)
      {
         if (ConfirmUpload != null)
         {
            ConfirmUpload(this, oArgs);
         }
      }
   }
}