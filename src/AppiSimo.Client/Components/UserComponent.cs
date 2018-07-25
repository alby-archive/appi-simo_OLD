namespace AppiSimo.Client.Components
{
    using Microsoft.AspNetCore.Blazor.Components;
    
    public class UserComponent : BlazorComponent
    {
        [Parameter]
        protected string Id { get; set; } = "fantastic";   
    }
}