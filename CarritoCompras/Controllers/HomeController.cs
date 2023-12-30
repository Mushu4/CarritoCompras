using Microsoft.AspNetCore.Mvc;
using CarritoCompras.Models;
using CarritoCompras.Data;
using CarritoCompras.Services;

namespace CarritoCompras.Controllers;

public class HomeController : OrigenController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductoService _productoService;
    private readonly ICategoriaService _categoriaService;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IProductoService productoService, ICategoriaService categoriaService) : base(context)
    {
        _logger = logger;
        _productoService = productoService;
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Categorias = await _categoriaService.GetCategorias();
        try
        {
            List<Producto> productos = await _productoService.GetProductos();
            return View(productos);
        }
        catch(Exception e)
        {
            return HandleError(e); 
        }
    }

    public IActionResult DetalleProducto(int id)
    {
        var producto = _productoService.GetProducto(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    public async Task<IActionResult> AgregarProducto(int id, int cantidad)
    {
        var carritoViewModel = await AgregarProductoCarrito(id, cantidad);

        if(carritoViewModel != null)
        {
            return RedirectToAction("Index");
        }
        else
        {
           return NotFound();
        }
    }

    public async Task<IActionResult> AgregarProductoVistaDetalle(int id, int cantidad)
    {
        var carritoViewModel = await AgregarProductoCarrito(id, cantidad);

        if (carritoViewModel != null)
        {
            return RedirectToAction("DetalleProducto", new {id});
        }
        else
        {
            return NotFound();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
