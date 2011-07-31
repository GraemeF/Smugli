namespace Smugli
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Nancy;
    using Nancy.Responses;

    #endregion

    public static class ResponseExtensions
    {
        public static Response AsBody<TModel>(this IResponseFormatter formatter, Request request, TModel model)
        {
            return request.AsksForJsonp()
                       ? formatter.AsJsonp(model, (string)request.Query.callback)
                       : request.AsksForJson()
                             ? formatter.AsJson(model)
                             : formatter.AsXml(model);
        }

        public static Response AsJsonp<TModel>(this IResponseFormatter formatter, TModel model, string jsonCallback)
        {
            return new JsonpResponse<TModel>(model, jsonCallback);
        }

        public static Response AsString(this IResponseFormatter formatter, string body)
        {
            return new Response
                       {
                           Contents = stream => WriteContentToStream(stream, body)
                       };
        }

        public static Response CacheableFor(this Response response, TimeSpan timeSpan)
        {
            response.Headers["Cache-Control"] = string.Format("max-age={0}", (int)timeSpan.TotalSeconds);
            return response;
        }

        public static string IfNoneMatch(this Request request)
        {
            IEnumerable<string> value = request.Headers["If-None-Match"].ToList();
            return value.Any()
                       ? string.Join("", value)
                       : null;
        }

        public static Response Uncacheable(this Response response)
        {
            response.Headers["Cache-Control"] = "No-cache";
            return response;
        }

        public static Response WithETag(this Response response, string etag)
        {
            response.Headers["ETag"] = etag;
            return response;
        }

        public static Response WithStatusCode(this Response response, HttpStatusCode statusCode)
        {
            response.StatusCode = statusCode;
            return response;
        }

        private static Response AsJson<TModel>(this IResponseFormatter formatter, TModel model)
        {
            Response response = FormatterExtensions.AsJson(formatter, model);
            response.ContentType = "application/json";

            return response;
        }

        private static Response AsXml<TModel>(this IResponseFormatter formatter, TModel model)
        {
            return new XmlResponse<TModel>(model, "application/xml");
        }

        private static void WriteContentToStream(Stream stream, string content)
        {
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
        }
    }
}