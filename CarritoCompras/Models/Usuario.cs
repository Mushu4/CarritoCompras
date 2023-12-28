using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class Usuario
    {
        public Usuario() {
            Pedidos = new List<Pedido>();
        }
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Telefono { get; set; } = null!;
        [Required]
        public string NombreUsuario { get; set; } = null!;
        [Required]
        public string Contrasena { get; set; } = null!;
        [Required]
        public string Correo { get; set; } = null!;
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Ciudad { get; set; } = null!;
        [Required]
        public string Departamento { get; set; } = null!;
        [Required]
        public string CodigoPostal { get; set; } = null!;
        [Required]
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; } = null!;
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
