using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.DataAccess.Models.EmployeesModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeesDto>()
                .ForMember(E => E.EmpType, Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(E => E.EmpGender, Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<Employee, DetailsEmployeeDto>()
                .ForMember(E => E.HiringDate, Options => Options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(E => E.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreatedEmployeeDto, Employee>()
                                .ForMember(E => E.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                                .ReverseMap();

            CreateMap<UpdatedEmployeeDto, Employee>()
                                .ForMember(E => E.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
