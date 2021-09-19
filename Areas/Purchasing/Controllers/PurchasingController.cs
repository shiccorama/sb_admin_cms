using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_light_admin.view.Areas.Purchasing.Controllers
{

    // dont forget to add your ((area)) attribute at first :

    [Area("Purchasing")]
    public class PurchasingController : Controller
    {
        public IActionResult Purchasing()
        {
            return View();
        }
    }
}
