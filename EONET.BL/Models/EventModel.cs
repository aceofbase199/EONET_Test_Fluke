using System;
using System.Collections.Generic;

namespace EONET.BL.Models
{
    public class EventModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsOpen { get; set; }
        public DateTime? Closed { get; set; }

        public List<CategoryModel> Categories { get; set; }
        public List<SourceModel> Sources { get; set; }
        public List<GeometryModel> Geometries { get; set; }
    }
}
