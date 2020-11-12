using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;

namespace WebEng.ReplacementParts.Models
{
    public class OEM
    {
        [Key]
        public string OEMNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Weight { get; set; }
        public string PictureUrl { get; set; }
    }
}
