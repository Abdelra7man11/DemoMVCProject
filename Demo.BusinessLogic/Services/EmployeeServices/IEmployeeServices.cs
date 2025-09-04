using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeesDto> GetEmployeeAll(string? SearchValue);
        DetailsEmployeeDto? GetEmployeeById(int id);   
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
