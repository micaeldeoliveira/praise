using Praise.Models;

namespace Praise.Models
{
    public class EventMusic
    {
        public int EventId { get; set; }
        public int MusicId { get; set; }
        public int Order { get; set; }
        public bool Play { get; set; }
        public virtual Music Music { get; set; }
        public virtual Event Event { get; set; }
    }
}
