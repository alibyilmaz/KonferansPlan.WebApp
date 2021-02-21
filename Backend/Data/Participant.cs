using System.Collections.Generic;

namespace Backend.Data
{
    public class Participant : KonferansDTO.Participant
    {
        public virtual ICollection<SessionParticipant> SessionParticipants { get; set; }
    }
}
