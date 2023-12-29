using CarritoCompras.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class OrigenController : Controller
    {
        public readonly ApplicationDbContext _context;

        public OrigenController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
