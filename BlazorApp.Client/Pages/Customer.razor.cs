using BlazorApp.Client.Services.CustomersService;
using Microsoft.AspNetCore.Components;
using Pocos;

namespace BlazorApp.Client.Pages
{
    public class CustomerBase : ComponentBase
    {
        [Parameter]
        public Guid? Id { get; set; }
        protected string btnText = string.Empty;
        protected CustomerDTO customer = new CustomerDTO();
        [Inject] 
        protected ICustomerService CustomersService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            btnText = Id == null ? "Save New Customer" : "Update Customer";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                customer = await CustomersService.GetSingleCustomer((Guid)Id);
            }
        }

        protected async Task HandleSubmit()
        {
            if (Id == null)
            {
                await CustomersService.CreateCustomer(customer);
            }
            else
            {
                await CustomersService.UpdateCustomer(customer);
            }
        }

        protected async Task DeleteCustomer()
        {
            await CustomersService.DeleteCustomer(customer.Id);
        }
    }
}