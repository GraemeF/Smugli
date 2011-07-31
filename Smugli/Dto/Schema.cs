namespace Smugli.Dto
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public static class Schema
    {
        public static class ContentTypes
        {
            public const string GenericJson = "application/json";

            public static IEnumerable<string> JsonContentTypes =
                new[] { GenericJson };

            public static IEnumerable<string> JsonpContentTypes;
        }
    }
}