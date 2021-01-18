using System.Collections.Generic;

namespace Praise.Models
{
    public class Music : Entity
    {
        public Music(string title, string reminder, string singer, string lirycs, string notation, string video, bool play)
        {
            Title = title;
            Reminder = reminder;
            Singer = singer;
            Lirycs = lirycs;
            Notation = notation;
            Video = video;
            Play = play;
        }

        public string Title { get; set; }
        public string Reminder { get; set; }
        public string Singer { get; set; }        
        public string Lirycs { get; set; }        
        public string Notation { get; set; }        
        public string Video { get; set; }        
        public bool Play { get; set; }
        public ICollection<EventMusic> EventMusics { get; set; }
    }
}