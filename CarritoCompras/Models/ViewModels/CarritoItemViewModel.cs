namespace CarritoCompras.Models.ViewModels
{
    public class CarritoItemViewModel
    {
        public int ProductoId { get; set; }
        public Producto Producto { get;} = null!;
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad;

    }
}
