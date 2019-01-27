namespace AppiSimo.Client.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using AppiSimo.Shared.Model;

    public class EventDetailView : IViewModel<Event>
    {
        public Event Entity { get; }

        public ICollection<UserEvent> SelectedUserEvents { get; set; } = new List<UserEvent>();

        public ICollection<UserEvent> FilteredUserEvents { get; set; } = new List<UserEvent>();

        public ICollection<Court> Courts { get; } = new List<Court>();

        public ICollection<Light> Lights { get; } = new List<Light>();

        public ICollection<Heat> Heats { get; } = new List<Heat>();

        // Validation Properties
        public bool IsValidStarDate { get; set; }
        public bool IsValidEndDate { get; set; }

        public EventDetailView()
        {
            Entity = new Event();
        }

        public EventDetailView(Event @event, IEnumerable<User> users, IEnumerable<Court> courts, IEnumerable<Light> lights, IEnumerable<Heat> heats)
        {
            Entity = @event;

            Lights = lights.ToList();
            Heats = heats.ToList();
            Courts = courts.ToList();

            SelectedCourt = @event.CourtId == Guid.Empty ? Courts.OrderBy(court => court.Name).First().Id.ToString() : @event.CourtId.ToString();
            
            SelectedUserEvents = @event.UsersEvents;
            FilteredUserEvents = users.Where(user => !SelectedUserEvents.Select(sue => sue.UserId).Contains(user.Id)).Select(user => new UserEvent { EventId = @event.Id, UserId = user.Id, User = user }).ToList();
        }

        public bool IsNewEntity() => Entity.Id == Guid.Empty;

        public string SelectedStartDate
        {
            get => Entity.StartDate == DateTime.MinValue ? string.Empty : Entity.StartDate.ToLocalTime().ToString("g");
            set
            {
                IsValidStarDate = DateTime.TryParse(value, out var datetime);

                Entity.StartDate = datetime.ToUniversalTime();
                SetLightAndHeatDuration();
            }
        }

        public string SelectedEndDate
        {
            get => Entity.EndDate == DateTime.MinValue ? string.Empty : Entity.EndDate.ToLocalTime().ToString("g");
            set
            {
                IsValidEndDate = DateTime.TryParse(value, out var datetime);

                Entity.EndDate = datetime.ToUniversalTime();
                SetLightAndHeatDuration();
            }
        }

        public string SelectedCourt { get => Entity.CourtId.ToString(); set => Entity.CourtId = Guid.Parse(value); }

        // TODO: https://github.com/aspnet/Blazor/issues/576
        public string SelectedLight
        {
            get => Entity.LightId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Entity.LightId = null;
                }
                else
                {
                    Entity.LightId = Guid.Parse(value);
                }
            }
        }

        public string SelectedLightDuration
        {
            get => Entity.LightDuration.ToString("0.0");
            set
            {
                var isValidDuration = double.TryParse(value, out var duration);
                Entity.LightDuration = isValidDuration ? duration : 0;
            }
        }

        public string SelectedHeat
        {
            get => Entity.HeatId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Entity.HeatId = null;
                }
                else
                {
                    Entity.HeatId = Guid.Parse(value);
                }
            }
        }

        public string SelectedHeatDuration
        {
            get => Entity.HeatDuration.ToString("0.0");
            set
            {
                var isValidDuration = double.TryParse(value, out var duration);
                Entity.HeatDuration = isValidDuration ? duration : 0;
            }
        }

        double GetDuration()
        {
            double duration = 0;

            if (!IsValidEndDate || !IsValidEndDate)
            {
                return duration;
            }

            duration = (Entity.EndDate - Entity.StartDate).TotalMinutes > 0 ? (Entity.EndDate - Entity.StartDate).TotalMinutes : 0;

            return duration;
        }

        void SetLightAndHeatDuration()
        {
            Entity.LightDuration = Entity.HeatDuration = GetDuration() / 60;
        }
    }
}