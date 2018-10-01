﻿namespace SIS.HTTP.Requests
{
    using Enums;
    using Exceptions;
    using Extensions;
    using Headers;
    using Headers.Contracts;
    using Requests.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] requestLine = splitRequestContent[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

        private void ParseRequestParameters(string bodyParameters)
        {
            this.ParseQueryParameters(this.Url);

            this.ParseFormDataParameters(bodyParameters);

        }

        private void ParseFormDataParameters(string bodyParameters)
        {
            string bodyKey;
            string bodyValue;

            if (bodyParameters.Contains('&'))
            {
                var splittedBody = bodyParameters.Split('&');

                foreach (var bodyKeyValuePairs in splittedBody)
                {
                    var bodyPair = bodyKeyValuePairs.Split('=');

                    bodyKey = bodyPair[0];
                    bodyValue = bodyPair[1];

                    this.FormData.Add(bodyKey, bodyValue);
                }
            }
            else
            {
                var bodyPair = bodyParameters.Split('=');

                bodyKey = bodyPair[0];
                bodyValue = bodyPair[1];

                this.FormData.Add(bodyKey, bodyValue);
            }
        }

        private void ParseQueryParameters(string url)
        {
            if (url.Contains('?') || (url.Contains('?') && url.Contains('#')))
            {
                var queryParameters = url?.Split(new[] { '?', '#' })
                                      .Skip(1)
                                      .ToArray()[0];

                if (!this.IsValidRequestQueryString(queryParameters))
                {
                    throw new BadRequestException();
                }

                var queryKeyValuePairs = queryParameters.Split('&', StringSplitOptions.RemoveEmptyEntries);

                foreach (var query in queryKeyValuePairs)
                {
                    var splitQuery = query.Split('=');

                    if (splitQuery.Length != 2)
                    {
                        throw new BadRequestException();
                    }

                    var queryKey = splitQuery[0];
                    var queryValue = splitQuery[1];

                    if (!this.QueryData.ContainsKey(queryKey))
                    {
                        this.QueryData.Add(queryKey, queryValue);
                    }
                }
            }
        }

        private bool IsValidRequestQueryString(string queryParameters)
        {
            if (string.IsNullOrEmpty(queryParameters) || !queryParameters.Contains('='))
            {
                return false;
            }

            return true;
        }

        private void ParseHeaders(string[] headers)
        {
            foreach (var headerString in headers)
            {
                if (!string.IsNullOrEmpty(headerString))
                {
                    var splittedHeader = headerString.Split(": ");

                    var headerKey = splittedHeader[0];
                    var headerValue = splittedHeader[1];

                    var header = new HttpHeader(headerKey, headerValue);

                    this.Headers.Add(header);
                }
            }
        }

        private void ParseRequestPath()
        {
            var path = this.Url.Split('?').FirstOrDefault();

            if (string.IsNullOrEmpty(path))
            {
                throw new BadRequestException();
            }

            this.Path = path;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            var requestMethodString = requestLine[0].Capitalize();

            HttpRequestMethod requestMethod;

            bool successParseMethod = Enum.TryParse<HttpRequestMethod>(requestMethodString, out requestMethod);

            if (!successParseMethod)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = requestMethod;
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length == 3 && requestLine[2] == "HTTP/1.1")
            {
                return true;
            }

            return false;
        }
    }
}