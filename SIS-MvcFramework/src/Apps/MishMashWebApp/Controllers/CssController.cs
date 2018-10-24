namespace MishMashWebApp.Controllers
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using SIS.WebServer.Results;

    public class CssController : Controller
    {
        [HttpGet("/reset-css.css")]
        public IHttpResponse ResetCss()
        {
            var css = System.IO.File.ReadAllText("wwwroot/reset-css.css");

            return new TextResult(css, HttpResponseStatusCode.Ok, "text/css");
        }

        [HttpGet("/bootstrap.min.css")]
        public IHttpResponse BootstrapMinCss()
        {
            var css = System.IO.File.ReadAllText("wwwroot/bootstrap.min.css");

            return new TextResult(css, HttpResponseStatusCode.Ok, "text/css");
        }

        [HttpGet("/style.css")]
        public IHttpResponse StyleCss()
        {
            var css = System.IO.File.ReadAllText("wwwroot/style.css");

            return new TextResult(css, HttpResponseStatusCode.Ok, "text/css");
        }
    }
}
