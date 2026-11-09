using System;
using System.Collections.Generic;

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
        this.Tipo = tipo;
        this.PrecioPorNoche = precioPorNoche;
        this.disponible = true;
        this.clienteActual = "";
    }

    // Métodos de reserva y liberación
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

    // 1.A Método para aplicar descuento
    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje <= 0 || porcentaje >= 100)
        {
            Console.WriteLine($"[Aviso] Porcentaje de {porcentaje}% no válido. Debe ser entre 1% y 99%.");
            return;
        }

        double descuento = precioPorNoche * (porcentaje / 100.0);
        double nuevoPrecio = precioPorNoche - descuento;

        // Validación: Asegurar que el precio nunca quede en 0 o negativo
        if (nuevoPrecio > 0)
        {
            precioPorNoche = nuevoPrecio;
        }
        else
        {
            Console.WriteLine($"[Aviso] No se pudo aplicar el descuento. El precio final debe ser mayor a $0.");
        }
    }

    // 1.B Método para obtener resumen corto
    public string ObtenerDatosResumen()
    {
        string infoCliente = disponible ? "Disponible" : $"Ocupada por: {clienteActual}";
        return $"[{numero}] {tipo} - ${precioPorNoche:N0} - {infoCliente}";
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

        // 2. Mostrar estado inicial con el formato de resumen corto
        Console.WriteLine("=== ESTADO INICIAL (RESUMEN) ===");
        foreach (var hab in habitaciones)
        {
            Console.WriteLine(hab.ObtenerDatosResumen());
        }
        Console.WriteLine(new string('-', 60));

        // 3. Simulación de acciones
        Console.WriteLine("\n=== SIMULACIÓN DE ACCIONES ===");

        // Reservar 101 a Osnaider marquez 
        double costo1 = hab1.Reservar("Osnaider marquez", 3);
        Console.WriteLine($"Reserva Hab 101 - Costo: ${costo1:N0}");

        // Reservar 202 a Neider Hernandez
        double costo2 = hab2.Reservar("Neider Hernandez", 2);
        Console.WriteLine($"Reserva Hab 202 - Costo: ${costo2:N0}");

        // Aplicar descuento del 10% a la 303
        hab3.AplicarDescuento(10);
        Console.WriteLine($"Descuento aplicado a Hab 303. Nuevo precio: ${hab3.PrecioPorNoche:N0}");

        Console.WriteLine(new string('-', 60));

        // 4. Mostrar estado final (resumen corto)
        Console.WriteLine("\n=== ESTADO FINAL ===");
        foreach (var hab in habitaciones)
        {
            Console.WriteLine(hab.ObtenerDatosResumen());
        }
        Console.WriteLine(new string('-', 60));

        // 5. Métricas requeridas (Puntos 2.A y 2.B)
        Console.WriteLine("\n=== MÉTRICAS DEL HOTEL ===");
        
        int libres = 0;
        int ocupadas = 0;
        double sumaPrecios = 0;

        // Bucle foreach para calcular métricas
        foreach (HabitacionHotel hab in habitaciones)
        {
            if (hab.Disponible)
            {
                libres++;
            }
            else
            {
                ocupadas++;
            }

            sumaPrecios += hab.PrecioPorNoche;
        }

        double precioPromedio = sumaPrecios / habitaciones.Count;

        // Resultados en consola
        Console.WriteLine($"Habitaciones libres: {libres}");
        Console.WriteLine($"Habitaciones ocupadas: {ocupadas}");
        Console.WriteLine($"Precio promedio por noche: ${precioPromedio:N2}");
    }
}
