using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationMaxim.DAL;
using WebApplicationMaxim.Models;

namespace WebApplicationMaxim.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbContext;
        public HomeController(AppDbContext context)
        {
            _dbContext= context;
        }
     
        public IActionResult Index()
        {
            
            List<Services> services = _dbContext.Services.ToList();
                        
            return View(services); 
        } 

    }
}