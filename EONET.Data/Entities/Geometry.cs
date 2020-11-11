using System;
using System.Collections.Generic;

namespace EONET.DAL.Entities
{
    public class Geometry
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public List<decimal> Coordinates { get; set; }
    }
}