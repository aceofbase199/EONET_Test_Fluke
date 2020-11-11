using EONET.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.BL.Abstraction
{
    public interface IEventService
    {
        /// <summary>
        /// Gets the filtered events asynchronously.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>List of Event models</returns>
        Task<List<EventModel>> GetEventsAsync(EventFilterModel model);

        /// <summary>
        /// Gets the event by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Evet model</returns>
        Task<EventModel> GetEventAsync(string id);
    }
}
