using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace P_LAB
{
    internal class Program
    {
        static bool validar_datos(double precio, int cantidad)
        {
            if (precio > 0 && cantidad > 0)
            {
                return true;
            }
            else 
            {
              Console.WriteLine("Error: El precio y la cantidad deben ser mayores que cero.");
            }
            return false;
        }
        static void ImprimirMatriz(double[,] matriz, string[] codigos)
        {
          Console.WriteLine("Matriz de Precios y Cantidades:");
          Console.WriteLine("Fila| código| COL 0(Precio $)| COL 1(Cantidad)");
          Console.WriteLine("-------------------------------------------------");
          
            for (int i = 0; i < 6; i++)
            {
               Console.WriteLine("["+i+"] | "+codigos[i]+" | "+matriz[i,0].ToString("N2") + " | "+matriz[i,1]);

            }

        }
        static void EncontrarCodigo(string[] codigos, string[] modelos, double[] montos, string codigoBuscado)
        {
           if (codigoBuscado == "") 
           {
                Console.WriteLine("Error: El código no puede estar vacío.");
                return;
           }
           bool encontrado = false;
            for (int i = 0; i < 3; i++) 
            {
                if (codigos[i] == codigoBuscado) 
                {
                    Console.WriteLine("Código encontrado: " + codigos[i]);
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("Código no encontrado: " + codigoBuscado);
            }
        }
        static void Main(string[] args)
        {
            string[] codigos = {"AUTO-201", "AUTO-202", "AUTO-203", "AUTO-204", "AUTO-205", "AUTO-206"};
            string[] modelos = { "SUV Interceptor", "Sedan Executive", "Pickup Titan", "City hatchback", "Electric EV", "Van Carga" };

            double[,] matriz = new double[6, 2];
            double[] precios= { 28000.00, 18500.00, 35000.00, 14000.00, 42000.00, 22500.00 };
            int[] cantidades = { 2, 4, 1, 5, 1,3 };
            for (int i = 0; i < 6; i++)
            {
                matriz[i, 0] = precios[i];
                matriz[i, 1] = cantidades[i];
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("\tCONTROL DE INVENTARIO DE VEHÍCULOS");
            Console.WriteLine("===============================================");

            ImprimirMatriz(matriz, codigos);
            
            double[] montos_netos= new double[6];
            double total_monto=0;
            int ventasPremiun=0;

            for (int i = 0; i < 6; i++) 
            {
                double precio = matriz[i, 0];
                int cantidad = (int)matriz[i, 1];
                if (validar_datos(precio, cantidad) ==true )
                {
                   montos_netos[i] = precio * cantidad;
                    total_monto= total_monto + montos_netos[i];
                    
                    if(montos_netos[i] > 50000)
                    {
                        ventasPremiun++;
                    }


                }
            }
            for (int i=0; i<5; i++)
            {
                for (int j = 0; j < 5; j++) 
                {
                   double auxMonto= montos_netos[j];
                    montos_netos[j] = montos_netos[j + 1];
                    montos_netos[j + 1] = auxMonto;

                    string auxCodigo = codigos[j];
                    codigos[j] = codigos[j + 1];
                    codigos[j + 1] = auxCodigo;

                    string auxModelo = modelos[j];
                    modelos[j] = modelos[j + 1];
                    modelos [j + 1] = auxModelo;
                    for (int c=0; c<2 ; c++)
                    {
                        double auxMatriz = matriz[j, c];
                        matriz[j, c] = matriz[j + 1, c];
                        matriz[j + 1, c] = auxMatriz;
                    }


                }


            }
            Console.WriteLine();
            Console.WriteLine("---REPORTE ORDENADO POR MONTO NETO (DECRECIENTE)---");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("Código: " + codigos[i] + " | Modelo: " + modelos[i] + " | Precio: $" + matriz[i, 0].ToString("N2") + " | Cantidad: " + matriz[i, 1] + " | Monto Neto: $" + montos_netos[i].ToString("N2"));
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("MÉTRICAS RESUMEN :");
            Console.WriteLine("Monto total de ventas: $" + total_monto.ToString("N2"));
            Console.WriteLine("Cantidad de ventas premium (monto neto > $50,000): " + ventasPremiun);



            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("PRUEBA DE BÚSQUEDA DE CÓDIGO DE VEHÍCULO");
            EncontrarCodigo(codigos, modelos, montos_netos, "AUTO-203");
            
            Console.ReadLine();










        }
    }
}
