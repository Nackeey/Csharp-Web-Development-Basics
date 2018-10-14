namespace IRunesWebApp.Controllers
{
    using IRunesWebApp.Models;
    using Services;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Linq;

    public class UsersController : BaseController
    {
        private readonly HashService hashService;
        private readonly UserCookieService userCookieService;

        public UsersController()
        {
            this.hashService = new HashService();
            this.userCookieService = new UserCookieService();
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View(request);
        }

        public IHttpResponse PostLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);
            var user = this.Context.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);

            if (user == null)
            {
                return new RedirectResult("/");
            }

            this.SignUser(username, request);
            
            return new RedirectResult("/");
        }

        public IHttpResponse PostRegister(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();
            var email = request.FormData["email"].ToString();

            var hashedPassword = this.hashService.Hash(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email
            };

            this.Context.Users.Add(user);

            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                return new BadRequestResult(e.Message, HttpResponseStatusCode.InternalServerError);
            }

            this.SignUser(username, request);

            return new RedirectResult("/");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie("SIS_ID"))
            {
                return new RedirectResult("/");
            }

            var userCookie = request.Cookies.GetCookie("SIS_ID");
            userCookie.Delete();

            var response = new RedirectResult("/");
            response.Cookies.Add(userCookie);

            return response;
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View(request);
        }
    }
}
