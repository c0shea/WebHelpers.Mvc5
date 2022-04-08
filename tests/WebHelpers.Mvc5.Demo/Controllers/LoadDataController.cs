using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.Demo.Controllers
{
    public class LoadDataController : Controller
    {
        public IActionResult Json() => View();

        public IActionResult JsonData() => new JsonNetResult(new { key = "value" });

        public IActionResult Array() => View();
    }
}
