using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.flajaxian;

namespace Tadmap.Model
{
   public interface IUploadAdapter
   {
      event EventHandler<com.flajaxian.FileNameDeterminingEventArgs> FileNameDetermining;

      event EventHandler<com.flajaxian.ConfirmUploadEventArgs> ConfirmUpload;
   }
}
