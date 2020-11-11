using System;
using System.Collections.Generic;

namespace EONET.BL.Models
{
    public class GeometryModel
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public List<decimal> Coordinates { get; set; }
    }
}