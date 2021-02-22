using System;
using System.Collections.Generic;
using System.Text;

namespace KonferansDTO
{
    public class SpeakerResponse : Speaker
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
