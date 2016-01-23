using Devkoes.HttpMessage;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using System;
using System.Threading.Tasks;
using Devkoes.HttpMessage.Models.Schemas;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Static;

namespace Devkoes.Restup.WebServer
{
    public class RestWebServer : HttpServer
    {
        private RestControllerRequestHandler _requestHandler;
        private RestToHttpResponseConverter _restToHttpConverter;
        private StaticFileRequestHandler _staticFileRequestHandler;
        private RestServerRequestFactory _restServerRequestFactory;

        public RestWebServer(int port, string urlPrefix) : base(port)
        {
            var fixedFormatUrlPrefix = urlPrefix.FormatRelativeUri();

            _restServerRequestFactory = new RestServerRequestFactory();
            _requestHandler = new RestControllerRequestHandler(fixedFormatUrlPrefix);
            _restToHttpConverter = new RestToHttpResponseConverter();
            _staticFileRequestHandler = new StaticFileRequestHandler();
        }

        public RestWebServer(int port) : this(port, null) { }

        public RestWebServer() : this(8800, null) { }

        public void RegisterController<T>() where T : class
        {
            _requestHandler.RegisterController<T>();
        }

        public void RegisterController<T>(params object[] args) where T : class
        {
            _requestHandler.RegisterController<T>(() => args);
        }

        public void RegisterController<T>(Func<object[]> args) where T : class
        {
            _requestHandler.RegisterController<T>(args);
        }

        internal override async Task<HttpServerResponse> HandleRequest(HttpServerRequest request)
        {
            var restServerRequest = _restServerRequestFactory.Create(request);

            var restResponse = await _requestHandler.HandleRequest(restServerRequest);

            if (request.Method == HttpMethod.GET && restResponse.StatusCode != 200)
            {
                return await _staticFileRequestHandler.HandleRequest(request);
            }

            var httpResponse = restResponse.Visit(_restToHttpConverter, request);

            return httpResponse;
        }
    }
}
