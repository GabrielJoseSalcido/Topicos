using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static int sumaTotal = 0;
    static object lockObject = new object();
    static void CalcularPrimos(object rango)
    {
        (int inicio, int fin) = ((int, int))rango;
        int suma = 0;
        for (int i = inicio; i <= fin; i++)
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }
        lock (lockObject)

        {
            sumaTotal += suma;
        }
    }
    static bool EsPrimo(int numero)
    {
        if (numero < 2) return false;
        for (int i = 2; i * i <= numero; i++)
        {
            if (numero % i == 0) return false;
        }
        return true;
    }
    static void Main()
    {
        Console.WriteLine("Ingrese el número límite:");
        int N = int.Parse(Console.ReadLine());
        Console.WriteLine("Ingrese el número de hilos:");
        int M = int.Parse(Console.ReadLine());
        int rango = N / M;

        // Ejecución secuencial
        Stopwatch tiempo_secuencial = Stopwatch.StartNew();
        for (int i = 0; i <= N; i++)
        {
            if (EsPrimo(i))
            {
                sumaTotal += i;
            }
        }
        tiempo_secuencial.Stop();
        Console.WriteLine("Resultados de ejecución secuencial:");
        Console.WriteLine($"Suma total de números primos hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución: {tiempo_secuencial.ElapsedMilliseconds} ms");

        sumaTotal = 0;
        // Ejecución concurrente
        Thread[] hilos = new Thread[M];
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < M; i++)
        {
            int inicio = i * rango + 1;
            int fin = (i == M - 1) ? N : inicio + rango - 1;
            hilos[i] = new Thread(CalcularPrimos);
            hilos[i].Start((inicio, fin));
        }
        foreach (var hilo in hilos)
        {
            hilo.Join();
        }
        stopwatch.Stop();
        Console.WriteLine("Resultados de ejecución concurrente:");
        Console.WriteLine($"Suma total de números primos hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución: {stopwatch.ElapsedMilliseconds} ms");
    }
}