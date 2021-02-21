using KonferansDTO;
using System;
using System.Text;

namespace Backend.Data
{
    public class SessionSpeaker
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int SpeakerrId { get; set; }
        public Speaker Speaker { get; set; }
    }
}
