using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEng.ReplacementParts.Models
{
    public class SparePart
    {
        public long Key { get; set; }
        // OEM
        public virtual OEM OEM { get; set; }
        public string OEMKey { get; set; }

        //Manufacturer
        public virtual Manufacturer Manufacturer { get; set; }
        public long ManufacturerKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public long Available { get; set; }
        public string PictureUrl { get; set; }
    }
}
