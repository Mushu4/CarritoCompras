using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace CarritoCompras.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Producto GetProducto(int Id)
        {
            var producto = _context.Productos
            .Include(p=>p.Categoria)
            .FirstOrDefault(p=>p.ProductoId== Id);

            if(producto != null)
            {
                return producto;
            }
            return new Producto();
        }

        public async Task<List<Producto>> GetProductos()
        {
            IQueryable<Producto> productosQuery = _context.Productos;
            productosQuery = productosQuery.Where(p => p.Disponible);

             List<Producto> Productos = await productosQuery
            .Take(6)
            .ToListAsync();

            return Productos;
        }
    }
}
