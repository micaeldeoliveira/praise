using System;
using System.ComponentModel.DataAnnotations;

namespace Praise.Models
{
    public class Entity
    {        
        public Entity()
        {
            CreatedDate = DateTime.Now;
            LastModifiedDate = null;
        }
                
        public int Id { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
