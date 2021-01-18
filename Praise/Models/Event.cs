using Praise.Enums;
using Praise.Models;
using System;
using System.Collections.Generic;

namespace Praise.Models
{
    public class Event : Entity
    {
        public Event(string name, DateTime date, DateTime? startHour, DateTime? endHour, string local, string note)
        {
            Name = name;
            Date = date;
            StartHour = startHour;
            EndHour = endHour;
            Local = local;
            Note = note;
            Status = EStatusEvent.Waiting;
        }
        
        public string Name { get; set; }
        
        public DateTime Date { get; set; }
        public DateTime? StartHour { get; set; }
        public DateTime? EndHour { get; set; }
        
        public string Local { get; set; }
        
        public string Note { get; set; }
        
        public EStatusEvent Status { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
        public ICollection<EventMusic> EventMusics { get; set; }
        

        public void UpdateEvent(string name, DateTime date, DateTime? startHour, DateTime? endHour, string local, string note, EStatusEvent status)
        {
            Name = name;
            Date = date;
            StartHour = startHour;
            EndHour = endHour;
            Local = local;
            Note = note;
            Status = status;
        }
    }
}
