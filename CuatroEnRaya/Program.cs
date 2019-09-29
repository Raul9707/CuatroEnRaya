using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp20
{
    class Program
    {
        static void Main(string[] args)
        {
            int Eleccion;
            bool esNumero;
            bool finJuego = false;
            do
            {
                Eleccion = 0;
                esNumero = true;
                Console.WriteLine("BIENVENIDO AL JUEGO 4 EN RAYA\n");
                Console.WriteLine("Eliga una Opcion");
                Console.WriteLine("1. Comenzar un nuevo juego");
                Console.WriteLine("2. Continuar juego guardado");
                Console.WriteLine("3. Leer las reglas del juego");
                Console.WriteLine("4. Salir del juego");
                do
                {
                    esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);

                } while (!esNumero);
                ValidarRango(1, ref Eleccion, 4, esNumero);

                Console.Clear();
                switch (Eleccion)
                {
                    case 1:
                        Juego(Eleccion,esNumero);
                        break;

                    case 2:
                        break;

                    case 3:
                        Reglas();
                        break;

                    case 4:
                        finJuego = true;
                        break;
                }

                Console.Clear();
            } while (!finJuego) ;
                   

        }

        static void Juego(int Eleccion, bool esNumero)
        {
            Eleccion = 0;
            esNumero = true;
            Console.WriteLine("ELIGA UN TABLERO PARA JUGAR");
            Console.WriteLine("1. Clásico: 6 filas y 7 columnas.");
            Console.WriteLine("2. Cuadrado chico: 6 filas y 6 columnas.");
            Console.WriteLine("3. Cuadrado grande: 7 filas y 7 columnas.");
            Console.WriteLine("4. Largo: 5 filas y 8 columnas");
            Console.WriteLine("5. Alto: 8 filas y 5 columnas.");
            do
            {
                esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);
            } while (!esNumero);
            ValidarRango(1, ref Eleccion, 4, esNumero);
            int[,] tablero;
            switch (Eleccion)
            {
                case 1:
                    tablero = new int[6, 7];
                    InicializarMatriz(tablero,0);
                    break;

                case 2:
                    tablero = new int[6, 6];
                    InicializarMatriz(tablero, 0);
                    break;

                case 3:
                    tablero = new int[7, 7];
                    InicializarMatriz(tablero, 0);
                    break;

                case 4:
                    tablero = new int[5, 8];
                    InicializarMatriz(tablero, 0);
                    break;
                case 5:
                    tablero = new int[8, 5];
                    InicializarMatriz(tablero, 0);
                    break;
            }
        }

        static void Reglas()
        {
            Console.WriteLine("\tREGLAS DEL JUEGO\n");
            Console.ReadKey();
            Console.WriteLine("\t1. El juego se realiza en un tablero de medidas que el usuario escoga\n");
            Console.ReadKey();
            Console.WriteLine("\t2. Son 2 jugadores, cada jugador tiene una ficha de distinto color al de su oponente\n");
            Console.WriteLine("\t Se colocan las fichas por columna hasta caer encima de la ficha que este por debajo de ella\n");
            Console.WriteLine("\t Para moverte entre Columnas presione Izquieda (<-) o Derecha (->)\n");
            Console.WriteLine("\t Y para soltarla en la columna presione Enter\n");
            Console.ReadKey();
            Console.WriteLine("\t3. Gana quien haya conectado 4 fichas del mismo color en horizontal,vertical o diagonal\n");
            Console.WriteLine("\t Si el tablero se llena de fichas se le considera empate y gana el mejor de 5 rondas\n");
            Console.ReadKey();
            Console.WriteLine("\t4. Cada ronda tendras la posibilidad de tener fichas especiales, se te consedera una de cada una cada ronda\n");
            Console.WriteLine("\t Si no utilizas una ficha especial se te acumulara para la siguiente ronda\n");
            Console.WriteLine("\t Y puedes usar mas de una ficha especial por ronda,cuantas tengas en tu inventario\n");
            Console.WriteLine("\t Para escoger que ficha especial quieres usar presiona las direccionales arriba y abajo \n");
            Console.WriteLine("\t Presionando Space lo activas y lo utilizas\n");
            Console.ReadKey();
            Console.WriteLine("\t5. Guardar tu partida solo debes presionas ESC, escribir el nombre de la partida y automaticamente se guarda\n");
            Console.ReadKey();
            
        }

        static void ValidarRango(int condicionMinimo, ref int num, int condicionMaximo, bool esNumero)
        {
            while (num < condicionMinimo || num > condicionMaximo)
            {
                Console.WriteLine("\nEl valor debe estar entre los valores " + condicionMinimo + " y " + condicionMaximo);
                Console.Write("\nReingrese: ");
                do
                {
                    esNumero = Int32.TryParse(Console.ReadLine(), out num);
                    Console.WriteLine("\n Ingrese un numero valido: ");
                } while (!esNumero);
                Console.Clear();
            }
        }

        static void InicializarMatriz(int[,] matriz, int elemento)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = elemento;
                }
            }
        }
    }
}
