using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        public string Codigo { get; set; } = null!;
        [Required]
        public string Modelo { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } = null!;
        [Required]
        public int Stock { get; set; }
        [Required]
        public bool Disponible { get; set; }
        public ICollection<Detalle_Pedido> DetallesPedido { get; set; } = null!;

    }
}
