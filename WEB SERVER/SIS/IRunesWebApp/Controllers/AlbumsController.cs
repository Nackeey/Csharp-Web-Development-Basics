namespace IRunesWebApp.Controllers
{
    using Models;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System.Linq;
    using System.Text;

    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            var albums = this.Context.Albums;
            var sb = new StringBuilder();

            if (albums.Any())
            {
                foreach (var album in albums)
                {
                    var albumHtml = $@"<a href=""/Albums/Details?id={album.id}"">{album.Name}</a>";
                    sb.AppendLine(albumHtml);

                }

                this.ViewBag["albumsList"] = sb.ToString();
            }
            else
            {
                this.ViewBag["albumsList"] = "There are currently no albums.";
            }

            return this.View();
        }

        public IHttpResponse PostCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString();
            var cover = request.FormData["cover"].ToString();

            Album album = new Album
            {
                Name = name,
                Cover = cover
            };

            this.Context.Albums.Add(album);
            this.Context.SaveChanges();

            return new RedirectResult("/Albums/All");
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/Users/Login");
            }

            return this.View();
        }
    }
}
