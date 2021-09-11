using dotnetcoreapp.Models;
using dotnetcoreapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Controllers
{
  //  [Route("Home")]
    // [Route("Controller")]   this means the controller  get replaced with name of controller
    // [Route("[Controller]/[action]")] if we write this we need not to write the action route on the action method name 
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository,IHostingEnvironment hostingEnvironment,ILogger<HomeController> logger)
        {
            this.employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }
        //[Route("")]
        //[Route("home/index")]
        //[Route("home")]
        //as we have mentioned home at outside controller so no need to write home
        //[Route("index")]
        //[Route("")]
        //[Route("~/")]       // if we dont add this and move to root url of app we get run middleware o/p . ~/ ignores the controller view
                            // [Route("action")]   this means the action  get replaced with name of action method on which route is mentioned
        public ViewResult Index()
        {
            var model=employeeRepository.GetEmployees();
            return View(model);
        }
        //[Route("details/{id?}")]
        public ViewResult Details(int? id)
        {
            logger.LogTrace("Log trace");
            logger.LogDebug("Log Debug");
            logger.LogInformation("Log info");
            logger.LogWarning("Log warning");
            logger.LogError("Log error");
            logger.LogCritical("Log critical");
            Employee employee = employeeRepository.GetEmployee(id.Value);
            if(employee==null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id.Value);
            }
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Employee =employee,
                PageTitle = "Employee Details"
            };
            
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel employee)
        {
            if(ModelState.IsValid)
            {
                string uniquefilename = ProcessUploadedFile(employee);

                Employee newemployee = new Employee
                {
                    Name=employee.Name,
                    Email=employee.Email,
                    Department=employee.Department,
                    PhotoPath=uniquefilename
                };
                employeeRepository.Add(newemployee);
                return RedirectToAction("details", new { id = newemployee.Id });
            }
            return View();
         
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
          
            if (ModelState.IsValid)
            {
                Employee employee = employeeRepository.GetEmployee(model.Id);
               
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

               
                Employee updatedEmployee = employeeRepository.Update(employee);

                return RedirectToAction("index");
            }

            return View(model);
        }
        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
    }
}
