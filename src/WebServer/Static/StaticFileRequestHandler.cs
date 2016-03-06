using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.HttpMessage;
using Devkoes.HttpMessage.Models.Schemas;
using Devkoes.HttpMessage.Plumbing;

namespace Devkoes.Restup.WebServer.Static
{
    class StaticFileRequestHandler
    {
        private readonly DirectoryInfo _pathToServe = new DirectoryInfo(".\\wwwroot\\");

        public Task<HttpServerResponse> HandleRequest(HttpServerRequest request)
        {
            var uriPath = request.Uri.ToString();

            if (uriPath.Contains("?"))
            {
                uriPath = uriPath.Split('?').First();
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

            var fileExtension = Path.GetExtension(fileUri.LocalPath);
            var mediaType = HttpCodesTranslator.Default.GetMediaTypeForFileExtension(fileExtension);

            return GetHttpResponseForFile(fileString, mediaType);
        }

        private Task<HttpServerResponse> FileNotFoundResponse()
        {
            return Task.Factory.StartNew(() => HttpServerResponse.Create(HttpResponseStatus.NotFound));
        }

        private Task<HttpServerResponse> GetHttpResponseForFile(string data, MediaType mediaType)
        {
            var response = HttpServerResponse.Create(HttpResponseStatus.OK);
            response.Content = Encoding.UTF8.GetBytes(data);
            response.ContentType = mediaType;

            return Task.Factory.StartNew(() => response);
        }
    }
}
