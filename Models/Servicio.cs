namespace SalonManagementSystem.Models
{
    public class Servicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public decimal PorcentajeComision { get; set; }
    }
}