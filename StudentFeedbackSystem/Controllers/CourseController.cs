using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentFeedbackSystem.Filters;
using StudentFeedbackSystem.Models;
using StudentFeedbackSystem.Services;
using System.Security.Claims;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin", "Principal", "Instructor")]
    public class CourseController : Controller
    {
        private readonly ICourse service;
        private readonly IDepartment deptService;

        public CourseController(ICourse _service, IDepartment _deptService)
        {
            this.service = _service;
            this.deptService = _deptService;
        }

        public IActionResult Index()
        {
            loadData();
            return View(new CoursesVM());
        }

        private void loadData()
        {
            ViewBag.Departments = new SelectList(deptService.GetAll(), "DepartmentId", "DepartmentName");
            ViewBag.IsViewOnly = User.FindFirst(ClaimTypes.Role)?.Value == "Instructor";
        }

        [HttpPost]
        public IActionResult Save([FromBody] CoursesVM obj)
        {
            if (User.FindFirst(ClaimTypes.Role)?.Value == "Instructor")
                return Ok(new { Code = -1, Message = "Access Denied" });

            if (!ModelState.IsValid)
                return Ok(new { Code = -1, Message = "Validation failed", Errors = ModelState });

            int res = service.Save(obj);
            if (res > 0)
                return Ok(new { Code = 0, Message = "Saved Successfully", Data = res });
            else
                return Ok(new { Code = -1, Message = "Failed to save" });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);
            if (data != null)
                return Ok(new { Code = 0, Data = data });
            else
                return Ok(new { Code = -1, Message = "Record not found" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (User.FindFirst(ClaimTypes.Role)?.Value == "Instructor")
                return Ok(new { Code = -1, Message = "Access Denied" });

            bool res = service.Delete(id);
            if (res) 
                return Ok(new { Code = 0, Message = "Deleted Successfully" });
            else
                return Ok(new { Code = -1, Message = "Failed to delete" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return Ok(new { Code = 0, Data = data });
        }
    }
}
