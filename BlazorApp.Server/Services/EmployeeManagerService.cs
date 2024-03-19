using Pocos;

namespace BlazorApp.Server.Services
{
    public class EmployeeManagerService
    {
        public void PrintName(INameable person)
        {
            Console.WriteLine(person.Name);
        }
    }
}
