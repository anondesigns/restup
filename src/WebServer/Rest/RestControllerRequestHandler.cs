﻿using Devkoes.HttpMessage.Models.Schemas;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.InstanceCreators;
using Devkoes.Restup.WebServer.Models.Contracts;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Devkoes.Restup.WebServer.Rest
{
    internal class RestControllerRequestHandler
    {
        private List<RestControllerMethodInfo> _restMethodCollection;
        private RestResponseFactory _responseFactory;
        private RestControllerMethodExecutorFactory _methodExecuteFactory;
        private string _urlPrefix;

        internal RestControllerRequestHandler(string urlPrefix)
        {
            _urlPrefix = urlPrefix;
            _restMethodCollection = new List<RestControllerMethodInfo>();
            _responseFactory = new RestResponseFactory();
            _methodExecuteFactory = new RestControllerMethodExecutorFactory();
        }

        internal void RegisterController<T>() where T : class
        {
            RegisterController<T>(() => Enumerable.Empty<object>().ToArray());
        }

        internal void RegisterController<T>(Func<object[]> constructorArgs) where T : class
        {
            _restMethodCollection.AddRange(GetRestMethods<T>(constructorArgs));

            InstanceCreatorCache.Default.CacheCreator(typeof(T));
        }

        internal IEnumerable<RestControllerMethodInfo> GetRestMethods<T>(Func<object[]> constructorArgs) where T : class
        {
            var restMethods = new List<RestControllerMethodInfo>();

            var allPublicAsyncRestMethods = GetRestAsyncMethodDefinitions<T>();
            var allPublicRestMethods = GetRestMethodDefinitions<T>();

            foreach (var methodDef in allPublicRestMethods)
                restMethods.Add(new RestControllerMethodInfo(methodDef, _urlPrefix, constructorArgs));

            foreach (var methodDef in allPublicAsyncRestMethods)
                restMethods.Add(new RestControllerMethodInfo(methodDef, _urlPrefix, constructorArgs, true));

            Debug.WriteLine(string.Join(Environment.NewLine, restMethods));

            return restMethods;
        }

        internal IEnumerable<MethodInfo> GetRestMethodDefinitions<T>()
        {
            var allPublicRestMethods =
               from m in typeof(T).GetRuntimeMethods()
               where
                   m.IsPublic &&
                   m.IsDefined(typeof(UriFormatAttribute)) &&
                   HasRestResponse(m)
               select m;

            return allPublicRestMethods.ToArray();
        }

        internal IEnumerable<MethodInfo> GetRestAsyncMethodDefinitions<T>()
        {
            var allPublicRestMethods =
               from m in typeof(T).GetRuntimeMethods()
               where
                   m.IsPublic &&
                   m.IsDefined(typeof(UriFormatAttribute)) &&
                   HasAsyncRestResponse(m)
               select m;

            return allPublicRestMethods.ToArray();
        }

        internal async Task<IRestResponse> HandleRequest(RestServerRequest req)
        {
            if (!req.HttpServerRequest.IsComplete ||
                req.HttpServerRequest.Method == HttpMethod.Unsupported)
            {
                return _responseFactory.CreateBadRequest();
            }

            var restMethods = _restMethodCollection.Where(r => r.Match(req.HttpServerRequest.Uri));
            if (!restMethods.Any())
            {
                return _responseFactory.CreateBadRequest();
            }

            var restMethod = restMethods.SingleOrDefault(r => r.Verb == req.HttpServerRequest.Method);
            if (restMethod == null)
            {
                return new MethodNotAllowedResponse(restMethods.Select(r => r.Verb));
            }

            var restMethodExecutor = _methodExecuteFactory.Create(restMethod);

            try
            {
                return await restMethodExecutor.ExecuteMethodAsync(restMethod, req);
            }
            catch
            {
                return _responseFactory.CreateBadRequest();
            }
        }

        private static bool HasAsyncRestResponse(MethodInfo m)
        {
            var isTask = m.ReturnType.GetTypeInfo().IsSubclassOf(typeof(Task));
            if (!isTask)
                return false;

            var genericArgs = m.ReturnType.GetGenericArguments();
            if (!genericArgs.Any())
                return false;

            return genericArgs[0].GetTypeInfo().ImplementedInterfaces.Contains(typeof(IRestResponse));
        }

        private static bool HasRestResponse(MethodInfo m)
        {
            return m.ReturnType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IRestResponse));
        }
    }
}
