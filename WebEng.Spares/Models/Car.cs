using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebEng.ReplacementParts.Data
{
    public class Car
    {
        public string PictureUrl { get; set; }
        public long Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Started { get; set; }
        public long Finished { get; set; }
        public long Weight { get; set; }
        public long BrandFK { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
