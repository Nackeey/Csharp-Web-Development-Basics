namespace IRunesWebApp.Models
{
    using System.Linq;
    using System.Collections.Generic;

    public class Album : BaseEntity<string>
    {
        public const decimal AlbumPriceReducer = 0.13M;

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price => this.Tracks.Sum(t => t.Price) * 0.87m;

        public virtual ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
