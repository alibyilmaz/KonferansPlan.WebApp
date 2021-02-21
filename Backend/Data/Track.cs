using System.Collections.Generic;

namespace Backend.Data
{
    public class Track : KonferansDTO.Track
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
