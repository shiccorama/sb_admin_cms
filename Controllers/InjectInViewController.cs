using sb_light_admin.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_light_admin.view.Controllers
{
    public class InjectInViewController : Controller
    {
        public IActionResult Forms()
        {
            return View();
        }
    }
}
