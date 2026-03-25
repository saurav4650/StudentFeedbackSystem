using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentFeedbackSystem.Filters;
using StudentFeedbackSystem.Models;
using StudentFeedbackSystem.Services;
using System.Security.Claims;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin", "Principal")]
    public class UserController : Controller
    {
        private readonly IUser service;
        private readonly IRoles roleService;
        private readonly IDepartment deptService;

        public UserController(IUser _service, IRoles _roleService, IDepartment _deptService)
        {
            this.service = _service;
            this.roleService = _roleService;
            this.deptService = _deptService;
        }

        public IActionResult Index()
        {
            loadData();
            return View(new UsersVM());
        }

        private void loadData()
        {
            ViewBag.Departments = new SelectList(deptService.GetAll(), "DepartmentId", "DepartmentName");

            var currentRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var allRoles = roleService.GetAll();

            // Principal cannot create Admin or Principal 
            if (currentRole == "Principal")
            {
                allRoles = allRoles.Where(r => r.RoleName != "Admin" && r.RoleName != "Principal").ToList();
            }
            ViewBag.Roles = new SelectList(allRoles, "RoleId", "RoleName");
        }

        [HttpPost]
        public IActionResult Save([FromBody] UsersVM obj)
        {
            if (!ModelState.IsValid)
                return Ok(new { Code = -1, Message = "Validation failed", Errors = ModelState });

            var currentRole = User.FindFirst(ClaimTypes.Role)?.Value;

           
            if (currentRole == "Principal")
            {
                var role = roleService.GetById(obj.RoleId);
                if (role.RoleName == "Admin" || role.RoleName == "Principal")
                {
                    return Ok(new { Code = -1, Message = "You cannot create Admin or Principal users" });
                }
            }

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
            var currentRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Principal not delete admin
            if (currentRole == "Principal")
            {
                var user = service.GetById(id);
                if (user.RoleName == "Admin" || user.RoleName == "Principal")
                {
                    return Ok(new { Code = -1, Message = "You cannot delete Admin or Principal users" });
                }
            }

            bool res = service.Delete(id);
            if (res) return Ok(new { Code = 0, Message = "Deleted Successfully" });
            return Ok(new { Code = -1, Message = "Failed to delete" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Code = 0, Data = service.GetAll() });
        }
    }
}
