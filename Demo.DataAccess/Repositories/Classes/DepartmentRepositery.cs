using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentsModel;
using Demo.DataAccess.Models.EmployeesModels;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.DepartmentsRepositery
{
    public class DepartmentRepositery(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext) , IDepartmentRepositery
    {

    }
}
