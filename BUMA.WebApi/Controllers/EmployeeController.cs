using BUMA.WebApi.Models;
using BUMA.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Test_BUMA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GellAll()
        {
            var Employees = _EmployeeRepository.GetAllEmployees();
            return Ok(Employees);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var Employee = _EmployeeRepository.GetById(id);
            return Ok(Employee);
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee entity)
        {
            int id = _EmployeeRepository.AddEmployee(entity);
            return Ok(id);
        }
        [HttpPut("{id}")]
        public ActionResult<Employee> Update(Employee entity, int id)
        {
            Employee employee = _EmployeeRepository.UpdateEmployee(entity, id);
            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public ActionResult<Employee> Delete(int id)
        {
            _EmployeeRepository.RemoveEmployee(id);
            return Ok("Data has been deleted");
        }
    }
}
