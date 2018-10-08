namespace IRunesWebApp.Controllers
{
    using IRunesWebApp.Data;
    using Services;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class BaseController
    {
        private const string ControllerDefaultName = "Controller";
        private readonly UserCookieService userCookieService;

        protected IRunesDbContext Context { get; set; }

        public BaseController()
        {
            this.Context = new IRunesDbContext();
            this.userCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        protected IDictionary<string, string> ViewBag { get; set; }

        public bool IsAuthenticated(IHttpRequest request)
        {
           return request.Session.ContainsParameter("username");
        }

        public void SignUser(string username, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = this.userCookieService.GetUserCookie(username);

            var cookie = new HttpCookie("IRunes_auth", userCookieValue);

            request.Cookies.Add(cookie);
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            string filePath = $"../../../Views/{this.GetControllerName()}/{viewName}.html";

            if (!File.Exists(filePath))
            {
                return new BadRequestResult($"View {viewName} not found.", HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllText(filePath);

            foreach (var viewBagKey in ViewBag.Keys)
            {
                var dynamicDataPlaceholder = $"{{{{{viewBagKey}}}}}";
                if (fileContent.Contains(dynamicDataPlaceholder))
                {
                    fileContent = fileContent.Replace(dynamicDataPlaceholder, this.ViewBag[viewBagKey]);
                }
            }

            var response = new HtmlResult(fileContent, HttpResponseStatusCode.Ok);

            return response;
        }

        private string GetControllerName()
            => this.GetType().Name.Replace(ControllerDefaultName, string.Empty);
    }
}
