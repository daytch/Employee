using BUMA.WebApi.Models;
using BUMA.WebApi.Services.ExecuteCommands;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BUMA.WebApi.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        public EmployeeRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("Dapper");
            _executers = executers;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> query = _executers.ExecuteCommand(_connStr,
                   conn => conn.Query<Employee>("sp_GetAllEmployee", commandType: CommandType.StoredProcedure)).ToList();
            return query;
        }

        public Employee GetById(int id)
        {
            var Employee = _executers.ExecuteCommand<Employee>(_connStr, conn =>
                conn.Query<Employee>("sp_GetEmployee", new { Id = id }, commandType: CommandType.StoredProcedure).First());
            return Employee;
        }

        public int AddEmployee(Employee entity)
        {
            var param = new DynamicParameters();
            param.Add("@name", entity.Name);
            param.Add("@dob", entity.DOB);
            param.Add("@gender", entity.Gender);

            param.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn =>
            {
                conn.Execute("sp_InsertEmployee", param, commandType: CommandType.StoredProcedure);
            });

            int id = param.Get<int>("@id");
            return id;
        }

        public Employee UpdateEmployee(Employee entity, int id)
        {
            Employee query = _executers.ExecuteCommand<Employee>(_connStr, conn =>
            conn.Query<Employee>("sp_UpdateEmployee", new
            {
                id = entity.Id,
                name = entity.Name,
                dob = entity.DOB,
                gender = entity.Gender
            }, commandType: CommandType.StoredProcedure).First());

            return query;
        }

        public void RemoveEmployee(int id)
        {
            _executers.ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Employee>("sp_DeleteEmployee", new { Id = id }, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
