using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapiFullstack.Data;
using webapiFullstack.Models;

namespace webapiFullstack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly webapiDbContext _webapiDB;

        public EmployeesController(webapiDbContext webapiDB)
        {
            _webapiDB = webapiDB;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _webapiDB.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest) { 
        
            employeeRequest.Id= Guid.NewGuid();
            await _webapiDB.Employees.AddAsync(employeeRequest);
            await _webapiDB.SaveChangesAsync();
            return Ok(employeeRequest);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute]Guid id) {

            var employee = await _webapiDB.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {

                return NotFound();
            }
    

                return Ok(employee);
         
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest) {

            var employee = await _webapiDB.Employees.FindAsync(id);

            if (employee == null)
            {

                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department= updateEmployeeRequest.Department;

            await _webapiDB.SaveChangesAsync();

            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id) {

            var employee = await _webapiDB.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();

            }

             _webapiDB.Remove(employee);
            await _webapiDB.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
