namespace IRunesWebApp.Controllers
{
    using IRunesWebApp.Models;
    using Microsoft.EntityFrameworkCore;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System.Linq;

    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            var albumId = request.QueryData["id"].ToString();

            this.ViewBag["AlbumId"] = albumId;

            return this.View(request);
        }

        public IHttpResponse PostCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString();
            var link = request.FormData["link"].ToString();
            var price = decimal.Parse(request.FormData["price"].ToString());
            var albumId = request.QueryData["id"].ToString();
            var album = this.Context.Albums.FirstOrDefault(a => a.Id == albumId);

            Track track = new Track
            {
                Name = name,
                Link = link,
                Price = price
            };
            album.Tracks.Add(track);
            this.Context.Tracks.Add(track);
            this.Context.SaveChanges();

            return new RedirectResult($"/Tracks/Details?id={track.Id}");
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            var trackId = request.QueryData["id"].ToString();
            Track track = this.Context.Tracks.Include(t => t.Album).FirstOrDefault(a => a.Id == trackId);

            this.ViewBag["Name"] = track.Name;
            this.ViewBag["Price"] = track.Price.ToString();
            this.ViewBag["AlbumId"] = track.Albums.Select(x => x.Album).ToString();

            return this.View(request);
        }
    }
}
