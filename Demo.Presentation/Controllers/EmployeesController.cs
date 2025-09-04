using Demo.BusinessLogic.DataTransferObjects.DepartmentsDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.BusinessLogic.Services.DepartmentServices;
using Demo.BusinessLogic.Services.EmployeeServices;
using Demo.DataAccess.Models.EmployeesModels;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.Reflection.Metadata.Ecma335;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeServices.GetEmployeeAll(EmployeeSearchName);
            return View(employees);
        }


        #region Create Employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeView)
        {
            if (ModelState.IsValid)
            {
                var employeeDto = new CreatedEmployeeDto
                {
                    Name = employeeView.Name,
                    Age = employeeView.Age,
                    Address = employeeView.Address,
                    Salary = employeeView.Salary,
                    Email = employeeView.Email,
                    IsActive = employeeView.IsActive,
                    PhoneNumber = employeeView.PhoneNumber,
                    EmployeeType = employeeView.EmployeeType,
                    Gender = employeeView.Gender,
                    HiringDate = employeeView.HiringDate,
                    DepartmentId = employeeView.DepartmentId
                };
                try
                {
                    int result = _employeeServices.CreateEmployee(employeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't be created.");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console and Return Same View With Error message
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table in Database And Return Error View
                        _logger.LogError(ex.Message);
                    }
                }
            }

            return View(employeeView);
        }
        #endregion

        #region Details Employee
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employees = _employeeServices.GetEmployeeById(id.Value);
            return View(employees);
        }
        #endregion

        #region Edit Employee

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeView = new EmployeeViewModel()
            {

                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
            };

            return View(employeeView);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeView)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeView);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeView.Name,
                    Age = employeeView.Age,
                    Address = employeeView.Address,
                    Salary = employeeView.Salary,
                    Email = employeeView.Email,
                    IsActive = employeeView.IsActive,
                    PhoneNumber = employeeView.PhoneNumber,
                    EmployeeType = employeeView.EmployeeType,
                    Gender = employeeView.Gender,
                    HiringDate = employeeView.HiringDate,
                    DepartmentId = employeeView.DepartmentId
                };
                var Result = _employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is not Updated");
                    return View(employeeView);

                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeView);
                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

        #region Delete Employee

        // 1. The Other Way to Delete in controller     =>  2. The Second Way Is WithIn HTML
        //[HttpGet]  
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeServices.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
    }
}
