using EONET.BL.Enums;
using System;

namespace EONET.BL.Models
{
    public class EventFilterModel
    {
        public bool? IsOpen => Status == "All" ? default(bool?) : Status == "Open";
        public string Status { get; set; }
        public int? Category { get; set; }
        public DateTime Date { get; set; }
        public string SortField { get; set; }
        public SortingDirection SortOrder { get; set; }
    }
}
