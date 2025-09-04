using Demo.BusinessLogic.DataTransferObjects.DepartmentsDto;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.DepartmentServices;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepositery _departmentRepositery) : IDepartmentService
    {
        // 1. Injection

        // Get All Departmens
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepositery.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }
        // Create Department
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepositery.Add(department);
        }
        // Get By ID 
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepositery.GetById(id);
            return department is null ? null : department.ToDepartmentDetialsDto();
        }
        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepositery.Update(departmentDto.ToEntity());
        }
        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositery.GetById(id);
            if (department is null) return false;
            else
            {
                int Result = _departmentRepositery.Remove(department);
                if (Result > 0) return true;
                else return false;
            }

        }
    }
}
