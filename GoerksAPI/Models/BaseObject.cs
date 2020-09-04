using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public abstract class BaseObject
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastChange { get; set; }
    }
}
