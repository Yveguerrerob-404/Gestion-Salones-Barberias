using System;
using System.Collections.Generic;
using MySqlConnector;
using SalonManagementSystem.Models;

namespace SalonManagementSystem.Services
{
    public class CitaService
    {
        // 🔗 Conexión MySQL
        private string connectionString =
            "Server=localhost;Database=SalonDB;User ID=root;Password=;";

        // 📝 CREAR CITA
        public int CrearCita(
            Cliente cliente,
            Empleado empleado,
            List<Servicio> servicios)
        {
            int clienteId = 0;
            int empleadoId = 0;
            int citaId = 0;

            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                // =========================================
                // 👤 INSERTAR CLIENTE
                // =========================================
                string qCliente = @"
                    INSERT INTO Clientes (Nombre, Telefono)
                    VALUES (@Nombre, @Telefono);

                    SELECT LAST_INSERT_ID();";

                MySqlCommand cmdCliente =
                    new MySqlCommand(qCliente, conn);

                cmdCliente.Parameters.AddWithValue(
                    "@Nombre",
                    cliente.Nombre);

                cmdCliente.Parameters.AddWithValue(
                    "@Telefono",
                    cliente.Telefono);

                clienteId =
                    Convert.ToInt32(cmdCliente.ExecuteScalar());

                // =========================================
                // 👨‍💼 INSERTAR EMPLEADO
                // =========================================
                string qEmpleado = @"
                    INSERT INTO Empleados (Nombre, Especialidad)
                    VALUES (@Nombre, @Especialidad);

                    SELECT LAST_INSERT_ID();";

                MySqlCommand cmdEmpleado =
                    new MySqlCommand(qEmpleado, conn);

                cmdEmpleado.Parameters.AddWithValue(
                    "@Nombre",
                    empleado.Nombre);

                cmdEmpleado.Parameters.AddWithValue(
                    "@Especialidad",
                    (int)empleado.Especialidad);

                empleadoId =
                    Convert.ToInt32(cmdEmpleado.ExecuteScalar());

                // =========================================
                // 📅 INSERTAR CITA
                // =========================================
                string qCita = @"
                    INSERT INTO Citas
                    (ClienteId, EmpleadoId, Fecha, Estado)

                    VALUES
                    (@ClienteId, @EmpleadoId, @Fecha, @Estado);

                    SELECT LAST_INSERT_ID();";

                MySqlCommand cmdCita =
                    new MySqlCommand(qCita, conn);

                cmdCita.Parameters.AddWithValue(
                    "@ClienteId",
                    clienteId);

                cmdCita.Parameters.AddWithValue(
                    "@EmpleadoId",
                    empleadoId);

                cmdCita.Parameters.AddWithValue(
                    "@Fecha",
                    DateTime.Now);

                cmdCita.Parameters.AddWithValue(
                    "@Estado",
                    "Pendiente");

                citaId =
                    Convert.ToInt32(cmdCita.ExecuteScalar());

                // =========================================
                // ✂️ INSERTAR SERVICIOS
                // =========================================
                foreach (var servicio in servicios)
                {
                    string qServicio = @"
                        INSERT INTO CitaServicios
                        (CitaId, ServicioId)

                        VALUES
                        (@CitaId, @ServicioId)";

                    MySqlCommand cmdServicio =
                        new MySqlCommand(qServicio, conn);

                    cmdServicio.Parameters.AddWithValue(
                        "@CitaId",
                        citaId);

                    cmdServicio.Parameters.AddWithValue(
                        "@ServicioId",
                        servicio.Id);

                    cmdServicio.ExecuteNonQuery();
                }
            }

            return citaId;
        }

        // 📋 MOSTRAR TODAS LAS CITAS
        public void MostrarTodasLasCitas()
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT
                        c.Id,
                        cl.Nombre AS Cliente,
                        e.Nombre AS Empleado,
                        c.Fecha,
                        c.Estado

                    FROM Citas c

                    INNER JOIN Clientes cl
                        ON c.ClienteId = cl.Id

                    INNER JOIN Empleados e
                        ON c.EmpleadoId = e.Id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine(
                        "\n══════════════ LISTA DE CITAS ══════════════");

                    Console.ResetColor();

                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $" ID: {reader["Id"]} " +
                            $"| Cliente: {reader["Cliente"]} " +
                            $"| Empleado: {reader["Empleado"]} " +
                            $"| Fecha: {reader["Fecha"]} " +
                            $"| Estado: {reader["Estado"]}"
                        );
                    }
                }
            }
        }
    }
}