using System.Collections.Generic;

namespace EONET.DAL.Entities
{
    public class RootEvent
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public List<Event> Events { get; set; }
    }
}
