using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Models.Contracts;
using Devkoes.Restup.WebServer.Models.Schemas;

namespace Devkoes.Restup.WebServer.Static
{
    class StaticFileRequestHandler
    {
        private readonly DirectoryInfo _pathToServe = new DirectoryInfo(".\\wwwroot\\");

        public Task<IHttpResponse> HandleRequest(HttpRequest request)
        {
            var uriPath = request.Uri.ToString();

            if (uriPath.Contains("&"))
            {
                uriPath = uriPath.Split('&').First();
            }

            if (uriPath == "/")
            {
                uriPath = "index.html";
            }

            var fileUri = new Uri(_pathToServe.FullName + uriPath);

            if (!fileUri.IsFile)
            {
                return FileNotFoundResponse();
            }

            var file = File.OpenText(fileUri.LocalPath);
            var fileString = file.ReadToEnd();
            file.Dispose();

            var mimeType = MimeMap.ContentTypeFromPath(fileUri.LocalPath);

            return GetHttpResponseForFile(fileString, mimeType);
        }

        private Task<IHttpResponse> FileNotFoundResponse()
        {

            var header = "HTTP/1.1 404 Not Found\r\n" +
                        $"Content-Length: 0\r\n" +
                        "Connection: close\r\n\r\n";

            return Task.Factory.StartNew<IHttpResponse>(() => new HttpResponse(header, Encoding.UTF8.GetBytes(header)));
        }

        private Task<IHttpResponse> GetHttpResponseForFile(string file, string contentType)
        {
            var header = "HTTP/1.1 200 OK\r\n" +
                        $"Content-Length: {file.Length}\r\n" +
                        $"Content-Type: {contentType}\r\n" +
                        "Connection: close\r\n\r\n";

            var res = header + file;

            return Task.Factory.StartNew<IHttpResponse>(() => new HttpResponse(res, Encoding.UTF8.GetBytes(res)));
        }
    }
}
