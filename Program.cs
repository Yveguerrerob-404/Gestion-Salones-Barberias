using System;
using System.Collections.Generic;
using MySqlConnector;
using SalonManagementSystem.Models;
using SalonManagementSystem.Services;

class Program
{
    
    static string connectionString =
        "Server=localhost;Database=SalonDB;User ID=root;Password=;";

    static CitaService citaService = new CitaService();

    static void Main(string[] args)
    {
        bool sistemaActivo = true;

       
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [✓] Base de datos conectada correctamente.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Error de conexión:");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            return;
        }

       
        while (sistemaActivo)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║          SISTEMA DE GESTIÓN DE BARBERÍA              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine(" 1.  Ver Agenda");
            Console.WriteLine(" 2.  Registrar Nueva Cita");
            Console.WriteLine(" 3.  Caja (Cobrar)");
            Console.WriteLine(" 4.  Ver Inventario");
            Console.WriteLine(" 5.  Salir");
            Console.WriteLine("────────────────────────────────────────────────────────");

            Console.Write(" >> Seleccione una opción: ");
            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    VerAgenda();
                    break;

                case "2":
                    RegistrarNuevaCita();
                    break;

                case "3":
                    PantallaCaja();
                    break;

                case "4":
                    MostrarInventario();
                    break;

                case "5":
                    sistemaActivo = false;
                    break;

                default:
                    Console.WriteLine(" Opción inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    
    static void VerAgenda()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("────────────── AGENDA DE CITAS ──────────────");
        Console.ResetColor();

        citaService.MostrarTodasLasCitas();

        Console.WriteLine("\n >> Presione una tecla para volver...");
        Console.ReadKey();
    }

  
    static void RegistrarNuevaCita()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("──────────── REGISTRO DE CITA ────────────");
        Console.ResetColor();


        Console.Write(" • Nombre Cliente: ");
        string nombreCliente = Console.ReadLine() ?? "";

        Console.Write(" • Teléfono Cliente: ");
        string telefonoCliente = Console.ReadLine() ?? "";

        Cliente cliente = new Cliente
        {
            Nombre = nombreCliente,
            Telefono = telefonoCliente
        };

     
        Console.Write("\n • Nombre Empleado: ");
        string nombreEmpleado = Console.ReadLine();

        Console.Write(" • Especialidad (0 = Barbero, 1 = Estilista): ");
        int esp = int.Parse(Console.ReadLine());

        Empleado empleado = new Empleado
        {
            Nombre = nombreEmpleado,
            Especialidad = (EspecialidadEmpleado)esp
        };

       
        List<Servicio> disponibles =
            empleado.Especialidad == EspecialidadEmpleado.Barbero
            ? new List<Servicio>
            {
                new Servicio { Id = 1, Nombre = "Corte", Precio = 500, PorcentajeComision = 0.40m },
                new Servicio { Id = 2, Nombre = "Barba", Precio = 300, PorcentajeComision = 0.30m },
                new Servicio { Id = 3, Nombre = "Corte + Barba", Precio = 700, PorcentajeComision = 0.45m },
                new Servicio { Id = 4, Nombre = "Perfilado de cejas", Precio = 200, PorcentajeComision = 0.25m }
            }
            : new List<Servicio>
            {
                new Servicio { Id = 5, Nombre = "Lavado y secado", Precio = 600, PorcentajeComision = 0.35m },
                new Servicio { Id = 6, Nombre = "Tratamiento capilar", Precio = 800, PorcentajeComision = 0.45m },
                new Servicio { Id = 7, Nombre = "Tinte", Precio = 1000, PorcentajeComision = 0.50m },
                new Servicio { Id = 8, Nombre = "Peinado", Precio = 400, PorcentajeComision = 0.30m }
            };

        List<Servicio> seleccionados = new List<Servicio>();

        Console.WriteLine("\n──────── SERVICIOS DISPONIBLES ────────");

        for (int i = 0; i < disponibles.Count; i++)
        {
            Console.WriteLine(
                $" {i + 1}. {disponibles[i].Nombre} - RD${disponibles[i].Precio}");
        }

        Console.Write("\n ¿Cuántos servicios desea agregar?: ");
        int cantidad = int.Parse(Console.ReadLine());

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write($" >> Seleccione servicio #{i + 1}: ");
            int opcion = int.Parse(Console.ReadLine());

            seleccionados.Add(disponibles[opcion - 1]);
        }

  
        int citaId = citaService.CrearCita(
            cliente,
            empleado,
            seleccionados
        );

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n [✓] Cita creada correctamente. ID: {citaId}");
        Console.ResetColor();

        Console.WriteLine("\n >> Presione una tecla para continuar...");
        Console.ReadKey();
    }


    static void PantallaCaja()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("────────────── CAJA ──────────────");
        Console.ResetColor();

        Console.Write("Ingrese el monto a cobrar: RD$");
        decimal monto = decimal.Parse(Console.ReadLine());

        decimal total = monto * 1.18m;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n [✓] Total con ITBIS: RD${total:N2}");
        Console.ResetColor();

        Console.WriteLine("\n Pago procesado correctamente.");

        Console.WriteLine("\n >> Presione una tecla para volver...");
        Console.ReadKey();
    }

    
    static void MostrarInventario()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("──────────── INVENTARIO ────────────");
        Console.ResetColor();

        Console.WriteLine(" Gel para pelo   | 15");
        Console.WriteLine(" Navajas         | 25");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Tinte           | 2  [!] STOCK BAJO");
        Console.ResetColor();

        Console.WriteLine(" Shampoo         | 8");

        Console.WriteLine("\n >> Presione una tecla para volver...");
        Console.ReadKey();
    }
}