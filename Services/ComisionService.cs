using SalonManagementSystem.Models;

namespace SalonManagementSystem.Services
{
    public class ComisionService
    {
        public decimal CalcularComision(Cita cita)
        {
            if (cita == null || cita.Servicios == null)
                return 0;

            decimal totalComision = 0;

            foreach (var servicio in cita.Servicios)
            {
                if (servicio != null)
                {
                    totalComision += servicio.Precio * servicio.PorcentajeComision;
                }
            }

            return totalComision;
        }
    }
}