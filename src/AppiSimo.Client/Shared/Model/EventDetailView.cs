namespace AppiSimo.Client.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using AppiSimo.Shared.Model;
    using AppiSimo.Shared.Validators.Abstract;
    using AppiSimo.Shared.Validators.Model;

    public class EventDetailView : IViewModel<Event>
    {
        readonly IValidator<Event> _validator;
        public Event Event { get; }

        public ICollection<UserEvent> SelectedUserEvents { get; set; } = new List<UserEvent>();

        public ICollection<UserEvent> FilteredUserEvents { get; set; } = new List<UserEvent>();

        public ICollection<Court> Courts { get; } = new List<Court>();

        // Validation Properties
        public bool IsValidStarDate { get; set; }
        public bool IsValidEndDate { get; set; }

        public Result State { get; set; } = Result.Valid;

        public EventDetailView()
        {
            Event = new Event();
        }

        public EventDetailView(Event @event, IEnumerable<User> users, IEnumerable<Court> courts, IValidator<Event> validator)
        {
            Event = @event;

            _validator = validator;

            Courts = courts.ToList();

            SelectedCourt = @event.CourtId == Guid.Empty ? Courts.OrderBy(court => court.Name).First().Id.ToString() : @event.CourtId.ToString();

            SelectedUserEvents = @event.UsersEvents;
            FilteredUserEvents = users.Where(user => !SelectedUserEvents.Select(sue => sue.UserId).Contains(user.Id)).Select(user => new UserEvent { EventId = @event.Id, UserId = user.Id, User = user }).ToList();

            IsValidStarDate = IsValidEndDate = !IsNewEntity();
        }

        public bool IsNewEntity() => Event.Id == Guid.Empty;

        public void SwitchPayment(Guid id)
        {
            var userEvent = SelectedUserEvents.FirstOrDefault(ue => ue.UserId == id);

            if (userEvent != null)
            {
                userEvent.Paid = !userEvent.Paid;
            }
        }

        public string SelectedStartDate
        {
            get => Event.StartDate == DateTime.MinValue ? string.Empty : $"{Event.StartDate:g}";
            set
            {
                IsValidStarDate = DateTime.TryParse(value, out var datetime);

                Event.StartDate = datetime.ToUniversalTime();
                SetLightAndHeatDuration();
                State = _validator.Validate(Event);
            }
        }

        public string SelectedEndDate
        {
            get => Event.EndDate == DateTime.MinValue ? string.Empty : $"{Event.EndDate:g}";
            set
            {
                IsValidEndDate = DateTime.TryParse(value, out var datetime);

                Event.EndDate = datetime.ToUniversalTime();
                SetLightAndHeatDuration();
                State = _validator.Validate(Event);
            }
        }

        public string SelectedCourt { get => Event.CourtId.ToString(); set => Event.CourtId = Guid.Parse(value); }

        public Light SelectedCourtLight => Courts.FirstOrDefault(court => court.Id.ToString() == SelectedCourt)?.Light ?? new Light();

        // TODO: https://github.com/aspnet/Blazor/issues/576
        public string SelectedLight
        {
            get => Event.LightId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Event.LightId = null;
                }
                else
                {
                    Event.LightId = Guid.Parse(value);
                }
                
                SetLightAndHeatDuration();
            }
        }

        public string SelectedLightDuration
        {
            get => Event.LightDuration.ToString("0.0");
            set
            {
                var isValidDuration = double.TryParse(value, out var duration);
                Event.LightDuration = isValidDuration ? duration : 0;
            }
        }

        public Heat SelectedCourtHeat => Courts.FirstOrDefault(court => court.Id.ToString() == SelectedCourt)?.Heat ?? new Heat();

        public string SelectedHeat
        {
            get => Event.HeatId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Event.HeatId = null;
                }
                else
                {
                    Event.HeatId = Guid.Parse(value);
                }
                
                SetLightAndHeatDuration();
            }
        }

        public string SelectedHeatDuration
        {
            get => Event.HeatDuration.ToString("0.0");
            set
            {
                var isValidDuration = double.TryParse(value, out var duration);
                Event.HeatDuration = isValidDuration ? duration : 0;
            }
        }

        double GetDuration()
        {
            if (!IsValidEndDate || !IsValidEndDate)
            {
                return 0;
            }

            var totalHours = (Event.EndDate - Event.StartDate).TotalHours;

            return totalHours > 0 ? totalHours : 0;
        }

        void SetLightAndHeatDuration()
        {
            var duration = GetDuration();

            Event.LightDuration = Event.LightId == null ? 0 : duration;
            Event.HeatDuration = Event.HeatId == null ? 0 : duration;
        }
    }
}