using System.Collections.Generic;

namespace IRunesWebApp.Models
{
    public class Track : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public decimal Price { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<TrackAlbum> Albums { get; set; } = new HashSet<TrackAlbum>();
    }
}
