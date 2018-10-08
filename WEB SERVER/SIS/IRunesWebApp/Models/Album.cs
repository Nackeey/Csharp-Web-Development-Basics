namespace IRunesWebApp.Models
{
    using System.Linq;
    using System.Collections.Generic;

    public class Album : BaseEntity<string>
    {
        public const decimal AlbumPriceReducer = 0.13M;

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price => this.Tracks.Sum(t => t.Album.Price - (t.Album.Price * AlbumPriceReducer));

        public virtual ICollection<TrackAlbum> Tracks { get; set; } = new HashSet<TrackAlbum>();
    }
}
