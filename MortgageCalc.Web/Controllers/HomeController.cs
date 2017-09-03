using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MortgageCalc.Web.Models;
using MortgageCalc.Core;
using MortgageCalc.Core.Models;

namespace MortgageCalc.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new MortgageCalcActionResult
            {
                Request = new MortgageCalcRequest()
            });
        }

        [HttpPost]
        public IActionResult Index(MortgageCalcRequest request)
        {
            if (request.AnnualInterestRate > 1)
            {
                request.AnnualInterestRate /= 100m;
            }
            return View(new MortgageCalcActionResult
            {
                Request = request,
                Response = MortgageCalculator.Calculate(request)
            });
        }
    }
}
