using CarritoCompras.Data;
using CarritoCompras.Models;
using CarritoCompras.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarritoCompras.Controllers
{
    public class CarritoController : OrigenController
    {
        public CarritoController(ApplicationDbContext context) : base(context)
        { }

        [AllowAnonymous]
        public async  Task<IActionResult> Index(){

            var carritoViewModel = await GetCarritoViewModelAsync();

            foreach (var item in carritoViewModel.Items)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto != null)
                {
                    item.Producto = producto;

                    if (!producto.Disponible)
                    {
                        item.Cantidad = 0;
                    }
                    else
                    {
                        item.Cantidad = Math.Min(item.Cantidad, producto.Stock);
                    }
                    if (item.Cantidad == 0)
                    {
                        item.Cantidad = 1;
                    }
                    else if (item.Cantidad < 0)
                    {
                        item.Cantidad = 0;
                    }
                }
            }


            var usuarioId = User.Identity?.IsAuthenticated == true ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;

            var compraViewModel = new CompraViewModel
            {
                Carrito = carritoViewModel
            };
            return View(compraViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCantidad(int id, int cantidad)
        {
            var carritoViewModel = await GetCarritoViewModelAsync();
            var carritoItem = carritoViewModel.Items.FirstOrDefault(i=>i.ProductoId ==id);

            if (carritoItem!=null)
            {
                carritoItem.Cantidad = cantidad;

                var producto = await _context.Productos.FindAsync(id);
                if (producto != null && producto.Disponible && producto.Stock>0)
                {
                    carritoItem.Cantidad = Math.Min(cantidad, producto.Stock);
                }
                await UpdateCarritoViewModelAsync(carritoViewModel);
            }
            return RedirectToAction("Index", "Carrito");
        }
        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var carritoViewModel = await GetCarritoViewModelAsync();
            var carritoItem = carritoViewModel.Items.FirstOrDefault(i => i.ProductoId == id);

            if (carritoItem != null)
            {
                carritoViewModel.Items.Remove(carritoItem);

                 await UpdateCarritoViewModelAsync(carritoViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> VaciarCarrito()
        {
            await EliminarCarritoViewModelAsync();
            return RedirectToAction("Index");
        }

        private async Task EliminarCarritoViewModelAsync()
        {
            await Task.Run(() => Response.Cookies.Delete("carrito"));
        }

        [HttpPost]
        public async Task<IActionResult> Compra()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carritoViewModel = await GetCarritoViewModelAsync();

            try
            {
                var pedido = new Pedido
                {
                    UsuarioId = int.Parse(usuarioId), 
                    Fecha = DateTime.Now,
                    Estado = "Realizada",
                    Total = carritoViewModel.Total,
                    DetallesPedido = carritoViewModel.Items.Select(item => new Detalle_Pedido
                    {
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio
                    }).ToList()
                };

                foreach (var detalle in pedido.DetallesPedido)
                {
                    var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                    if (producto == null || detalle.Cantidad > producto.Stock)
                    {
                        //Cuando no hay Stock de algun producto
                        return RedirectToAction("Index");
                    }
                }

                foreach (var detalle in pedido.DetallesPedido)
                {
                    var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                    producto.Stock -= detalle.Cantidad;
                }

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                await VaciarCarrito();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

    }
}
