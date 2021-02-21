using System.Collections.Generic;

namespace Backend.Data
{
    public class Session : KonferansDTO.Session
    {
        public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; }
        public virtual ICollection<SessionParticipant> SessionParticipants { get; set; }

        public Track Track { get; set; }
    }
}
