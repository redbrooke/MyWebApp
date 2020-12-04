using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApp.Models;

namespace MyWebApp.Controllers
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

        private static int GCD(int a, int b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        [HttpPost]
        public IActionResult Index(Calculator equation)
        {
            int x, y, equType, a;
            x = equation.value1;
            y = equation.value2;
            equType = equation.type;

            switch(equType)
            {
                case 1:
                    //Eclids algorithm
                    if (x > y)
                    {
                        a = y;
                        y = x;
                        x = a;
                    }
                    while (y != 0)
                    {
                        equation.result = x % y;
                        x = y;
                        if (equation.result == 0)
                        {
                            equation.result = y;
                            y = 0;  // cleanly exit the loop
                                               //break;  // I do not like doing this.
                        }
                        else
                        {
                            y = equation.result; // do this only if we need to loop again
                        }
                    }
                    break;
                case 2:
                    // Native C#
                    equation.result = (int)BigInteger.GreatestCommonDivisor(x, y);
                    break;
                case 3:
                    //recursive
                    equation.result= GCD(x,y);
                    break;
            }

            ViewData["result"] = equation.result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
