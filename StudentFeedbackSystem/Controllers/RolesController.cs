using Microsoft.AspNetCore.Mvc;
using StudentFeedbackSystem.Filters;
using StudentFeedbackSystem.Models;
using StudentFeedbackSystem.Services;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin")]
    public class RolesController : Controller
    {
        private readonly IRoles service;
        public RolesController(IRoles _service) => this.service = _service;

        public IActionResult Index()
        {
            return View(new RolesVM());
        }

        [HttpPost]
        public IActionResult Save([FromBody] RolesVM obj)
        {
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
            bool res = service.Delete(id);
            if (res) 
                return Ok(new { Code = 0, Message = "Deleted Successfully" });
            else
                return Ok(new { Code = -1, Message = "Failed to delete" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Code = 0, Data = service.GetAll() });
        }
    }
}
