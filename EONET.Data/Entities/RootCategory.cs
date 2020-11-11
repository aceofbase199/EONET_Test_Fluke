using System.Collections.Generic;

namespace EONET.DAL.Entities
{
    public class RootCategory
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Category> Categories { get; set; }
    }
}