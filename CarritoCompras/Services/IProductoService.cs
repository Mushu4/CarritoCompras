using CarritoCompras.Models;

namespace CarritoCompras.Services
{
    public interface IProductoService
    {
        Producto GetProducto(int Id);
        Task<List<Producto>> GetProductos();
    }
}
