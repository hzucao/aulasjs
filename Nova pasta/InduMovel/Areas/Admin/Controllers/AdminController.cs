using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InduMovel.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AdminController: Controller
    {
       
        public IActionResult Index(){

            return View();

        }
        
    }
}