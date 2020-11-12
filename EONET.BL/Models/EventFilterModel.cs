using EONET.BL.Enums;
using System;

namespace EONET.BL.Models
{
    public class EventFilterModel
    {
        public bool? IsOpen => Status == Status.All ? default(bool?) : Status == Status.Open;
        public Status Status { get; set; }
        public int? Category { get; set; }
        public DateTime Date { get; set; }
        public string SortField { get; set; }
        public SortingDirection SortOrder { get; set; }
    }
}