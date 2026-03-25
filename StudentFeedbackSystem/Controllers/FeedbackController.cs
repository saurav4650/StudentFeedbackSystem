using Microsoft.AspNetCore.Mvc;
using StudentFeedbackSystem.Filters;
using StudentFeedbackSystem.Models;
using StudentFeedbackSystem.Services;
using System.Security.Claims;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin", "Principal", "Instructor", "Student")]
    public class FeedbackController : Controller
    {
        private readonly IFeedback service;

        public FeedbackController(IFeedback _service)
        {
            this.service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmitFeedback(int scheduleId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (service.HasSubmitted(userId, scheduleId))
            {
                TempData["ErrorMessage"] = "You have already submitted feedback for this course.";
                return RedirectToAction("Index");
            }

            var model = service.GetSubmissionForm(scheduleId);
            model.StudentUserId = userId;
            return View(model);
        }


        [HttpPost]
        public IActionResult SubmitFeedback(FeedbackSubmissionVM model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            model.StudentUserId = userId;

            int res = service.SubmitFeedback(model);

            if (res > 0)
            {
                TempData["SuccessMessage"] = "Feedback submitted successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to submit feedback. You may have already submitted.";
            return RedirectToAction("Index");
        }
    }
}
