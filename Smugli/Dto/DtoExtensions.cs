namespace Smugli.Dto
{
    #region Using Directives

    using Nancy.Json;

    #endregion

    public static class DtoExtensions
    {
        public static string ToJson(this IDto dto)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(dto);
        }
    }
}