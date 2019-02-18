namespace AppiSimo.Api
{
    using System;

    public static class Heroku
    {
        public static string TryParseConnectionString(string value)
        {
            if (value == null)
            {
                return null;
            }

            if (!Uri.TryCreate(value, UriKind.Absolute, out var url))
            {
                return null;
            }

            var userInfo = url.UserInfo.Split(separator: ':');

            return userInfo.Length != 2
                ? null
                : $"Host={url.Host}; Port={url.Port}; Username={userInfo[0]}; Password={userInfo[1]}; Database={url.LocalPath.Substring(startIndex: 1)}; Pooling=true;";
        }
    }
}