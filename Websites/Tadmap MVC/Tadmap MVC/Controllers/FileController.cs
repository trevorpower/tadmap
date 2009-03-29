using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;
using Affirma.ThreeSharp;

namespace Tadmap.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index(string key)
        {
           return File(
              Url.Content("F:/TadmapLocalData/LocalBinaryFolder" + key),
              ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(key))
           );
        }
    }
}
