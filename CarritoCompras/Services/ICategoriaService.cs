using CarritoCompras.Models;

namespace CarritoCompras.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetCategorias();
    }
}
