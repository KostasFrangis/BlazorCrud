using BlazorApp.Client.Services.CustomersService;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{

    public class CustomersBase : ComponentBase
    {
        [Inject]
        protected ICustomerService CustomersService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected int currentPage = 1;
        protected int pageSize = 10; // Change as per your requirement

        protected override async Task OnInitializedAsync()
        {
            await LoadPage(currentPage);
        }

        protected async Task LoadPage(int pageNumber)
        {
            currentPage = pageNumber;
            await CustomersService.GetPagedCustomers(currentPage, pageSize);
            await CustomersService.GetCustomerCount();

        }

        protected void ShowCustomer(Guid id)
        {
            NavigationManager.NavigateTo($"customer/{id}");
        }

        protected void CreateNewCustomer()
        {
            NavigationManager.NavigateTo("/customer");
        }

        protected bool IsFirstPage => currentPage == 1;
        protected bool IsLastPage => currentPage == Math.Ceiling((double)CustomersService.TotalCustomers / pageSize);
        protected int TotalPages => (int)Math.Ceiling((double)CustomersService.TotalCustomers / pageSize);

    }
}
