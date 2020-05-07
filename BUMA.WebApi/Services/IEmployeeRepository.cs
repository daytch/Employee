using BUMA.WebApi.Models;
using System.Collections.Generic;

namespace BUMA.WebApi.Services
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        int AddEmployee(Employee entity);
        Employee UpdateEmployee(Employee entity, int id);
        void RemoveEmployee(int id);
        List<Employee> GetAllEmployees();
    }
}
