namespace IRunesWebApp.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Collections.Generic;
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

            var albums = this.Context.Albums.ToArray();
            var sb = new StringBuilder();

            if (albums.Any())
            {
                foreach (var album in albums)
                {
                    var albumHtml = $@"<a href=""/Albums/Details?id={album.Id}""><b>{album.Name}</b></a><br><br>";
                    sb.AppendLine(albumHtml);
                }

                this.ViewBag["albumsList"] = sb.ToString();
            }
            else
            {
                this.ViewBag["albumsList"] = "There are currently no albums.";
            }

            return this.View(request);
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

        public IHttpResponse Details(IHttpRequest request)
        {
            var albumId = request.QueryData["id"].ToString();

            Album album = this.Context.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.Id == albumId);

            var sb = new StringBuilder();

            if (album.Tracks.Any())
            {
                foreach (var track in album.Tracks)
                {
                    sb.AppendLine($@"<a href=""/Tracks/Details?id={track.Id}"">{track.Name}</a>");
                }
            }
            else
            {
                sb.AppendLine("There are no tracks in this album.");
            }

            this.ViewBag["tracks"] = sb.ToString();
            this.ViewBag["Name"] = album.Name;
            this.ViewBag["CoverUrl"] = album.Cover;
            this.ViewBag["Price"] = album.Price.ToString("F2");
            this.ViewBag["AlbumId"] = albumId;
        
            return this.View(request);
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/Users/Login");
            }

            return this.View(request);
        }
    }
}
