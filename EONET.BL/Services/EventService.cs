using AutoMapper;
using EONET.Api.Client;
using EONET.BL.Abstraction;
using EONET.BL.Extensions;
using EONET.BL.Models;
using EONET.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EONET.BL.Services
{
    public class EventService : IEventService
    {
        private readonly IHttpEonetClient _eonetClient;
        private readonly IMapper _mapper;

        public EventService(IMapper mapper, IHttpEonetClient eonetClient)
        {
            _mapper = mapper;
            _eonetClient = eonetClient;
        }

        public async Task<EventModel> GetEventAsync(string id)
        {
            var @event = await _eonetClient.GetEventAsync(id);

            return _mapper.Map<EventModel>(@event);
        }

        public async Task<List<EventModel>> GetEventsAsync(EventFilterModel model)
        {
            var days = (int)(DateTime.UtcNow - model.Date).TotalDays + 1;
            var events = await GetFilteredEvents(model, days);
            var sortedEvents = events.ApplyOrdering(model.SortField, model.SortOrder);

            return _mapper.Map<List<EventModel>>(sortedEvents);
        }

        private async Task<List<Event>> GetFilteredEvents(EventFilterModel model, int days)
        {
            var events = new List<Event>();

            if (model.IsOpen.HasValue)
            {
                events = await GetEventsAsync(days, model.IsOpen.Value);
            }
            else
            {
                var openedEvents = await GetEventsAsync(days, true);
                var closedevents = await GetEventsAsync(days, false);

                // combine opened and closed events due to API only works with exact status
                events = openedEvents.Union(closedevents).DistinctBy(x => x.Id).ToList();
            }

            if (model.Category.HasValue && model.Category.Value > 0)
            {
                events = events.Where(x => x.Categories.Any(y => y.Id == model.Category.Value)).ToList();
            }

            return events;
        }

        private async Task<List<Event>> GetEventsAsync(int days, bool isOpen = true)
        {
            var openedEvents = await _eonetClient.GetEventsAsync(days, isOpen);
            openedEvents.ForEach(x => x.IsOpen = isOpen);

            return openedEvents;
        }
    }
}