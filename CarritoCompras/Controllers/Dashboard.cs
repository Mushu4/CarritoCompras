using CarritoCompras.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class Dashboard : OrigenController
    {
        public Dashboard(ApplicationDbContext context):base(context)
        {
            
        }

        public IActionResult Index ()
        {
            return View();
        }
    }
}
