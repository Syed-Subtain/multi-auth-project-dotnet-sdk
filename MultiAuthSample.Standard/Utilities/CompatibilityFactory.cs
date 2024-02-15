// <copyright file="CompatibilityFactory.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>

namespace MultiAuthSample.Standard.Utilities
{
    using APIMatic.Core.Types.Sdk;
    using APIMatic.Core.Utilities;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Http.Request;
    using MultiAuthSample.Standard.Http.Response;

    internal class CompatibilityFactory : ICompatibilityFactory<HttpRequest, HttpResponse, HttpContext, ApiException>
    {
        public ApiException CreateApiException(string reason, CoreContext<CoreRequest, CoreResponse> context) =>
            new ApiException(reason, CreateHttpContext(context.Request, context.Response));

        public HttpContext CreateHttpContext(CoreRequest request, CoreResponse response) =>
            new HttpContext(CreateHttpRequest(request), CreateHttpResponse(response));

        public HttpRequest CreateHttpRequest(CoreRequest request) =>
            new HttpRequest(request.HttpMethod, request.QueryUrl, request.Headers, request.Body,
                request.FormParameters, request.Username, request.Password, request.QueryParameters);

        public HttpResponse CreateHttpResponse(CoreResponse response)
        {
            if (response.Body != null)
            {
                return new HttpStringResponse(response.StatusCode, response.Headers, response.RawBody, response.Body);
            }
            return new HttpResponse(response.StatusCode, response.Headers, response.RawBody);
        }
    }
}