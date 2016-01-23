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
        private string pathToServe = ".\\wwwroot\\";

        public Task<IHttpResponse> HandleRequest(HttpRequest request)
        {
            var filePath = pathToServe + request.Uri;

            if (request.Uri.ToString() == "/")
            {
                filePath = pathToServe + "/index.html";
            }

            if (!File.Exists(filePath))
            {
                return FileNotFoundResponse();
            }

            var file = File.OpenText(filePath);
            var fileString = file.ReadToEnd();
            file.Dispose();

            var contentType = "text/html";


            if (filePath.Contains(".css"))
            {
                contentType = "text/css";
            }
            else if (filePath.Contains(".ico"))
            {
                contentType = "image/x-icon";
            }
            else if (filePath.Contains(".js"))
            {
                contentType = "application/javascript";
            }

            return GetHttpResponseForFile(fileString, contentType);
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
