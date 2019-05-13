namespace AppiSimo.Shared.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class CognitoContract : Attribute
    {
        public string Convention{ get; set; }
    }
}