using EmployeeAPIWithJWT_123.Identity;
using EmployeeAPIWithJWT_123.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPIWithJWT_123.Controllers
{
    [Route("api/employee")]
    [ApiController]
   // [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext contex)
        {
            _context = contex;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_context.Employees.ToList());
        }
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeindb = _context.Employees.Find(id);
            if(employeeindb == null) return BadRequest();
            _context.Employees.Remove(employeeindb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
