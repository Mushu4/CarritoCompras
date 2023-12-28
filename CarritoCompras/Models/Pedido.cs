using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; } = null!;
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Estado { get; set; } = null!;
        [Required]
        public decimal Total { get; set; }
        public ICollection<Detalle_Pedido> DetallesPedido { get; set; } = null!;
    }
}
