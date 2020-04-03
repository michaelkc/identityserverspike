using System;

namespace SharedConfiguration
{
    public static class Api1
    {
        public const string BaseUrl = "https://localhost:44212/";
        public const string ClientId = "api1";
        public const string ClientSecret = "Ej^ATR$^1WBRw1UlO&f4lTMI9OpX#O";
        public const string ResourceId = "api1";
        public const string ResourceName = "API 1";
    }

    public static class Api2
    {
        public const string BaseUrl = "https://localhost:44327/";
        public const string ClientId = "api2";
        public const string ClientSecret = "OruJJXnCMmB5fh#k@yPF*#g87wersD";
        public const string ResourceId = "api2";
        public const string ResourceName = "API 2";
    }

    public static class Mvc
    {
        public const string BaseUrl = "https://localhost:44308/";
        public const string ClientId = "mvc";
        public const string ClientSecret = "T9wfR*yP*Y5d*#fR&J*QtCZD7ioUr9";
        public const string ClientName = "MVC Client";
        public const string RedirectUri = BaseUrl + "signin-oidc";
    }

    public static class IdentityServer
    {
        public const string BaseUrl = "https://localhost:44380/";
    }
}
