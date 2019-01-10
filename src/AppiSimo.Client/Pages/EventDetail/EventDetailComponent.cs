namespace AppiSimo.Client.Pages.EventDetail
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Pages.Abstract;

    public class EventDetailComponent : BaseDetailComponent<Event>
    {
        [Inject]
        IUriHelper UriHelper { get; set; }
        
        [Inject]
        EndPoint<Court> CourtEndPoint { get; set; }

        protected IEnumerable<Court> Courts { get; private set; } = new List<Court>();

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
            
            Courts = (await CourtEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint._client)).Value; 
        }        
        
        protected string StartDate
        {
            get => Entity.StartDate == DateTime.MinValue ? string.Empty : Entity.StartDate.ToString("g");
            set => Entity.StartDate = DateTime.Parse(value);
        }

        protected string EndDate
        {
            get => Entity.EndDate == DateTime.MinValue ? string.Empty : Entity.EndDate.ToString("g");
            set => Entity.EndDate = DateTime.Parse(value);
        }

        protected string SelectedCourt
        {
            get => Entity.CourtId.ToString();
            set => Entity.CourtId = Guid.Parse(value);
        }

        protected override async Task Save()
        {
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
    }
}