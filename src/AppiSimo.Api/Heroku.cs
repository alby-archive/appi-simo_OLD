namespace AppiSimo.Api
{
    using System;
    using Shared.Environment;

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

            var connectionString = url.UserInfo.Split(separator: ':');

            return connectionString.Length != 2
                ? null
                : $"Host={url.Host}; Port={url.Port}; Username={connectionString[0]}; Password={connectionString[1]}; Database={url.LocalPath.Substring(startIndex: 1)}; Pooling=true;";
        }

        public static Authority TryParseAuthority(string value)
        {
            var authority = value?.Split(separator: ';');

            return authority?.Length != 2
                ? null
                : new Authority
                {
                    EndPoint = authority[0],
                    Audiences = authority[1]
                };
        }

        public static IdentityAccessManagement TryParseIdentityAccessManagement(string value)
        {
            var iam = value?.Split(separator: ';');

            return iam?.Length != 2
                ? null
                : new IdentityAccessManagement
                {
                    AccessKeyId = iam[0],
                    SecretAccessKey = iam[1]
                };
        }
    }
}