#region Imports

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Devkoes.Restup.WebServer.Static
{
    public static class MimeMap
    {
        public const string ApplicationOctetStream = "application/octet-stream";

        private static readonly Dictionary<string, string> _map;

        static MimeMap()
        {
            _map = CreateDefaultMap();
        }

        public static string ContentTypeFromPath(string path)
        {
            return ContentTypeFromExtension(Path.GetExtension(path));
        }

        public static string ContentTypeFromExtension(string extension)
        {
            extension = extension ?? string.Empty;

            if (extension.Length > 0)
            {
                if (extension[0] != '.')
                    throw new ArgumentOutOfRangeException("extension");

                if (extension.Length == 1)
                    extension = string.Empty;
            }

            var contentType = _map[extension];
            return !string.IsNullOrEmpty(contentType) ? contentType : ApplicationOctetStream;
        }

        #region Default MIME map

        private static Dictionary<string, string> CreateDefaultMap()
        {
            var map = new Dictionary<string, string>(
                /* capacity */ 220,
                    /* comparer */ StringComparer.OrdinalIgnoreCase)
            {
                { ".323"    , "text/h323" },
                { ".asx"    , "video/x-ms-asf" },
                { ".acx"    , "application/internet-property-stream" },
                { ".ai"     , "application/postscript" },
                { ".aif"    , "audio/x-aiff" },
                { ".aiff"   , "audio/aiff" },
                { ".axs"    , "application/olescript" },
                { ".aifc"   , "audio/aiff" },
                { ".asr"    , "video/x-ms-asf" },
                { ".avi"    , "video/x-msvideo" },
                { ".asf"    , "video/x-ms-asf" },
                { ".au"     , "audio/basic" },
                { ".bin"    , "application/octet-stream" },
                { ".bas"    , "text/plain" },
                { ".bcpio"  , "application/x-bcpio" },
                { ".bmp"    , "image/bmp" },
                { ".cdf"    , "application/x-cdf" },
                { ".cat"    , "application/vndms-pkiseccat" },
                { ".crt"    , "application/x-x509-ca-cert" },
                { ".c"      , "text/plain" },
                { ".css"    , "text/css" },
                { ".cer"    , "application/x-x509-ca-cert" },
                { ".crl"    , "application/pkix-crl" },
                { ".cmx"    , "image/x-cmx" },
                { ".csh"    , "application/x-csh" },
                { ".cod"    , "image/cis-cod" },
                { ".cpio"   , "application/x-cpio" },
                { ".clp"    , "application/x-msclip" },
                { ".crd"    , "application/x-mscardfile" },
                { ".dll"    , "application/x-msdownload" },
                { ".dot"    , "application/msword" },
                { ".doc"    , "application/msword" },
                { ".dvi"    , "application/x-dvi" },
                { ".dir"    , "application/x-director" },
                { ".dxr"    , "application/x-director" },
                { ".der"    , "application/x-x509-ca-cert" },
                { ".dib"    , "image/bmp" },
                { ".dcr"    , "application/x-director" },
                { ".disco"  , "text/xml" },
                { ".exe"    , "application/octet-stream" },
                { ".etx"    , "text/x-setext" },
                { ".evy"    , "application/envoy" },
                { ".eml"    , "message/rfc822" },
                { ".eps"    , "application/postscript" },
                { ".flr"    , "x-world/x-vrml" },
                { ".fif"    , "application/fractals" },
                { ".gtar"   , "application/x-gtar" },
                { ".gif"    , "image/gif" },
                { ".gz"     , "application/x-gzip" },
                { ".hta"    , "application/hta" },
                { ".htc"    , "text/x-component" },
                { ".htt"    , "text/webviewhtml" },
                { ".h"      , "text/plain" },
                { ".hdf"    , "application/x-hdf" },
                { ".hlp"    , "application/winhlp" },
                { ".html"   , "text/html" },
                { ".htm"    , "text/html" },
                { ".hqx"    , "application/mac-binhex40" },
                { ".isp"    , "application/x-internet-signup" },
                { ".iii"    , "application/x-iphone" },
                { ".ief"    , "image/ief" },
                { ".ivf"    , "video/x-ivf" },
                { ".ins"    , "application/x-internet-signup" },
                { ".ico"    , "image/x-icon" },
                { ".jpg"    , "image/jpeg" },
                { ".jfif"   , "image/pjpeg" },
                { ".jpe"    , "image/jpeg" },
                { ".jpeg"   , "image/jpeg" },
                { ".js"     , "application/x-javascript" },
                { ".lsx"    , "video/x-la-asf" },
                { ".latex"  , "application/x-latex" },
                { ".lsf"    , "video/x-la-asf" },
                { ".mhtml"  , "message/rfc822" },
                { ".mny"    , "application/x-msmoney" },
                { ".mht"    , "message/rfc822" },
                { ".mid"    , "audio/mid" },
                { ".mpv2"   , "video/mpeg" },
                { ".man"    , "application/x-troff-man" },
                { ".mvb"    , "application/x-msmediaview" },
                { ".mpeg"   , "video/mpeg" },
                { ".m3u"    , "audio/x-mpegurl" },
                { ".mdb"    , "application/x-msaccess" },
                { ".mpp"    , "application/vnd.ms-project" },
                { ".m1v"    , "video/mpeg" },
                { ".mpa"    , "video/mpeg" },
                { ".me"     , "application/x-troff-me" },
                { ".m13"    , "application/x-msmediaview" },
                { ".movie"  , "video/x-sgi-movie" },
                { ".m14"    , "application/x-msmediaview" },
                { ".mpe"    , "video/mpeg" },
                { ".mp2"    , "video/mpeg" },
                { ".mov"    , "video/quicktime" },
                { ".mp3"    , "audio/mpeg" },
                { ".mpg"    , "video/mpeg" },
                { ".ms"     , "application/x-troff-ms" },
                { ".nc"     , "application/x-netcdf" },
                { ".nws"    , "message/rfc822" },
                { ".oda"    , "application/oda" },
                { ".ods"    , "application/oleobject" },
                { ".pmc"    , "application/x-perfmon" },
                { ".p7r"    , "application/x-pkcs7-certreqresp" },
                { ".p7b"    , "application/x-pkcs7-certificates" },
                { ".p7s"    , "application/pkcs7-signature" },
                { ".pmw"    , "application/x-perfmon" },
                { ".ps"     , "application/postscript" },
                { ".p7c"    , "application/pkcs7-mime" },
                { ".pbm"    , "image/x-portable-bitmap" },
                { ".ppm"    , "image/x-portable-pixmap" },
                { ".pub"    , "application/x-mspublisher" },
                { ".png"    , "image/png" },
                { ".pnm"    , "image/x-portable-anymap" },
                { ".pml"    , "application/x-perfmon" },
                { ".p10"    , "application/pkcs10" },
                { ".pfx"    , "application/x-pkcs12" },
                { ".p12"    , "application/x-pkcs12" },
                { ".pdf"    , "application/pdf" },
                { ".pps"    , "application/vnd.ms-powerpoint" },
                { ".p7m"    , "application/pkcs7-mime" },
                { ".pko"    , "application/vndms-pkipko" },
                { ".ppt"    , "application/vnd.ms-powerpoint" },
                { ".pmr"    , "application/x-perfmon" },
                { ".pma"    , "application/x-perfmon" },
                { ".pot"    , "application/vnd.ms-powerpoint" },
                { ".prf"    , "application/pics-rules" },
                { ".pgm"    , "image/x-portable-graymap" },
                { ".qt"     , "video/quicktime" },
                { ".ra"     , "audio/x-pn-realaudio" },
                { ".rgb"    , "image/x-rgb" },
                { ".ram"    , "audio/x-pn-realaudio" },
                { ".rmi"    , "audio/mid" },
                { ".ras"    , "image/x-cmu-raster" },
                { ".roff"   , "application/x-troff" },
                { ".rtf"    , "application/rtf" },
                { ".rtx"    , "text/richtext" },
                { ".sv4crc" , "application/x-sv4crc" },
                { ".spc"    , "application/x-pkcs7-certificates" },
                { ".setreg" , "application/set-registration-initiation" },
                { ".snd"    , "audio/basic" },
                { ".stl"    , "application/vndms-pkistl" },
                { ".setpay" , "application/set-payment-initiation" },
                { ".stm"    , "text/html" },
                { ".shar"   , "application/x-shar" },
                { ".sh"     , "application/x-sh" },
                { ".sit"    , "application/x-stuffit" },
                { ".spl"    , "application/futuresplash" },
                { ".sct"    , "text/scriptlet" },
                { ".scd"    , "application/x-msschedule" },
                { ".sst"    , "application/vndms-pkicertstore" },
                { ".src"    , "application/x-wais-source" },
                { ".sv4cpio", "application/x-sv4cpio" },
                { ".tex"    , "application/x-tex" },
                { ".tgz"    , "application/x-compressed" },
                { ".t"      , "application/x-troff" },
                { ".tar"    , "application/x-tar" },
                { ".tr"     , "application/x-troff" },
                { ".tif"    , "image/tiff" },
                { ".txt"    , "text/plain" },
                { ".texinfo", "application/x-texinfo" },
                { ".trm"    , "application/x-msterminal" },
                { ".tiff"   , "image/tiff" },
                { ".tcl"    , "application/x-tcl" },
                { ".texi"   , "application/x-texinfo" },
                { ".tsv"    , "text/tab-separated-values" },
                { ".ustar"  , "application/x-ustar" },
                { ".uls"    , "text/iuls" },
                { ".vcf"    , "text/x-vcard" },
                { ".wps"    , "application/vnd.ms-works" },
                { ".wav"    , "audio/wav" },
                { ".wrz"    , "x-world/x-vrml" },
                { ".wri"    , "application/x-mswrite" },
                { ".wks"    , "application/vnd.ms-works" },
                { ".wmf"    , "application/x-msmetafile" },
                { ".wcm"    , "application/vnd.ms-works" },
                { ".wrl"    , "x-world/x-vrml" },
                { ".wdb"    , "application/vnd.ms-works" },
                { ".wsdl"   , "text/xml" },
                { ".xml"    , "text/xml" },
                { ".xlm"    , "application/vnd.ms-excel" },
                { ".xaf"    , "x-world/x-vrml" },
                { ".xla"    , "application/vnd.ms-excel" },
                { ".xls"    , "application/vnd.ms-excel" },
                { ".xof"    , "x-world/x-vrml" },
                { ".xlt"    , "application/vnd.ms-excel" },
                { ".xlc"    , "application/vnd.ms-excel" },
                { ".xsl"    , "text/xml" },
                { ".xbm"    , "image/x-xbitmap" },
                { ".xlw"    , "application/vnd.ms-excel" },
                { ".xpm"    , "image/x-xpixmap" },
                { ".xwd"    , "image/x-xwindowdump" },
                { ".xsd"    , "text/xml" },
                { ".z"      , "application/x-compress" },
                { ".zip"    , "application/x-zip-compressed" },
                { ".*"      , "application/octet-stream" },
                // Office 2007 MIME types
                // http://www.bram.us/2007/05/25/office-2007-mime-types-for-iis/
                { ".docm"   , "application/vnd.ms-word.document.macroEnabled.12" },
                { ".docx"   , "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".dotm"   , "application/vnd.ms-word.template.macroEnabled.12" },
                { ".dotx"   , "application/vnd.openxmlformats-officedocument.wordprocessingml.template" },
                { ".potm"   , "application/vnd.ms-powerpoint.template.macroEnabled.12" },
                { ".potx"   , "application/vnd.openxmlformats-officedocument.presentationml.template" },
                { ".ppam"   , "application/vnd.ms-powerpoint.addin.macroEnabled.12" },
                { ".ppsm"   , "application/vnd.ms-powerpoint.slideshow.macroEnabled.12" },
                { ".ppsx"   , "application/vnd.openxmlformats-officedocument.presentationml.slideshow" },
                { ".pptm"   , "application/vnd.ms-powerpoint.presentation.macroEnabled.12" },
                { ".pptx"   , "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
                { ".xlam"   , "application/vnd.ms-excel.addin.macroEnabled.12" },
                { ".xlsb"   , "application/vnd.ms-excel.sheet.binary.macroEnabled.12" },
                { ".xlsm"   , "application/vnd.ms-excel.sheet.macroEnabled.12" },
                { ".xlsx"   , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".xltm"   , "application/vnd.ms-excel.template.macroEnabled.12" },
                { ".xltx"   , "application/vnd.openxmlformats-officedocument.spreadsheetml.template" },
            };

            //
            // NOTE! If you add more MIME mappings here, do not forget to
            // update the capacity of the hashtable.
            //

            return map;
        }

        #endregion
    }
}