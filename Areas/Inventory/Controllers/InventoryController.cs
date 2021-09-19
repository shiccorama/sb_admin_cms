using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_light_admin.view.Areas.Inventory.Controllers
{

    // this is important step to make, you have to assign area to this controller as long as you're working with areas :

    [Area("Inventory")]
    public class InventoryController : Controller
    {
        public IActionResult Inventory()
        {
            return View();
        }
    }
}
