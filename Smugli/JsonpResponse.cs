namespace Smugli
{
    #region Using Directives

    using System;
    using System.IO;

    using Nancy;
    using Nancy.Json;
    using Nancy.Responses;

    #endregion

    public class JsonpResponse<TModel> : Response
    {
        public JsonpResponse(TModel model, string jsonpCallback)
        {
            Contents = GetJsonContents(model, jsonpCallback);
            ContentType = "application/json";
            StatusCode = HttpStatusCode.OK;
        }

        private static Action<Stream> GetJsonContents(TModel model, string jsonpCallback)
        {
            return stream =>
                {
                    var serializer = new JavaScriptSerializer
                                         {
                                             MaxJsonLength = JsonSettings.MaxJsonLength,
                                             RecursionLimit = JsonSettings.MaxRecursions
                                         };
                    string json = string.Format("{0}({1});", jsonpCallback, serializer.Serialize(model));

                    var writer = new StreamWriter(stream);
                    writer.Write(json);
                    writer.Flush();
                };
        }
    }
}