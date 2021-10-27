namespace TestWebApp.WebAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Product
        {
            public const string GetAll = Base + "/products";
            public const string Get = Base + "/product/{productId}";
            public const string Create = Base + "/product";
            public const string Update = Base + "/product/{productId}";
            public const string Delete = Base + "/product/{productId}";
        }

        public static class Auth
        {
            public const string Login = Base + "/auth/login";
            public const string Register = Base + "/auth/register";
            public const string Logout = Base + "/auth/logout";
            public const string Refresh = Base + "/auth/refresh";
        }
    }
}