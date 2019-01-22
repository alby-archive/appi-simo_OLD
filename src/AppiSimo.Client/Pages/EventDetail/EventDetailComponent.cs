namespace AppiSimo.Client.Pages.EventDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;

    public class EventDetailComponent : BaseDetailComponent<Event>
    {
        [Inject]
        IUriHelper UriHelper { get; set; }

        [Inject]
        EndPoint<Court> CourtEndPoint { get; set; }

        [Inject]
        EndPoint<Light> LightEndPoint { get; set; }

        [Inject]
        EndPoint<Heat> HeatEndPoint { get; set; }

        [Inject]
        EndPoint<User> UserEndPoint { get; set; }

        bool _isValidStarDate;
        bool _isValidEndDate;

        protected IEnumerable<Court> Courts { get; private set; } = new List<Court>();
        protected IEnumerable<Light> Lights { get; private set; } = new List<Light>();
        protected IEnumerable<Heat> Heats { get; private set; } = new List<Heat>();
        protected ICollection<UserEvent> FilteredUserEvents { get; private set; } = new List<UserEvent>();

        protected string Filter { get; set; } = string.Empty;

        protected override DataServiceQuery<Event> Selector(DataServiceQuery<Event> @event) => @event
            .Expand("UsersEvents($expand=User)");

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();

            Lights = (await LightEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Heats = (await HeatEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;

            await InitUserEvents();

            await InitCourts();
        }

        protected string StartDate
        {
            get => Entity.StartDate == DateTime.MinValue ? string.Empty : Entity.StartDate.ToLocalTime().ToString("g");
            set
            {
                _isValidStarDate = DateTime.TryParse(value, out var datetime);

                Entity.StartDate = datetime.ToUniversalTime();
                InitHeatAndLightDuration();
            }
        }

        protected string EndDate
        {
            get => Entity.EndDate == DateTime.MinValue ? string.Empty : Entity.EndDate.ToLocalTime().ToString("g");
            set
            {
                _isValidEndDate = DateTime.TryParse(value, out var datetime);

                Entity.EndDate = datetime.ToUniversalTime();
                InitHeatAndLightDuration();
            }
        }

        protected string SelectedCourt { get => Entity.CourtId.ToString(); set => Entity.CourtId = Guid.Parse(value); }

        // TODO: https://github.com/aspnet/Blazor/issues/576
        protected string SelectedLight
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

        protected string SelectedHeat
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

        protected decimal Cost
        {
            get
            {
                var duration = Entity.EndDate - Entity.StartDate;

                if (duration == TimeSpan.Zero)
                {
                    return 0;
                }

                var price = Courts.FirstOrDefault(court => court.Id == Entity.CourtId)?.CourtsRates.Select(cr =>
                                (decimal) (HowManyTimeInRate(cr.Rate).TotalSeconds / duration.TotalSeconds) * cr.Rate.Price).Sum() ?? 0;

                var total = Lights.FirstOrDefault(light => light.Id == Entity.LightId)?.Price ?? 0 + Heats.FirstOrDefault(heat => heat.Id == Entity.HeatId)?.Price ?? 0 + price;

                return Entity.UsersEvents.Count == 0 ? 0 : total / Entity.UsersEvents.Count;
            }
        }

        TimeSpan HowManyTimeInRate(Rate rate)
        {
            var started = (rate.StartHour.TimeOfDay > Entity.StartDate.TimeOfDay ? rate.StartHour : Entity.StartDate).TimeOfDay;

            var ended = (rate.EndHour.TimeOfDay < Entity.EndDate.TimeOfDay ? rate.EndHour : Entity.EndDate).TimeOfDay;

            var result = ended - started < TimeSpan.Zero ? TimeSpan.Zero : ended - started;

            return result;
        }

        protected void AddUser(UserEvent userEvent)
        {
            Entity.UsersEvents.Add(userEvent);
            FilteredUserEvents = FilteredUserEvents.Where(ue => ue.UserId != userEvent.UserId).ToList();
            StateHasChanged();
        }

        protected void RemoveUser(UserEvent userEvent)
        {
            Entity.UsersEvents = Entity.UsersEvents.Where(ue => ue.UserId != userEvent.UserId).ToList();
            FilteredUserEvents.Add(userEvent);
            StateHasChanged();
        }

        protected override async Task Save()
        {
            Entity.UsersEvents = Entity.UsersEvents;

            Entity.Users = Entity.UsersEvents.Count;

            await base.Save();
            GoToEvents();
        }

        protected override async Task Delete()
        {
            await base.Delete();
            GoToEvents();
        }

        void GoToEvents()
        {
            UriHelper.NavigateTo("/events");
        }

        async Task InitUserEvents()
        {
            if (Entity.UsersEvents == null)
            {
                Entity.UsersEvents = new List<UserEvent>();
            }

            var users = (await UserEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;

            var userEvents = users.Select(u => new UserEvent { User = u, UserId = u.Id, EventId = Entity.Id, Event = Entity });
            FilteredUserEvents = userEvents.Where(ue => !Entity.UsersEvents.Select(sue => sue.UserId).Contains(ue.UserId)).ToList();
        }

        async Task InitCourts()
        {
            Courts = (await CourtEndPoint.Entities.IncludeTotalCount().Expand("CourtsRates($expand=Rate)").ToListAsync(CourtEndPoint.Client)).Value;
            Entity.CourtId = Entity.CourtId == Guid.Empty ? Courts.OrderBy(court => court.Name).First().Id : Entity.CourtId;
        }

        void InitHeatAndLightDuration()
        {
            double duration = 0;

            if (_isValidEndDate && _isValidEndDate)
            {
                duration = (Entity.EndDate - Entity.StartDate).TotalMinutes > 0 ? (Entity.EndDate - Entity.StartDate).TotalMinutes : 0;
            }

            Entity.LightDuration = Entity.HeatDuration = duration / 60;
        }
    }
}