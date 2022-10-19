using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models.Domain;
using ASPNETMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace dotnetmvcdatabase.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }
 
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth

            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {

            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                }; 

                return View(viewModel);

            }

            return RedirectToAction("Index");
        }

       
    }
}
