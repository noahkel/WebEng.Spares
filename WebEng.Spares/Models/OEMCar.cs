using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;

namespace WebEng.ReplacementParts.Models
{
    public class OEMCar
    {
        public long Key { get; set; }
        public virtual Car Car { get; set; }
        public long CarFK { get; set; }
        public virtual OEM OEM { get; set; }
        public string OEMFK { get; set; }
    }
}
