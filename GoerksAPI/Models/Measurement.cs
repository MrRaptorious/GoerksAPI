using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public class Measurement : BaseObject
    {
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
