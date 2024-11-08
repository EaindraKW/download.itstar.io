using download.itstar.io.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using download.itstar.io.Data;
using Microsoft.VisualBasic;

namespace download.itstar.io.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
          
        }

        public IActionResult Privacy()
        {
           AppData appData = new AppData();
        //    var app=appData.AvailableBundlesList();
           //var appDetail= appData.OneBundle("EBS Android Free");
           var versionHistory=appData.GetVersionHistory("EBS Android Free");
           return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
