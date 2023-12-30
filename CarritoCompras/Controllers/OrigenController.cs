using CarritoCompras.Data;
using CarritoCompras.Models;
using CarritoCompras.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CarritoCompras.Controllers
{
    public class OrigenController : Controller
    {
        public readonly ApplicationDbContext _context;

        public OrigenController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override ViewResult View(string? viewName, object? model)
        {
            ViewBag.NumeroProductos = GetCarritoCount();
            return base.View(viewName, model);
        }

        protected int GetCarritoCount()
        {
            int count = 0;

            string? carritoJson = Request.Cookies["carrito"];

            if (!string.IsNullOrEmpty(carritoJson) )
            {
                var carrito = JsonConvert.DeserializeObject<List<ProductoIdAndCantidad>>(carritoJson);

                if(carrito != null)
                {
                    count = carrito.Count;
                }
            }

            return count;
        }

        public async Task<CarritoViewModel> AgregarProductoCarrito(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);

            if(producto!=null)
            {
                var carritoViewModel = await GetCarritoViewModelAsync();

                var carritoItem = carritoViewModel.Items.FirstOrDefault(item=>item.ProductoId==productoId);

                if(carritoItem!=null)
                {
                    carritoItem.Cantidad += cantidad;
                }
                else
                {
                    carritoViewModel.Items.Add(new CarritoItemViewModel
                    {
                        ProductoId = producto.ProductoId,
                        Nombre = producto.Nombre,
                        Precio = producto.Precio,
                        Cantidad = cantidad
                    });
                }
                carritoViewModel.Total = carritoViewModel.Items.Sum(item => item.Cantidad * item.Precio);
                await UpdateCarritoViewModelAsync(carritoViewModel);
                return carritoViewModel;
            }
            return new CarritoViewModel();
        }

        public async Task UpdateCarritoViewModelAsync(CarritoViewModel carritoViewModel)
        {
            var productoIds = carritoViewModel.Items.Select(
                item => new ProductoIdAndCantidad
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad
                }
                ).ToList();

            var carritoJson = await Task.Run(() => JsonConvert.SerializeObject(productoIds));

            Response.Cookies.Append(
                "carrito",
                 carritoJson,
                 new CookieOptions { Expires=DateTimeOffset.Now.AddDays(5)}
            );
        }

        public async Task<CarritoViewModel> GetCarritoViewModelAsync()
        {
            var carritoJson = Request.Cookies["carrito"];

            if(string.IsNullOrEmpty(carritoJson))
            {
                return new CarritoViewModel();
            }

            var ProductoIdsAndCantidades = JsonConvert.DeserializeObject<List<ProductoIdAndCantidad>>(carritoJson);
            var carritoViewModel= new CarritoViewModel();

            if(ProductoIdsAndCantidades != null)
            {
                foreach(var item in ProductoIdsAndCantidades)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if(producto!=null)
                    {
                        carritoViewModel.Items.Add(new CarritoItemViewModel
                        {
                            ProductoId = producto.ProductoId,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Cantidad = item.Cantidad
                        }
                        );
                    }
                }
            }
            carritoViewModel.Total = carritoViewModel.Items.Sum(item => item.Subtotal);
            return carritoViewModel;
        }
    }
}
