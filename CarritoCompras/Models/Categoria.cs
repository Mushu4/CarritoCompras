using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.Models
{
    public class Categoria
    {
        public Categoria() { 
            Productos = new List<Producto>();
        }

        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;

        public ICollection<Producto> Productos { get; set; } = null!;

    }
}
