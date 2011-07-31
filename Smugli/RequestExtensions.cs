namespace Smugli
{
    #region Using Directives

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using Nancy;
    using Nancy.IO;
    using Nancy.Json;

    using Smugli.Dto;

    #endregion

    public static class RequestExtensions
    {
        public static string AsString(this RequestStream requestStream)
        {
            return new StreamReader(requestStream).ReadToEnd();
        }

        public static XDocument AsXml(this RequestStream requestStream)
        {
            return XDocument.Load(requestStream);
        }

        public static bool AsksForJson(this Request request)
        {
            return request.AsksFor(Schema.ContentTypes.JsonContentTypes);
        }

        public static bool AsksForJsonp(this Request request)
        {
            return request.AsksFor(Schema.ContentTypes.JsonpContentTypes) ||
                   request.QueryParameterNames().Contains("callback");
        }

        public static TDto BodyAsDto<TDto>(this Request request)
        {
            var bodyReader = new StreamReader(request.Body);
            return request.SuppliesJson()
                       ? new JavaScriptSerializer().Deserialize<TDto>(bodyReader.ReadToEnd())
                       : (TDto)new XmlSerializer(typeof(TDto)).Deserialize(bodyReader);
        }

        public static IEnumerable<string> QueryParameterNames(this Request request)
        {
            return request.Query.GetDynamicMemberNames();
        }

        public static bool Supplies(this Request request, IEnumerable<string> contentTypes)
        {
            return ContentTypesMatchAnyRequestHeader(request.Headers["Content-Type"], contentTypes);
        }

        public static bool SuppliesJson(this Request request)
        {
            return request.Supplies(Schema.ContentTypes.JsonContentTypes);
        }

        private static bool AnyContentTypeIsAccepted(IEnumerable<string> contentTypes, string acceptedType)
        {
            return contentTypes.Any(acceptedType.Contains);
        }

        private static bool AsksFor(this Request request, IEnumerable<string> contentTypes)
        {
            return ContentTypesMatchAnyRequestHeader(request.Headers["Accept"], contentTypes);
        }

        private static IEnumerable<string> ContentTypesInAcceptHeader(string acceptHeaderValue)
        {
            return acceptHeaderValue.Split(',');
        }

        private static bool ContentTypesMatchAnyRequestHeader(IEnumerable<string> headerValues,
                                                              IEnumerable<string> contentTypes)
        {
            return headerValues
                .Any(acceptHeaderValue => ContentTypesInAcceptHeader(acceptHeaderValue)
                                              .Any(acceptedType => AnyContentTypeIsAccepted(contentTypes,
                                                                                            acceptedType)));
        }
    }
}