using BlazorApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Pocos;

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerEmployeeController : ControllerBase
    {
        private EmployeeManagerService _employeeManagerService;
        public ManagerEmployeeController(EmployeeManagerService employeeManagerService)
        {
            _employeeManagerService = employeeManagerService;
        }


        [HttpPost("PrintManagerName")]
        public ActionResult PrintManagerName(string name)
        {
            Manager manager = new Manager { Name = name};
            _employeeManagerService.PrintName(manager);
            return Ok();
        }

        [HttpPost("PrintEmployeeName")]
        public ActionResult PrintEmployeeName(string name)
        {
            Employee employee = new Employee { Name = name };
            _employeeManagerService.PrintName(employee);
            return Ok();
        }

    }
}
