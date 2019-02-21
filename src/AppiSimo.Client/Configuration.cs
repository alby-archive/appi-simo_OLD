namespace AppiSimo.Client
{
    using System;

    public class Configuration
    {
        public Configuration(Uri api)
        {
            Api = api;
        }

        public Uri Api { get; }
    }
}