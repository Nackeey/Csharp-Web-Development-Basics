namespace IRunesWebApp.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse PostCreate(IHttpRequest request)
        {
            return new RedirectResult("/Tracks/Create");
        }
    }
}
