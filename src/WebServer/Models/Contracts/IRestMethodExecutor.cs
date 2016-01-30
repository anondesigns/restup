﻿using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest;
using System.Threading.Tasks;

namespace Devkoes.Restup.WebServer.Models.Contracts
{
    interface IRestMethodExecutor
    {
        Task<IRestResponse> ExecuteMethodAsync(RestControllerMethodInfo info, RestServerRequest request);
    }
}
