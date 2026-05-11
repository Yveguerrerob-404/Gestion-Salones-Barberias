using System;
using System.Collections.Generic;
using System.Linq;

namespace SalonManagementSystem.Models
{
    public class Cita
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public Cliente? Cliente { get; set; }

        public Empleado? Empleado { get; set; }

        public List<Servicio> Servicios { get; set; } = new List<Servicio>();

        public decimal CalcularTotal()
        {
            return Servicios.Sum(s => s.Precio);
        }
    }
}