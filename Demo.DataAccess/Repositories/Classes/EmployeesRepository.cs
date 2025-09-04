using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.EmployeesModels;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{  
    public class EmployeesRepository(ApplicationDbContext dbContext) :GenericRepository<Employee>(dbContext) ,IEmployeesReposiory
    {
    }
}
