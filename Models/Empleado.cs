namespace SalonManagementSystem.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public EspecialidadEmpleado Especialidad { get; set; }
        
    }
}