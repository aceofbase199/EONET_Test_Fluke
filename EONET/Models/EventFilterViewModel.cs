using EONET.Web.Enums;
using System;

namespace EONET.Web.Models
{
    public class EventFilterViewModel
    {
        public bool? IsOpen { get; set; }
        public int? Category { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string SortField { get; set; }
        public SortingOrder SortOrder { get; set; } = SortingOrder.Ascending;
    }
}
