using Praise.Models;
using System.Collections.Generic;

namespace Praise.Commands
{
    public class ChangeOrderEventMusicCommand
    {
        public int MusicId { get; set; }
        public int Order { get; set; }
    }
}
