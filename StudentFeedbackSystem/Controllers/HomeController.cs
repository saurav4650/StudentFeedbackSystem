using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentFeedbackSystem.Filters;
using System.Diagnostics;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Controllers
{
    [RoleBasedAuthorization("Admin", "Principal", "Instructor", "Student", "Staff", "Peon", "Worker")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
