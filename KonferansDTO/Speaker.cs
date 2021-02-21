using System;
using System.ComponentModel.DataAnnotations;

namespace KonferansDTO
{
    public class Speaker
    {
        public int Id { get; set; }
        ///<sumary>
        ///the name of the speaker
        ///</sumary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        ///<sumary>
        ///the bio information of the speaker
        ///</sumary>
        [StringLength(4000)]
        public string Bio { get; set; }

        ///<sumary>
        ///learn more about speaker
        ///</sumary>
        [StringLength(1000)]
        public virtual string Website { get; set; }
    }
}
