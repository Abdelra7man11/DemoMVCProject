using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.DataAccess.Models.EmployeesModels;
using Demo.DataAccess.Repositories.DepartmentsRepositery;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.EmployeeServices
{
    public class EmployeeServices(IEmployeesReposiory _employeesReposiory , IMapper _mapper) : IEmployeeServices
    {

        public IEnumerable<EmployeesDto> GetEmployeeAll(string? SearchValue)
        {
   
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(SearchValue))
                employees = _employeesReposiory.GetAll();
            else
                employees = _employeesReposiory.GetAll(E => E.Name.ToLower().Contains(SearchValue.ToLower()));

            var employeeDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeesDto>>(employees);
            return employeeDto;

        }

        public DetailsEmployeeDto? GetEmployeeById(int id)
        {
            var employee = _employeesReposiory.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, DetailsEmployeeDto>(employee);
                
            //    new DetailsEmployeeDto()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //    IsActive = employee.IsActive,
            //    PhoneNumber = employee.PhoneNumber,
            //    EmployeeType = employee.EmployeeType.ToString(),
            //    Gender = employee.Gender.ToString(),
            //    CreatedBy = 1,
            //    CreatedOn = employee.CreatedOn,
            //    LastModifiedBy = 1,
            //    LastModifiedOn = employee.LastModifiedOn

            //};
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeesReposiory.Add(employee);

        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
           return _employeesReposiory.Update(employee);
            
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeesReposiory.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeesReposiory.Update(employee) > 0 ? true : false;

                /* The Another Way
                 
                int Result = _employeesReposiory.Remove(employee);
                if (Result > 0)
                    return true;
                else 
                    return false;

                */
            }

        }

       
    }
}
