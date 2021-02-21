using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KonferansDTO
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string EmailAddress { get; set; }

    }
}
