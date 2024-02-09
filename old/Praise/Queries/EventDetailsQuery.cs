using Praise.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Queries
{
    public class EventDetailsQuery
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StartHour { get; set; }
        public DateTime? EndHour { get; set; }
        public string Local { get; set; }
        public string Note { get; set; }
        public EStatusEvent Status { get; set; }
        public IEnumerable<UserList> Users { get; set; }
        public IEnumerable<MusicList> Musics { get; set; }
    }
}
