using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace com.flajaxian
{
    public class DirectAmazonUploader : FileUploaderAdapter
    {
        private string _bucketName;
        private string _path;
        private string _xAmzMetaUuid;
        private int _uploadValidityExpiresAfterMinutes = 60;
        private FileAccess _fileAccess = FileAccess.PublicRead;
        private string _secretKey;
        private string _accessKey;
        private string _urlSchemeToAmazon;
        private bool _allowRootCrossDomainXmlUpload;
        private bool _allowPathDefinedByClient;
        private string _confirmUploadJsFunc;

        public const string WebConfigPrefix = "WebConfig:";

        public event EventHandler<FileNameDeterminingEventArgs> FileNameDetermining;
        public event EventHandler<ContentTypeDeterminingEventArgs> ContentTypeDetermining;
        public event EventHandler<ConfirmUploadEventArgs> ConfirmUpload;

        private static readonly Dictionary<string, string> _knownMimeTypes = GetKnownMimeTypes();

        #region Known Mime Types
        private static Dictionary<string, string> GetKnownMimeTypes()
        {
            Dictionary<string, string> doc = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            doc.Add(".rar", "application/x-rar-compressed");
            doc.Add(".gg", "app/gg");
            doc.Add(".fif","application/fractals");
            doc.Add(".spl","application/futuresplash");
            doc.Add(".hta","application/hta");
            doc.Add(".hqx","application/mac-binhex40");
            doc.Add(".mp4","application/mpeg4");
            doc.Add(".vsi","application/ms-vsi");
            doc.Add(".mdb","application/msaccess");
            doc.Add(".doc","application/msword");
            doc.Add(".pdf","application/pdf");
            doc.Add(".p10","application/pkcs10");
            doc.Add(".p7c","application/pkcs7-mime");
            doc.Add(".p7s","application/pkcs7-signature");
            doc.Add(".cer","application/pkix-cert");
            doc.Add(".crl","application/pkix-crl");
            doc.Add(".ps","application/postscript");
            doc.Add(".rss","application/rss+xml");
            doc.Add(".sdp","application/sdp");
            doc.Add(".smi","application/smil");
            doc.Add(".edn","application/vnd.adobe.edn");
            doc.Add(".pdx","application/vnd.adobe.pdx");
            doc.Add(".rmf","application/vnd.adobe.rmf");
            doc.Add(".xdp","application/vnd.adobe.xdp+xml");
            doc.Add(".xfd","application/vnd.adobe.xfd+xml");
            doc.Add(".xfdf","application/vnd.adobe.xfdf");
            doc.Add(".fdf","application/vnd.fdf");
            doc.Add(".kml","application/vnd.google-earth.kml");
            doc.Add(".kmz","application/vnd.google-earth.kmz");
            doc.Add(".xls","application/vnd.ms-excel");
            doc.Add(".sst","application/vnd.ms-pki.certstore");
            doc.Add(".pko","application/vnd.ms-pki.pko");
            doc.Add(".cat","application/vnd.ms-pki.seccat");
            doc.Add(".stl","application/vnd.ms-pki.stl");
            doc.Add(".ppt","application/vnd.ms-powerpoint");
            doc.Add(".wpl","application/vnd.ms-wpl");
            doc.Add(".xps","application/vnd.ms-xpsdocument");
            doc.Add(".rm","application/vnd.rn-realmedia");
            doc.Add(".rnx","application/vnd.rn-realplayer");
            doc.Add(".rsml","application/vnd.rn-rsml");
            doc.Add(".adobebridge","application/x-bridge-url");
            doc.Add(".z","application/x-compress");
            doc.Add(".tgz","application/x-compressed");
            doc.Add(".etd","application/x-ebx");
            doc.Add(".gz","application/x-gzip");
            doc.Add(".itms","application/x-itunes-itms");
            doc.Add(".itpc","application/x-itunes-itpc");
            doc.Add(".jnlp","application/x-java-jnlp-file");
            doc.Add(".jtx","application/x-jtx+xps");
            doc.Add(".latex","application/x-latex");
            doc.Add(".nix","application/x-mix-transfer");
            doc.Add(".mxp","application/x-mmxp");
            doc.Add(".amc","application/x-mpeg");
            doc.Add(".asx","application/x-mplayer2");
            doc.Add(".application","application/x-ms-application");
            doc.Add(".slupkg-ms","application/x-ms-license");
            doc.Add(".vsto","application/x-ms-vsto");
            doc.Add(".wmd","application/x-ms-wmd");
            doc.Add(".wmz","application/x-ms-wmz");
            doc.Add(".xbap","application/x-ms-xbap");
            doc.Add(".pinstall","application/x-picasa-detect");
            doc.Add(".p12","application/x-pkcs12");
            doc.Add(".p7b","application/x-pkcs7-certificates");
            doc.Add(".p7r","application/x-pkcs7-certreqresp");
            doc.Add(".pcast","application/x-podcast");
            doc.Add(".qtl","application/x-quicktimeplayer");
            doc.Add(".rtsp","application/x-rtsp");
            doc.Add(".swf","application/x-shockwave-flash");
            doc.Add(".xap","application/x-silverlight");
            doc.Add(".skype","application/x-skype");
            doc.Add(".sparc","application/x-sparc");
            doc.Add(".sit","application/x-stuffit");
            doc.Add(".tar","application/x-tar");
            doc.Add(".wtml","application/x-wtml");
            doc.Add(".wtt","application/x-wtt");
            doc.Add(".wwtfig","application/x-wwtfig");
            doc.Add(".zip","application/x-zip-compressed");
            doc.Add(".xaml", "text/xaml");
            doc.Add(".3gp","audio/3gpp");
            doc.Add(".3g2","audio/3gpp2");
            doc.Add(".AMR","audio/AMR");
            doc.Add(".au","audio/basic");
            doc.Add(".m4a","audio/m4a");
            doc.Add(".mp3","audio/mp3");
            doc.Add(".m3u","audio/mpegurl");
            doc.Add(".qcp","audio/vnd.qcelp");
            doc.Add(".wav","audio/wav");
            doc.Add(".aac","audio/x-aac");
            doc.Add(".ac3","audio/x-ac3");
            doc.Add(".aiff","audio/x-aiff");
            doc.Add(".caf","audio/x-caf");
            doc.Add(".gsm","audio/x-gsm");
            doc.Add(".wax","audio/x-ms-wax");
            doc.Add(".wma","audio/x-ms-wma");
            doc.Add(".ram","audio/x-pn-realaudio");
            doc.Add(".ra","audio/x-realaudio");
            doc.Add(".rms","audio/x-realaudio-secure");
            doc.Add(".divx","ICM.DIV4");
            doc.Add(".bmp","image/bmp");
            doc.Add(".gif","image/gif");
            doc.Add(".jp2","image/jp2");
            doc.Add(".jpg","image/jpeg");
            doc.Add(".jpeg","image/jpeg");
            doc.Add(".pict","image/pict");
            doc.Add(".png","image/png");
            doc.Add(".tiff","image/tiff");
            doc.Add(".rp","image/vnd.rn-realpix");
            doc.Add(".ico","image/x-icon");
            doc.Add(".pntg","image/x-macpaint");
            doc.Add(".qtif","image/x-quicktime");
            doc.Add(".sgi","image/x-sgi");
            doc.Add(".targa","image/x-targa");
            doc.Add(".mid","midi/mid");
            doc.Add(".dwfx","model/vnd.dwfx+xps");
            doc.Add(".p7m","pkcs7-mime");
            doc.Add(".css","text/css");
            doc.Add(".dlm","text/dlm");
            doc.Add(".htm","text/html");
            doc.Add(".txt","text/plain");
            doc.Add(".wsc","text/scriptlet");
            doc.Add(".rt","text/vnd.rn-realtext");
            doc.Add(".htc","text/x-component");
            doc.Add(".contact","text/x-ms-contact");
            doc.Add(".odc","text/x-ms-odc");
            doc.Add(".vcf","text/x-vcard");
            doc.Add(".xml","text/xml");
            doc.Add(".avi","video/avi");
            doc.Add(".flc","video/flc");
            doc.Add(".mpg","video/mpeg");
            doc.Add(".mov","video/quicktime");
            doc.Add(".sdv","video/sd-video");
            doc.Add(".rv","video/vnd.rn-realvideo");
            doc.Add(".m4v","video/x-m4v");
            doc.Add(".mps","video/x-mpeg");
            doc.Add(".mpeg","video/x-mpeg2a");
            doc.Add(".mpa","video/x-mpg");
            doc.Add(".wm","video/x-ms-wm");
            doc.Add(".wmv","video/x-ms-wmv");
            doc.Add(".wmx","video/x-ms-wmx");
            doc.Add(".wvx","video/x-ms-wvx");
            return doc;
        }
        #endregion

        #region Public Read-Write Properties
        /// <summary>
        // The Amazon Bucket Name
        /// </summary>
        public string BucketName
        {
            get { return this._bucketName; }
            set { this._bucketName = value; }
        }
        /// <summary>
        /// Specifies permissions for the file on the Amazon server.
        /// Possible values are: Private, PublicRead, PublicReadWrite, AuthenticatedRead
        /// By default it is: PublicRead
        /// </summary>
        public FileAccess FileAccess
        {
            get { return this._fileAccess; }
            set { this._fileAccess = value; }
        }

        /// <summary>
        /// Path inside the bucket
        /// </summary>
        public string Path
        {
            get { return this._path; }
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    this._path = value.Replace(@"\", @"/");
                    if(!value.EndsWith("/"))
                    {
                        this._path = this._path + @"/";
                        return;
                    }
                }
                this._path = value;
            }
        }
        /// <summary>
        /// The 'x-amz-meta-uuid' ID send to the server, by default the ClientID of the file uploader
        /// </summary>
        public string XAmzMetaUuid
        {
            get
            {
                if (String.IsNullOrEmpty(this._xAmzMetaUuid) && this.Parent != null)
                {
                    this._xAmzMetaUuid = this.Parent.ClientID;
                }
                return this._xAmzMetaUuid;
            }
            set { this._xAmzMetaUuid = value; }
        }

        /// <summary>
        /// The number of minutes after the user preses Upload button when the upload will be valid. By default 60.
        /// </summary>
        public int UploadValidityExpiresAfterMinutes
        {
            get { return this._uploadValidityExpiresAfterMinutes; }
            set { this._uploadValidityExpiresAfterMinutes = value; }
        }

        /// <summary>
        /// Secret key for the amazon web service. There are two ways for useing this property.
        /// First you can just enter the secret key or you can use WebConfig: prefix and then enter 
        /// the appSetting Web.Config key that holds the actual value
        /// </summary>
        public string SecretKey
        {
            get
            {
                return this.CheckForWebConfigEntry(this._secretKey, SR.NoSecretKeyError, SR.SecretKey);
            }
            set { this._secretKey = value; }
        }

        /// <summary>
        /// Access key for the amazon web service. There are two ways for useing this property.
        /// First you can just enter the access key or you can use WebConfig: prefix and then enter 
        /// the appSetting Web.Config key that holds the actual value
        /// </summary>
        public string AccessKey
        {
            get
            {
                return this.CheckForWebConfigEntry(this._accessKey, SR.NoAccessKeyError, SR.AccessKey);
            }
            set { this._accessKey = value; }
        }

        /// <summary>
        /// A value 'http' or 'https' that will determine the protocol used to connect to amazon server.
        /// </summary>
        public string UrlSchemeToAmazon
        {
            get
            {
                return this._urlSchemeToAmazon ?? HttpContext.Current.Request.Url.Scheme;
            }
            set
            {
                if(value == null || (value.ToLower() != "http" && value.ToLower() != "https"))
                {
                    throw new ArgumentException("The UrlSchemeToAmazon property can only be set to 'http' or 'https'");
                }
                this._urlSchemeToAmazon = value;
            }
        }

        /// <summary>
        /// Flag that is set to true will allow the upload of crossdomain.xml file to the root path of the bucket
        /// </summary>
        public bool AllowRootCrossDomainXmlUpload
        {
            get { return this._allowRootCrossDomainXmlUpload; }
            set { this._allowRootCrossDomainXmlUpload = value; }
        }
        /// <summary>
        /// Allows the path of the uploaded file to be defined by the client. By default False. 
        /// If set to True may allow the client to upload to a different folder.
        /// </summary>
        public bool AllowPathDefinedByClient
        {
            get { return this._allowPathDefinedByClient; }
            set { this._allowPathDefinedByClient = value; }
        }


        /// <summary>
        /// An optional javascript function that is called when OnConfirmUpload ajax call is executed
        /// </summary>
        public string ConfirmUploadJsFunc
        {
            get { return this._confirmUploadJsFunc; }
            set { this._confirmUploadJsFunc = value; }
        }
        #endregion

        /// <summary>
        /// The name of the JavaScript variable for Flajaxian.DirectAmazonUpload object
        /// </summary>
        public string JavascriptID
        {
            get { return this.Parent.JavascriptID + "_DirAmz"; }
        }
        /// <summary>
        /// Overriden property that allows to postpone the file upload before a JavaScript function executes
        /// </summary>
        public override bool UploadRequiresJsConfirmation
        {
            get { return true; }
        }
        /// <summary>
        /// A URL for the Ajax call
        /// </summary>
        public string PageUrl
        {
            get
            {
                return this.ComposePageUrl(HttpContext.Current.Request.Url.AbsoluteUri.Substring(0,
                            HttpContext.Current.Request.Url.AbsoluteUri.Length - HttpContext.Current.Request.Url.Query.Length));
            }
        }



        /// <summary>
        /// This method is not used (it is empty) as the processing of the file happens on the Amazon server
        /// </summary>
        /// <param name="file"></param>
        public override void ProcessFile(HttpPostedFile file)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Parent.PreRender += this.FileUploader_PreRender;
        }

        private void FlushResponse()
        {
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private void ProcessInitialAjaxCall()
        {
            string xml = HttpContext.Current.Request["data"];
            if(String.IsNullOrEmpty(xml))
            {
                this.FlushResponse();
                return;
            }
            // this is needed to avoid page validation exception, '|' char cannot be in file or path name
            StringBuilder sb = new StringBuilder(xml);
            sb.Replace("|fs", "<fs");
            sb.Replace("|f i=\"", "<f i=\"");
            sb.Replace("|/fs>", "</fs>");
            HttpContext.Current.Response.Write(this.GetJsonByXml(sb.ToString()));
            // the trace will damage the Ajax response so we remove it
            if(this.Parent.Page.Trace.IsEnabled)
            {
                this.Parent.Page.Trace.IsEnabled = false;
            }
            this.FlushResponse();
        }

        private void ProcessConfirmUploadAjaxCall(HttpRequest r)
        {
            bool isError = (r["e"] != "0");
            bool isLast = (r["l"] != "0");
            int httpStatus;
            Int32.TryParse(r["s"], out httpStatus);
            int index;
            Int32.TryParse(r["i"], out index);
            long bytes;
            Int64.TryParse(r["b"], out bytes);
            string name = HttpUtility.UrlDecode(r["n"]);
            string changedName = r["cn"];
            if (changedName != null) HttpUtility.UrlDecode(changedName);

            ConfirmUploadEventArgs args = new ConfirmUploadEventArgs(name, changedName, bytes, httpStatus, isError, isLast, --index);

            this.OnConfirmUpload(args);

            var sb = new StringBuilder();
            sb.AppendFormat(@"{{isError:{0},isLast:{1},httpStatus:{2},index:{3},bytes:{4},name:""{5}"", changedName:""{6}""}}",
                            isError.ToString().ToLower(), isLast.ToString().ToLower(), httpStatus, index, bytes, name, changedName);

            HttpContext.Current.Response.Write(sb.ToString());

            this.FlushResponse();
        }

        private string GetJsonByXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool isFirst = true;
            string path = this.AllowPathDefinedByClient ? 
                            HttpUtility.UrlDecode(GetAttributeValue(doc.SelectSingleNode("/fs"), "p", String.Empty))
                            : 
                            this.Path;
            foreach(XmlNode node in doc.SelectNodes("/fs/f"))
            {
                int id = GetAttributeValue(node, "i", 0);
                if (id == 0) continue;
                string name = HttpUtility.UrlDecode(GetAttributeValue(node, "n", String.Empty));
                long bytes = GetAttributeValue(node, "b", (long)0);

                name = this.OnFileNameDetermining(name);

                string contentType = GetContentType(name);

                contentType = this.OnContentTypeDetermining(name, contentType);

                string pathAndName = path + name;

                string policy = this.GetPolicy(pathAndName, contentType, bytes);
                string signiture = this.GetSigniture(policy);
                if (!isFirst) sb.Append(",");
                else isFirst = false;
                if (String.Compare(pathAndName, "crossdomain.xml", true) == 0 && !this.AllowRootCrossDomainXmlUpload)
                {
                    signiture = String.Empty;
                }
                sb.AppendFormat("{{i:'{0}',p:'{1}',s:'{2}',t:'{3}',k:'{4}'}}", id, policy, signiture, contentType, pathAndName.Replace("'", "\'"));
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string GetContentType(string name)
        {
            string extension = System.IO.Path.GetExtension(name);
            string type;
            if (_knownMimeTypes.TryGetValue(extension, out type))
            {
                return type;
            }
            return "application/octet-stream";
        }

        private string GetSigniture(string policy)
        {
            Encoding ae = new UTF8Encoding();
            HMACSHA1 signature = new HMACSHA1(ae.GetBytes(this.SecretKey));
            return Convert.ToBase64String(
                                        signature.ComputeHash(ae.GetBytes(
                                                        policy.ToCharArray()))
                                               );
        }

        private string GetPolicy(string pathAndName, string contentType, long bytes)
        {
            string policy = String.Format(
            @"{{ ""expiration"": ""{0}"",
              ""conditions"": [
                {{""acl"": ""{1}"" }},
                {{""bucket"": ""{2}"" }},
                {{""Content-Type"": ""{3}""}},    
                {{""key"": ""{4}""}},
                {{""x-amz-meta-uuid"": ""{5}""}},
                ['starts-with', '$Filename', ''],
                [""content-length-range"", {6}, {6}]    
              ]
            }}",
               DateTime.UtcNow.AddMinutes(this.UploadValidityExpiresAfterMinutes).ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
               FileAccessToString(this.FileAccess),
               EnsureEscape(this.BucketName),
               EnsureEscape(contentType),
               EnsureEscape(pathAndName),
               EnsureEscape(this.XAmzMetaUuid),
               bytes
               );
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(policy));
        }

        private static string EnsureEscape(string value)
        {
            return String.IsNullOrEmpty(value) ? value : value.Replace(@"""", @"\""");
        }

        private static string FileAccessToString(FileAccess access)
        {
            return access == FileAccess.Private
                       ? "private"
                       :
                           access == FileAccess.PublicRead
                               ? "public-read"
                               :
                                   access == FileAccess.PublicReadWrite
                                       ? "public-read-write"
                                       :
                /*access == FileAccess.AuthenticatedRead*/ "authenticated-read";
        }

        private static T GetAttributeValue<T>(XmlNode node, string attributeName, T defaultValue)
        {
            XmlAttribute attribute = node.Attributes[attributeName];
            if (attribute == null)
            {
                return defaultValue;
            }
            return (T)Convert.ChangeType(attribute.Value, typeof(T));
        }

        public override void WritePreInitializationJavaScript(HtmlTextWriter writer)
        {
            writer.Write("fu.addUploadPreProcessor(Flajaxian.bind({0}.getFileData, {0}));", this.JavascriptID);
            writer.WriteLine();
            if (this.ConfirmUpload != null)
            {
                writer.Write("fu.addStateChangeProcessors(Flajaxian.bind({0}.stateChange, {0}));", this.JavascriptID);
                writer.WriteLine();
            }
        }

        public override void WriteJavaScriptInstantiation(HtmlTextWriter writer)
        {
            writer.Write("{0} = new Flajaxian.DirectAmazonUploader();", this.JavascriptID);
            writer.WriteLine();
            writer.Write(@"{0}.set_url(""{1}"");", this.JavascriptID, EnsureEscape(this.PageUrl));
            writer.WriteLine();
            writer.Write(@"{0}.set_path(""{1}"");", this.JavascriptID, EnsureEscape(this.Path));
            writer.WriteLine();
            writer.Write(@"{0}.set_alc(""{1}"");", this.JavascriptID, FileAccessToString(this.FileAccess));
            writer.WriteLine();
            writer.Write(@"{0}.set_uuid(""{1}"");", this.JavascriptID, EnsureEscape(this.XAmzMetaUuid));
            writer.WriteLine();
            writer.Write(@"{0}.set_awsid(""{1}"");", this.JavascriptID, EnsureEscape(this.AccessKey));
            writer.WriteLine();
            if(!String.IsNullOrEmpty(this.ConfirmUploadJsFunc))
            {
                writer.Write(@"{0}.set_confirmFunc({1});", this.JavascriptID, this.ConfirmUploadJsFunc);
                writer.WriteLine();
            }
            // we do that to check the key exists
            string s = this.SecretKey;
        }

        private void FileUploader_PreRender(object sender, EventArgs e)
        {
            if(this.Parent.RequestAsPostBack)
            {
                throw new ApplicationException(SR.RequestAsPostBackCannotBeTrue);
            }
            HttpRequest r = HttpContext.Current.Request;
            if (r != null)
            {
                if (r["__T"] == "DirAmz.Initial" && r.RequestType == "POST" && r["__ID"] == this.JavascriptID)
                {
                    this.ProcessInitialAjaxCall();
                    return;
                }
                if (r["__T"] == "DirAmz.Confirm" && r.RequestType == "GET" && r["__ID"] == this.JavascriptID)
                {
                    this.ProcessConfirmUploadAjaxCall(r);
                    return;
                }
            }
            this.Parent.UploadDataFieldName = "file";
            this.Parent.SuppressQueryStringParametersOnUploadUrl = true;
            this.Parent.PageUrl = this.BuildAmazonUrl();
            this.Parent.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "com.flajaxian.js.DirectAmazonUploader.js");
        }

        private string BuildAmazonUrl()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.UrlSchemeToAmazon);
            sb.Append(@"://");
            sb.Append(this.BucketName);
            if (!String.IsNullOrEmpty(this.BucketName)) sb.Append(@".");
            sb.Append(@"s3.amazonaws.com/");
            return sb.ToString();
        }

        protected virtual string OnFileNameDetermining(string fileName)
        {
            if (this.FileNameDetermining != null)
            {
                FileNameDeterminingEventArgs args = new FileNameDeterminingEventArgs(null);

                args.FileName = fileName;

                this.FileNameDetermining(this, args);

                return args.FileName;
            }
            return fileName;
        }

        protected virtual string OnContentTypeDetermining(string fileName, string contentType)
        {
            if(this.ContentTypeDetermining != null)
            {
                ContentTypeDeterminingEventArgs args = new ContentTypeDeterminingEventArgs();
                args.ContentType = contentType;
                args.FileName = fileName;

                this.ContentTypeDetermining(this, args);

                return args.ContentType;
            }

            return contentType;
        } 

        protected virtual void OnConfirmUpload(ConfirmUploadEventArgs args)
        {
            if(this.ConfirmUpload != null)
            {
                this.ConfirmUpload(this, args);
            }
        }

        private string ComposePageUrl(string pageUrl)
        {
            StringBuilder sb = new StringBuilder(pageUrl);
            string idTemplate = null;
            if (!pageUrl.Contains("?"))
            {
                idTemplate = "?__ID={0}&";
            }
            else if (pageUrl.EndsWith("?") || pageUrl.EndsWith("&"))
            {
                idTemplate = "__ID={0}&";
            }
            else
            {
                idTemplate = "&__ID={0}&";
            }
            sb.AppendFormat(idTemplate, this.JavascriptID);
            return sb.ToString();
        }

        private void CheckParameter(string parameter, string errorMessage, string parameterName)
        {
            if (String.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException(errorMessage, parameterName);
            }
        }

        private string CheckForWebConfigEntry(string code, string errorMessage, string parameterName)
        {
            this.CheckParameter(code, errorMessage, parameterName);
            if (code.StartsWith(WebConfigPrefix))
            {
                code = ConfigurationManager.AppSettings[System.Text.RegularExpressions.Regex.Split(code, WebConfigPrefix)[1]];
                this.CheckParameter(code, errorMessage, parameterName);
            }
            return code;
        }
    }
}
