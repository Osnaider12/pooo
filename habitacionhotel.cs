using System;
using System.Collections.Generic;
using System.Linq;

public class HabitacionHotel
{
    // Atributos privados
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;

    // Propiedades públicas
    public int Numero 
    { 
        get { return numero; } 
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            if (value == "Individual" || value == "Doble" || value == "Suite")
                tipo = value;
            else
                throw new ArgumentException("Tipo no válido. Solo se acepta 'Individual', 'Doble' o 'Suite'.");
        }
    }

    public double PrecioPorNoche
    {
        get { return precioPorNoche; }
        set
        {
            if (value > 0)
                precioPorNoche = value;
            else
                throw new ArgumentException("El precio por noche debe ser mayor a 0.");
        }
    }

    public bool Disponible 
    { 
        get { return disponible; } 
    }

    public string ClienteActual 
    { 
        get { return clienteActual; } 
    }

    // Constructor
    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        this.numero = numero;
        this.Tipo = tipo;                   // Utiliza la propiedad para validar
        this.PrecioPorNoche = precioPorNoche; // Utiliza la propiedad para validar
        this.disponible = true;
        this.clienteActual = "";
    }

    // Métodos
    public double Reservar(string nombreCliente, int noches)
    {
        if (this.disponible)
        {
            this.disponible = false;
            this.clienteActual = nombreCliente;
            return this.precioPorNoche * noches;
        }
        return -1;
    }

    public bool Liberar()
    {
        if (!this.disponible)
        {
            this.disponible = true;
            this.clienteActual = "";
            return true;
        }
        return false;
    }

    public string MostrarInfo()
    {
        string estado = disponible ? "Disponible" : $"Ocupada por {clienteActual}";
        return $"Habitación {numero} | Tipo: {tipo} | Precio por noche: ${precioPorNoche:N0} | Estado: {estado}";
    }

    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje >= 0 && porcentaje <= 100)
        {
            double descuento = precioPorNoche * (porcentaje / 100.0);
            precioPorNoche = Math.Max(0, precioPorNoche - descuento);
        }
    }
}

public class Program
{
    public static void Main()
    {
        // 1. Instanciar habitaciones
        HabitacionHotel hab1 = new HabitacionHotel(101, "Individual", 50000);
        HabitacionHotel hab2 = new HabitacionHotel(202, "Doble", 80000);
        HabitacionHotel hab3 = new HabitacionHotel(303, "Suite", 150000);

        List<HabitacionHotel> habitaciones = new List<HabitacionHotel> { hab1, hab2, hab3 };

        // 2. Mostrar estado inicial
        Console.WriteLine("=== ESTADO INICIAL ===");
        foreach (var hab in habitaciones)
        {
            Console.WriteLine(hab.MostrarInfo());
        }
        Console.WriteLine(new string('-', 60));

        // 3. Simulación de acciones
        Console.WriteLine("\n=== SIMULACIÓN DE ACCIONES ===");

        // Reservar 101 a Juana Pérez por 3 noches
        double costo1 = hab1.Reservar("Juana Pérez", 3);
        Console.WriteLine($"Reserva Hab 101 - Costo: ${costo1:N0}");

        // Reservar 202 a Luis Gómez por 2 noches
        double costo2 = hab2.Reservar("Luis Gómez", 2);
        Console.WriteLine($"Reserva Hab 202 - Costo: ${costo2:N0}");

        // Intentar reservar 101 nuevamente (debe fallar)
        double costoFalla = hab1.Reservar("Pedro Mármol", 1);
        if (costoFalla == -1)
            Console.WriteLine("Intento de reservar la 101 nuevamente: Falló (ya está ocupada)");

        // Liberar la 101
        hab1.Liberar();
        Console.WriteLine("Habitación 101 liberada.");

        // Aplicar descuento del 10% a la 303
        hab3.AplicarDescuento(10);
        Console.WriteLine($"Descuento del 10% aplicado a la Hab 303. Nuevo precio: ${hab3.PrecioPorNoche:N0}");

        // Reservar 101 a Osnuaider Marquez por 1 noche
        double costo3 = hab1.Reservar("OSNUAIDER MARQUEZ", 1);
        Console.WriteLine($"Reserva Hab 101 - Costo: ${costo3:N0}");

        Console.WriteLine(new string('-', 60));

        // 4. Mostrar estado final
        Console.WriteLine("\n=== ESTADO FINAL ===");
        foreach (var hab in habitaciones)
        {
            Console.WriteLine(hab.MostrarInfo());
        }
        Console.WriteLine(new string('-', 60));

        // 5. Mensaje final con cantidad de ocupadas
        int ocupadas = habitaciones.Count(h => !h.Disponible);
        Console.WriteLine($"\nHabitaciones ocupadas actualmente: {ocupadas}");
    }
}
