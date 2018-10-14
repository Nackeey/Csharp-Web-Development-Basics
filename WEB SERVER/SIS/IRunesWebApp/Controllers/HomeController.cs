namespace IRunesWebApp.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;

    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            if (this.IsAuthenticated(request))
            {
                var username = request.Session.GetParameter("username").ToString();
                this.ViewBag["username"] = username;
                
                return this.View(request, "IndexLoggedIn");
            }
            
            return this.View(request);
        }
    }
}
