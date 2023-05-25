using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
