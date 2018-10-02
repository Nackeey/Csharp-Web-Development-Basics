namespace SIS.Demo
{
    using HTTP.Responses.Contracts;
    using SIS.HTTP.Enums;
    using WebServer.Results;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello, World!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
