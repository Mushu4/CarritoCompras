using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Nombre { get; set; } = null!;
    }
}
