using EONET.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.Api.Client
{
    public interface IHttpEonetClient
    {
        /// <summary>
        /// Gets the events asynchronous.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <param name="isOpen">if set to <c>true</c> [is open].</param>
        /// <returns>List of events</returns>
        Task<List<Event>> GetEventsAsync(int days, bool isOpen = true);

        /// <summary>
        /// Gets the event by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Event</returns>
        Task<Event> GetEventAsync(string id);

        /// <summary>
        /// Gets the categories asynchronously.
        /// </summary>
        /// <param name="isOpen">if set to <c>true</c> [is open].</param>
        /// <returns>List of all categories</returns>
        Task<List<Category>> GetCategoriesAsync(bool isOpen = true);
    }
}