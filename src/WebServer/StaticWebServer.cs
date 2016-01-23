using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Models.Contracts;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest;
using Devkoes.Restup.WebServer.Static;

namespace Devkoes.Restup.WebServer
{
    public class StaticWebServer : HttpServer
    {
        private readonly StaticFileRequestHandler _staticFileRequestHandler;

        public StaticWebServer(int port, string pathToServe = ".\\wwwroot\\") : base(port)
        {
            _staticFileRequestHandler = new StaticFileRequestHandler();
        }

        public StaticWebServer(int port) : this(port, null) { }

        public StaticWebServer() : this(8800, null) { }

        internal override async Task<IHttpResponse> HandleRequest(HttpRequest request)
        {
            return await _staticFileRequestHandler.HandleRequest(request);
        }
    }
}
