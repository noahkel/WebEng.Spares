using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEng.ReplacementParts.Models
{
    public class ReplacementPart
    {
        public long Key { get; set; }
        // OEM
        public virtual OEM OEM { get; set; }
        public string OEMKey { get; set; }

        //Manufacturer
        public virtual Manufacturer Manufacturer { get; set; }
        public string ManufacturerKey { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public long Weight { get; set; }
        public long Available { get; set; }



    }
}
