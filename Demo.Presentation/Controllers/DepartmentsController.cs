using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.DataTransferObjects;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Demo.BusinessLogic.DataTransferObjects.DepartmentsDto;
using Demo.BusinessLogic.Services.DepartmentServices;
using Demo.Presentation.ViewModels;


namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello From View Data"; // The View Data
            var department = _departmentService.GetAllDepartments();
            return View(department);
        }


        #region Create Department

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken] // 1.The Way Action Filter
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var departmentDto = new CreatedDepartmentDto()
                {
                    
                    Name = departmentViewModel.Name,
                    Code = departmentViewModel.Code,
                    Description = departmentViewModel.Description,
                    DateOfCreation = departmentViewModel.DateOfCreation
                };

                try
                {
                    int result = _departmentService.CreateDepartment(departmentDto);
                    string Message;
                    if (result > 0)
                        Message = $"Department {departmentDto.Name} Is Created Successfully";
                    else
                        Message = $"Department {departmentDto.Name} Can Not Created";
                    TempData["Message"] = Message ; // the Temp Data
                    return RedirectToAction(nameof(Index));
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

            return View(departmentViewModel);
        }
        #endregion

        #region Details Deparment
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            return View(department);
        } 
        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            };

            return View(departmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentDetialsDto viewModel)
        {
            try
            {
                var UpdatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = viewModel.Id,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };

                int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is not Updated");
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
                    return View("ErrorView", ex);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Departmnet

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
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
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
                    return View("ErrorView",ex);
                }
            }
        }
        #endregion

    }
}
