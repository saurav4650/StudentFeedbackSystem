using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentFeedbackSystem.Filters;
using StudentFeedbackSystem.Models;
using StudentFeedbackSystem.Services;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin", "Principal")]
    public class FeedbackQuestionsController : Controller
    {
          IFeedbackQuestion service;
          IQuestionCat catService;

        public FeedbackQuestionsController(IFeedbackQuestion _service, IQuestionCat _catService)
        {
            this.service = _service;
            this.catService = _catService;
        }

        public IActionResult Index()
        {
            loadData();
            return View(new FeedbackQuestionVM());
        }

        private void loadData()
        {
            ViewBag.Categories = new SelectList(catService.GetAll(), "CategoryId", "CategoryName");
        }

        [HttpPost]
        public IActionResult Save([FromBody] FeedbackQuestionVM obj)
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
