using KonferansDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class SessionParticipant
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}
