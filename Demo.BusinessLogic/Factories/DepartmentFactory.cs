using Demo.BusinessLogic.DataTransferObjects.DepartmentsDto;
using Demo.DataAccess.Models.DepartmentsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Description = department.Description,
                Name = department.Name,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)

            };
        }
        public static DepartmentDetialsDto ToDepartmentDetialsDto(this Department department)
        {
            return new DepartmentDetialsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,  
                Description = department.Description, 
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
                Description = departmentDto.Description,
            };
        }



    }
}
