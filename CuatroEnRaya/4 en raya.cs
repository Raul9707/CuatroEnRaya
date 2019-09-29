using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace ConsoleApp20
{
    class Program
    {
        static void Main(string[] args)
        {
            int Eleccion;
            SoundPlayer menu = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\menu1.wav");
            bool esNumero,finJuego = false,loading; 
            do
            {
                Eleccion = 0;
                esNumero = true;
                loading = false;
                Console.WriteLine("BIENVENIDO AL JUEGO 4 EN RAYA\n");
                Console.WriteLine("Eliga una Opcion");
                Console.WriteLine("1. Comenzar un nuevo juego");
                Console.WriteLine("2. Continuar juego guardado");
                Console.WriteLine("3. Leer las reglas del juego");
                Console.WriteLine("4. Salir del juego");
                Console.Write("Opcion: ");
                do
                {
                    esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);

                } while (!esNumero);
                ValidarRango(1, ref Eleccion, 4, esNumero);

                Console.Clear();
                switch (Eleccion)
                {
                    case 1:
                        menu.Play();
                        Configuracion(loading,menu);

                        break;

                    case 2:
                        menu.Play();
                        loading = true;
                        Configuracion(loading,menu);
                        break;

                    case 3:
                        menu.Play();
                        Reglas();
                        break;

                    case 4:
                        menu.Play();
                        finJuego = true;
                        break;
                }

                Console.Clear();
            } while (!finJuego);

            
        }

        static void Configuracion(bool loading,SoundPlayer menu)
        {
            SoundPlayer menu2 = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\menu2.wav");
            SoundPlayer wrong = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\error.wav");
            int Eleccion = 0;
            bool esNumero = true;
            int[,] tablero = new int[0, 0];
            string Nombre1 = "";
            string Nombre2 = "";
            if (!loading) {
                Console.WriteLine("ELIGA UN TABLERO PARA JUGAR");
                Console.WriteLine("1. Clásico: 6 filas y 7 columnas.");
                Console.WriteLine("2. Cuadrado chico: 6 filas y 6 columnas.");
                Console.WriteLine("3. Cuadrado grande: 7 filas y 7 columnas.");
                Console.WriteLine("4. Largo: 5 filas y 8 columnas");
                Console.WriteLine("5. Alto: 8 filas y 5 columnas.");
                Console.Write("Opción: ");
                do
                {
                    esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);
                    if (!esNumero) wrong.Play();
                } while (!esNumero);
                ValidarRango(1, ref Eleccion, 5, esNumero);
                switch (Eleccion)
                {
                    case 1:
                        tablero = new int[6, 7];
                        menu.Play();
                        InicializarMatriz(tablero, 0);
                        break;

                    case 2:
                        tablero = new int[6, 6];
                        menu.Play();
                        InicializarMatriz(tablero, 0);
                        break;

                    case 3:
                        tablero = new int[7, 7];
                        menu.Play();
                        InicializarMatriz(tablero, 0);
                        break;

                    case 4:
                        tablero = new int[5, 8];
                        menu.Play();
                        InicializarMatriz(tablero, 0);
                        break;
                    case 5:
                        tablero = new int[8, 5];
                        menu.Play();
                        InicializarMatriz(tablero, 0);
                        break;
                }
                
                do
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese su nombre Jugador 1\n");
                    Console.Write("Nombre: ");
                    Nombre1 = Console.ReadLine();
                    menu2.Play();
                } while (Nombre1 == "");
                do
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese su nombre Jugador 2\n");
                    Console.Write("Nombre: ");
                    Nombre2 = Console.ReadLine();
                    if(Nombre2 == "" || Nombre2 == Nombre1) wrong.Play();
                    else menu2.Play();
                } while (Nombre2 == "" || Nombre2==Nombre1);
            }
            
            Juego(tablero,Nombre1,Nombre2,loading,menu,menu2);
            Console.ReadKey();
        }

        static bool Guardar(int[,] tablero,int filaActual,int columnaActual,int filaActual2,int columnaActual2,string Nombre1, string Nombre2, int[] puntajes, int[] cambiaColor, int[] bombaY, int[] fichaTraidora, int turnos,int traidora,bool juega1,SoundPlayer menu2, SoundPlayer error)
        {
            Console.Clear();
            Console.WriteLine("Ingrese el nombre de su partida para ser guardada");
            Console.WriteLine("Solo se acepta letras del abecedario o numeros para guardar archivos");
            string nombreArchivo="";
            int Almacen = 0;
            bool ruptura = false;
            do
            {
                Console.Write("Nombre partida: ");
                nombreArchivo = Console.ReadLine().ToLower();
                foreach (char c in nombreArchivo)
                {
                    Almacen = Convert.ToInt32(c);
                    if (Almacen < 48 || Almacen > 57) ruptura = true;
                    if (Almacen >= 97 && Almacen <= 122) ruptura = false;
                    if (ruptura) break;
                }
                if (ruptura) error.Play();
            } while (ruptura);
            nombreArchivo += ".dat";
            menu2.Play();
            FileStream miArchivo = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(miArchivo);
            bw.Write(tablero.GetLength(0));
            bw.Write(tablero.GetLength(1));
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    bw.Write(tablero[i, j]);
                }
            }
            bw.Write(filaActual);
            bw.Write(columnaActual);
            bw.Write(filaActual2);
            bw.Write(columnaActual2);
            bw.Write(Nombre1);
            bw.Write(Nombre2);
            bw.Write(puntajes[0]);
            bw.Write(puntajes[1]);
            bw.Write(cambiaColor[0]);
            bw.Write(cambiaColor[1]);
            bw.Write(bombaY[0]);
            bw.Write(bombaY[1]);
            bw.Write(fichaTraidora[0]);
            bw.Write(fichaTraidora[1]);
            bw.Write(turnos);
            bw.Write(traidora);
            bw.Write(juega1);

            bw.Close();
            miArchivo.Close();
           
            Console.WriteLine("¿Desea salir del juego o desea continuar su partida?");
            Console.WriteLine("1. Salir del juego");
            Console.WriteLine("2. Continuar partida");
            int Eleccion = 0;
            bool esNumero = true, salirJuego = false;
            do
            {
                esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);
            } while (!esNumero);
            ValidarRango(1, ref Eleccion, 2, esNumero);
            switch (Eleccion)
            {
                case 1:
                    salirJuego = true;
                    break;
                case 2:
                    break;
            }
            Console.Clear();
            return salirJuego;
        }


        static void CargarPartida(ref int[,] tablero,ref int filaActual,ref int columnaActual,ref int filaActual2,ref int columnaActual2,ref string Nombre1,ref string Nombre2, int[] puntajes,int[] cambiaColor, int[] bombaY, int[] fichaTraidora,ref int turnos,ref int traidora,ref bool juega1,SoundPlayer menu2, SoundPlayer error)
        {
            Console.WriteLine("Escribe el archivo que desea abrir");
            string nombreArchivo="";
            do
            {
                Console.Write("Nombre archivo: ");
                nombreArchivo = Console.ReadLine().ToLower();
                nombreArchivo += ".dat";
                if (!File.Exists(nombreArchivo)) error.Play();
            } while(!(File.Exists(nombreArchivo)));

                menu2.Play();
                FileStream miArchivo = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(miArchivo);
                tablero = new int[br.ReadInt32(), br.ReadInt32()];
                for (int i = 0; i < tablero.GetLength(0); i++)
                {
                    for (int j = 0; j < tablero.GetLength(1); j++)
                    {
                        tablero[i, j] = br.ReadInt32();
                    }
                }
                filaActual = br.ReadInt32();
                columnaActual = br.ReadInt32();
                filaActual2 = br.ReadInt32();
                columnaActual2 = br.ReadInt32();
                Nombre1 = br.ReadString();
                Nombre2 = br.ReadString();
                puntajes[0] = br.ReadInt32();
                puntajes[1] = br.ReadInt32();
                cambiaColor[0] = br.ReadInt32();
                cambiaColor[1] = br.ReadInt32();
                bombaY[0] = br.ReadInt32();
                bombaY[1] = br.ReadInt32();
                fichaTraidora[0] = br.ReadInt32();
                fichaTraidora[1] = br.ReadInt32();
                turnos = br.ReadInt32();
                traidora = br.ReadInt32();
                juega1 = br.ReadBoolean();
                ImprimirMatriz(tablero,Nombre1,Nombre2,puntajes,bombaY,cambiaColor,fichaTraidora);
                br.Close();
                miArchivo.Close();
            Console.Clear();
        }


        static void Reglas()
        {
            SoundPlayer reglas = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\move.wav");
            Console.WriteLine("\tREGLAS DEL JUEGO\n");
            Console.ReadKey();
            Console.WriteLine("\t1. El juego se Realiza en un tablero de medidas que el usuario escoga\n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t2. Son 2 jugadores, cada jugador tiene una ficha de distinto color al de su oponente\n");
            Console.WriteLine("\t Se colocan las fichas por columna hasta caer encima de la ficha que este por debajo de ella\n");
            Console.WriteLine("\t Para moverte entre Columnas presione Izquieda (<-) o Derecha (->)\n");
            Console.WriteLine("\t Y para soltarla en la columna presione ENTER\n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t3. Gana la ronda quien haya conectado 4 fichas del mismo color en horizontal,vertical o diagonal\n");
            Console.WriteLine("\t Si el tablero se llena de fichas se le considera empate y no se tomara en cuenta esa ronda\n");
            Console.WriteLine("\t Gana el juego el usuario que haya acumulado mas puntos en las 5 rondas \n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t4. Cada ronda tendras la posibilidad de tener fichas especiales\n");
            Console.WriteLine("\t Presionando la tecla SPACE activas la seleccion de las fichas especiales y escoges cual mas te convenga\n");
            Console.WriteLine("\t4.1 Si presionas 1 escogeras la bomba-Y, tendras una ficha bomba-Y cada ronda y son acumulables\n");
            Console.WriteLine("\t La funcion de la bomba-Y es destruir una columna del tablero\n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t4.2 Si presionas 2 escogeras la bomba-X,esta se activa si sacrificas 2 fichas bomba-Y\n");
            Console.WriteLine("\t La bomba-X destruye una fila del tablero\n");
            Console.WriteLine("\t4.3 Si presionas 3 escogeras CambiaColor, tendras una ficha CambiaColor en todo el juego\n");
            Console.WriteLine("\t La funcion del CambiaColor es cambiar tus colores de fichas por las de tu oponente\n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t4.4 Si presionas 4 escogeras la Traidora, tendras una ficha Traidora en todo el juego\n");
            Console.WriteLine("\t Quien la active le cambiara la ficha en el siguiente turno a su oponente por una ficha valida para los dos\n");
            Console.WriteLine("\t El oponente estara forzado a jugar esa ficha en ese turno siendole imposible activar movimientos especiales\n");
            Console.WriteLine("\t Si activaste la traidora seguira siendo tu turno pero tampoco puedes activar otro movimiento especial\n");
            Console.WriteLine("\t4.5 Si presionas 5 saldras de la seleccion de fichas especiales y podras seguir jugando normalmente \n");
            reglas.Play();
            Console.ReadKey();
            Console.WriteLine("\t5. Guardar tu partida debes presionar F1, escribir el nombre de la partida y automaticamente se guarda\n");
            Console.WriteLine("\t5.1 Para cargar una partida debes presionas F2 y poner el nombre del documento que estas buscando\n");
            reglas.Play();
            Console.ReadKey();
            reglas.Play();
        }

        static void ValidarRango(int condicionMinimo, ref int num, int condicionMaximo, bool esNumero)
        {
            SoundPlayer wrong = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\error.wav");
            while (num < condicionMinimo || num > condicionMaximo)
            {
                wrong.Play();
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

        static Tuple<int, int> PosicionarRandom(int[,] matriz, int elemento)
        {
            int fila, columna;
            Random rdm = new Random((int)DateTime.Now.Ticks);
            do
            {
                fila = 0;
                columna = rdm.Next(0, matriz.GetLength(1));
            } while (matriz[fila, columna] != 0);
            matriz[fila, columna] = elemento;
            return Tuple.Create(fila, columna);
        }

        static void ImprimirMatriz(int[,] matriz,int elemento)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = elemento;
                    switch (matriz[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ■ \t");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" ■ \t");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" ■ \t");
                            break;
                    }
                }
                Console.WriteLine("\n");
                System.Threading.Thread.Sleep(250);
            }
        }

        static void ImprimirMatriz(int[,] matriz,string Nombre1, string Nombre2,int[] puntajes,int[] bombaY,int[] cambiaColor,int[] Traidora)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t!!!4 EN RAYA!!!");
       
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("<- = Izquierda      Enter=Soltar ficha      -> = Derecha");
            Console.WriteLine("SPACE+1= Bomba-Y (destruye una columna del tablero)");
            Console.WriteLine("SPACE+2= Bomba-X (destruye una fila del tablero sacrificando 2 fichas bomba-Y)");
            Console.WriteLine("SPACE+3= CambiaColor (cambia el color de tus fichas por las de tu oponente)");
            Console.WriteLine("SPACE+4= Traidora (obliga a tu oponente en su siguiente turno jugar una ficha valida para los dos");
            Console.WriteLine("SPACE+5= Salir del menú de poderes especiales\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■ P1:" + Nombre1 + "-score1:" + puntajes[0]);


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t■ P2:" + Nombre2 + "-score2:" + puntajes[1] + "\n\n");

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    switch (matriz[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ■ \t");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" ■ \t");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" ■ \t");
                                break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" ■ \t");
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                if(i == 0) Console.Write(" Poderes especiales de " + Nombre1 + " y " + Nombre2);
                if(i == 1) Console.Write(" SPACE+1= Bomba-Y: \t" + bombaY[0] + " , " + bombaY[1]);
                if(i == 2) Console.Write(" SPACE+2= Bomba-X: \t" + bombaY[0]/2 + " , " + bombaY[1]/2 );
                if(i == 3) Console.Write(" SPACE+3= CambiaColor:  " + cambiaColor[0] + " , " + cambiaColor[1]);
                if(i == 4) Console.Write(" SPACE+4= Traidora: \t" + Traidora[0] + " , " + Traidora[1]);
                if(i == 5) Console.Write(" SPACE+5= Salir menú");
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("F1 = Guardar partida ");
            Console.WriteLine("F2 = Cargar partida ");

        }

        static void Juego(int[,] tablero,string Nombre1,string Nombre2,bool loading,SoundPlayer menu,SoundPlayer menu2)
        {
            int[] puntajes = new int[2], bombaY = new int[2], cambiaColor = new int[2] { 1, 1 }, fichaTraidora = new int[2] { 1, 1 };
            bool scaner,salirJuego = false,juega1 = false,esNumero=true;
            int filaActual = 0, columnaActual = 0, filaActual2 = 0, columnaActual2 = 0, win1 = 0, win2 = 0, azar = 0, Eleccion = 0,turnos=0,traidora=0;
            Random rdm = new Random((int)DateTime.Now.Ticks);
            SoundPlayer movimiento = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\move.wav");
            SoundPlayer enter = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\enter.wav");
            SoundPlayer explosion = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\explosion.wav");
            SoundPlayer buff = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\buff.wav");
            SoundPlayer wrong = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\error.wav");
            do
            {
                turnos = 1; win1 = 0; win2 = 0; traidora = 0; azar = rdm.Next(0, 2);
                bombaY[0]++; bombaY[1]++;
                InicializarMatriz(tablero, 0);
                do
                {
                    if (loading)
                    {
                        menu.Play();
                        CargarPartida(ref tablero,ref filaActual,ref columnaActual,ref filaActual2,ref columnaActual2,ref Nombre1, ref Nombre2, puntajes,cambiaColor,bombaY,fichaTraidora, ref turnos,ref traidora,ref juega1,menu2,wrong);
                        loading = false;
                    }
                    else {
                        Iniciador(tablero, turnos, ref traidora, azar, ref filaActual, ref columnaActual, ref filaActual2, ref columnaActual2, ref juega1);
                    }
                        
                    Console.Clear();
                    ImprimirMatriz(tablero,Nombre1,Nombre2,puntajes,bombaY, cambiaColor, fichaTraidora);
                    ConsoleKeyInfo direccion;
                    do
                    {
                        scaner = true;

                        direccion = Console.ReadKey(true);

                        if (direccion.Key != ConsoleKey.RightArrow && direccion.Key != ConsoleKey.LeftArrow && direccion.Key != ConsoleKey.Enter && direccion.Key != ConsoleKey.Spacebar && direccion.Key != ConsoleKey.F1 && direccion.Key != ConsoleKey.F2) scaner = false;
                        switch (direccion.Key)
                        {
                            case ConsoleKey.RightArrow:
                                movimiento.Play();
                                if (juega1) MoverLateral(tablero, ref filaActual, ref columnaActual, ref scaner, filaActual, columnaActual + 1);
                                else MoverLateral(tablero, ref filaActual2, ref columnaActual2, ref scaner, filaActual2, columnaActual2 + 1);
                                break;
                            case ConsoleKey.LeftArrow:
                                movimiento.Play();
                                if (juega1) MoverLateral(tablero, ref filaActual, ref columnaActual, ref scaner, filaActual, columnaActual - 1);
                                else MoverLateral(tablero, ref filaActual2, ref columnaActual2, ref scaner, filaActual2, columnaActual2 - 1);
                                break;
                            case ConsoleKey.Enter:
                                enter.Play();
                                if (juega1) Caida(tablero, ref filaActual, ref columnaActual, ref scaner, filaActual + 1, columnaActual, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
                                else Caida(tablero, ref filaActual2, ref columnaActual2, ref scaner, filaActual2 + 1, columnaActual2, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
                                if (turnos > 6)
                                {
                                    Tuple<int, int> win = Mapeador(tablero);
                                    win1 = win.Item1;
                                    win2 = win.Item2;
                                }
                                break;
                            case ConsoleKey.Spacebar:
                                Console.Write("ficha especial: ");
                                if (traidora > 0) {
                                    Console.WriteLine("\nLa Traidora esta activada, no puedes usar poderes especiales");
                                    do
                                    {
                                        esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);
                                    } while (!esNumero);
                                    ValidarRango(5, ref Eleccion, 5, esNumero);
                                }
                                else {
                                    do
                                    {
                                        esNumero = Int32.TryParse(Console.ReadLine(), out Eleccion);
                                    } while (!esNumero);
                                    ValidarRango(1, ref Eleccion, 5, esNumero);
                                }

                                switch (Eleccion)
                                {
                                    case 1:
                                        //bomba-Y
                                        if (juega1)
                                        {
                                            if (bombaY[0] >= 1) {
                                                menu2.Play();
                                                MovBombaY(tablero, ref filaActual, ref columnaActual, ref scaner, filaActual + 1, columnaActual, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
                                                bombaY[0]--;
                                                explosion.Play();
                                            }
                                            else {
                                                wrong.Play();
                                                Console.WriteLine("Jugador " + Nombre1 + " no tiene suficientes fichas Bomba Y,debe tener minimo 1");
                                                Console.ReadKey();
                                                scaner = false;
                                            }
                                        }
                                        else {
                                            if (bombaY[1] >= 1) {
                                                menu2.Play();
                                                MovBombaY(tablero, ref filaActual2, ref columnaActual2, ref scaner, filaActual2 + 1, columnaActual2, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
                                                bombaY[1]--;
                                                explosion.Play();
                                            }
                                            else {
                                                wrong.Play();
                                                Console.WriteLine("Jugador " + Nombre2 + " no tiene suficientes fichas Bomba Y,debe tener minimo 1");
                                                Console.ReadKey();
                                                scaner = false;
                                            }
                                        }
                                        break;
                                    case 2:
                                        //bomba-X
                                        if (juega1)
                                        {
                                            if (bombaY[0] >= 2)
                                            {
                                                menu2.Play();
                                                MovBombaX(tablero, ref filaActual, ref columnaActual, ref scaner, filaActual + 1, columnaActual, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora, explosion);
                                                bombaY[0] -= 2;
                                            }
                                            else
                                            {
                                                wrong.Play();
                                                Console.WriteLine("Jugador " + Nombre1 + " no tiene suficientes fichas Bomba Y para usar la Bomba X,debe tener minimo 2");
                                                Console.ReadKey();
                                                scaner = false;
                                            }
                                        }
                                        else
                                        {
                                            if (bombaY[1] >= 2)
                                            {
                                                menu2.Play();
                                                MovBombaX(tablero, ref filaActual2, ref columnaActual2, ref scaner, filaActual2 + 1, columnaActual2, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora, explosion);
                                                bombaY[1] -= 2;
                                            }
                                            else
                                            {
                                                wrong.Play();
                                                Console.WriteLine("Jugador " + Nombre2 + " no tiene suficientes fichas Bomba Y para usar la Bomba X,debe tener minimo 2");
                                                Console.ReadKey();
                                                scaner = false;
                                            }
                                        }
                                        if (turnos > 6)
                                        {
                                            Tuple<int, int> win = Mapeador(tablero);
                                            win1 = win.Item1;
                                            win2 = win.Item2;
                                        }
                                        break;
                                    case 3:
                                        //cambiaColor
                                        CambiaColor(tablero, ref Nombre1, ref Nombre2, cambiaColor, puntajes, bombaY, fichaTraidora, ref juega1, ref scaner,buff,wrong);
                                        break;
                                    case 4:
                                        //Traidora
                                        Traidora(tablero, Nombre1, Nombre2, fichaTraidora, ref juega1, ref scaner, turnos, ref traidora,buff,wrong);
                                        break;
                                    case 5:
                                        menu.Play();
                                        scaner = false;
                                        break;
                                }
                                break;
                            case ConsoleKey.F1:
                                menu.Play();
                                salirJuego = Guardar(tablero, filaActual, columnaActual, filaActual2, columnaActual2, Nombre1, Nombre2, puntajes, cambiaColor, bombaY, fichaTraidora, turnos, traidora, juega1, menu2,wrong);
                                if (juega1) { tablero[filaActual, columnaActual] = 0; juega1 = false;}
                                else { tablero[filaActual2, columnaActual2] = 0; juega1 = true;}
                                break;
                            case ConsoleKey.F2:
                                loading = true;
                                break;
                        }
                        Console.Clear();
                        if (!salirJuego) ImprimirMatriz(tablero, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
                        if (traidora > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Ficha traidora Activada!!!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    } while (!scaner);
                    if (traidora == 1) traidora = 0;
                    turnos++;
                } while (win1 < 4 && win2 < 4 && turnos < (tablero.GetLength(0) * tablero.GetLength(1)+1) && !salirJuego);
                if (!salirJuego) GanadorRonda(win1, win2, Nombre1, Nombre2, puntajes);
                
            } while (puntajes[0]<3 && puntajes[1]<3 && puntajes[0]+puntajes[1] < 5 && !salirJuego);

            if (!salirJuego) GanadorJuego(tablero,Nombre1, Nombre2, puntajes);
        }
        static void Iniciador(int[,] matriz,int turnos,ref int traidora,int azar,ref int filaActual, ref int columnaActual, ref int filaActual2, ref int columnaActual2,ref bool juega1)
        {
            Tuple<int, int> posJugador1, posJugador2;
            if (turnos == 1)
            {
                if (azar % 2 != 0)
                {
                    if (turnos == traidora)
                    {
                        posJugador1 = PosicionarRandom(matriz, 3);
                        traidora = 1;
                    }
                    else posJugador1 = PosicionarRandom(matriz, 1);
                    filaActual = posJugador1.Item1;
                    columnaActual = posJugador1.Item2;
                    juega1 = true;
                }

                else
                {
                    if (turnos == traidora)
                    {
                        posJugador2 = PosicionarRandom(matriz, 3);
                        traidora = 1;
                    }
                    else posJugador2 = PosicionarRandom(matriz, 2);
                    filaActual2 = posJugador2.Item1;
                    columnaActual2 = posJugador2.Item2;
                    juega1 = false;
                }

            }
            else if (!juega1)
            {
                if (turnos == traidora)
                {
                    posJugador1 = PosicionarRandom(matriz, 3);
                    traidora = 1;
                }
                else posJugador1 = PosicionarRandom(matriz, 1);
                filaActual = posJugador1.Item1;
                columnaActual = posJugador1.Item2;
                juega1 = true;
            }
            else if (juega1)
            {
                if (turnos == traidora)
                {
                    posJugador2 = PosicionarRandom(matriz, 3);
                    traidora = 1;
                }
                else posJugador2 = PosicionarRandom(matriz, 2);
                filaActual2 = posJugador2.Item1;
                columnaActual2 = posJugador2.Item2;
                juega1 = false;
            }
        }
        static void MoverLateral(int[,] matriz, ref int filaActual, ref int columnaActual, ref bool scaner, int filaDestino, int columnaDestino)
        {
            if (columnaDestino < 0)
            {
                columnaDestino = matriz.GetLength(1) - 1;
            }
            if (columnaDestino > matriz.GetLength(1) - 1)
            {
                columnaDestino = 0;
            }

            if (filaDestino < 0)
            {
                filaDestino = matriz.GetLength(0) - 1;
            }
            if (filaDestino > matriz.GetLength(0) - 1)
            {
                filaDestino = 0;
            }
            if (matriz[filaDestino, columnaDestino] == 0)
            {
                int elementoAMover = matriz[filaActual, columnaActual];
                matriz[filaActual, columnaActual] = 0;
                matriz[filaDestino, columnaDestino] = elementoAMover;
                filaActual = filaDestino;
                columnaActual = columnaDestino;
            }
            scaner = false;
        }

        static void Caida(int[,] matriz, ref int filaActual, ref int columnaActual, ref bool scaner, int filaDestino, int columnaDestino,string Nombre1,string Nombre2,int[] puntajes,int[]bombaY,int[] cambiaColor,int[] fichaTraidora)
        {
            while (matriz[filaDestino, columnaDestino] == 0)
            {
                int elementoAMover = matriz[filaActual, columnaActual];
                matriz[filaActual, columnaActual] = 0;
                matriz[filaDestino, columnaDestino] = elementoAMover;
                filaActual = filaDestino;
                columnaActual = columnaDestino;
                if(filaDestino<matriz.GetLength(0)-1) filaDestino ++;
                System.Threading.Thread.Sleep(250);
                Console.Clear();
                ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
            }   
        }

        static void MovBombaY(int[,] matriz, ref int filaActual, ref int columnaActual, ref bool scaner, int filaDestino, int columnaDestino, string Nombre1,string Nombre2,int[] puntajes,int[]bombaY, int[] cambiaColor, int[] fichaTraidora)
        {
            while (matriz[filaDestino, columnaDestino] == 0)
            {
                int elementoAMover = matriz[filaActual, columnaActual];
                matriz[filaActual, columnaActual] = 0;
                matriz[filaDestino, columnaDestino] = elementoAMover;
                filaActual = filaDestino;
                columnaActual = columnaDestino;
                if (filaDestino < matriz.GetLength(0) - 1) filaDestino++;
                System.Threading.Thread.Sleep(250);
                Console.Clear();
                ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
            }
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                matriz[i, columnaActual] = 0;
            }
            Console.Clear();
            ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
        }
        static void MovBombaX(int[,] matriz, ref int filaActual, ref int columnaActual, ref bool scaner, int filaDestino, int columnaDestino, string Nombre1, string Nombre2, int[] puntajes,int[]bombaY, int[] cambiaColor, int[] fichaTraidora,SoundPlayer explosion)
        {
            while (matriz[filaDestino, columnaDestino] == 0)
            {
                int elementoAMover = matriz[filaActual, columnaActual];
                matriz[filaActual, columnaActual] = 0;
                matriz[filaDestino, columnaDestino] = elementoAMover;
                filaActual = filaDestino;
                columnaActual = columnaDestino;
                if (filaDestino < matriz.GetLength(0) - 1) filaDestino++;
                System.Threading.Thread.Sleep(250);
                Console.Clear();
                ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes,bombaY, cambiaColor, fichaTraidora);
            }
            int Explosion = columnaDestino,Aux=0,contador=0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                matriz[filaActual, i] = 0;
            }
            Console.Clear();
            ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
            explosion.Play();
            for (int i = filaActual; i > 0; i--)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {

                    Aux = matriz[i-1, j];
                    matriz[i, j] = Aux;
                    matriz[i - 1, j] = 0;
                    if (i-2>=0) if (matriz[i - 2, j] == 0) contador++;
                    else if (contador == matriz.GetLength(0)) i = 0;
                }
                Aux = 0; contador=0;
                System.Threading.Thread.Sleep(250);
                Console.Clear();
                ImprimirMatriz(matriz, Nombre1, Nombre2, puntajes, bombaY, cambiaColor, fichaTraidora);
            }
        }
        static void CambiaColor(int[,]matriz,ref string Nombre1,ref string Nombre2,int[]cambiaColor,int[]puntajes,int[]bombaY,int[]fichaTraidora,ref bool juega1,ref bool scaner,SoundPlayer buff, SoundPlayer error)
        {
            string NombreAux = "";
            int arrayAux = 0;
            if (juega1)
            {
                if (cambiaColor[0] >= 1)
                {
                    buff.Play();
                    NombreAux = Nombre1;
                    Nombre1 = Nombre2;
                    Nombre2 = NombreAux;
                    cambiaColor[0]--;
                    arrayAux = cambiaColor[0];
                    cambiaColor[0] = cambiaColor[1];
                    cambiaColor[1] = arrayAux;
                    arrayAux = 0;
                    arrayAux = puntajes[0];
                    puntajes[0] = puntajes[1];
                    puntajes[1] = arrayAux;
                    arrayAux = 0;
                    arrayAux = bombaY[0];
                    bombaY[0] = bombaY[1];
                    bombaY[1] = arrayAux;
                    arrayAux = 0;
                    arrayAux = fichaTraidora[0];
                    fichaTraidora[0] = fichaTraidora[1];
                    fichaTraidora[1] = arrayAux;
                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        matriz[0, i] = 0;
                    }
                    juega1 = false;
                }
                else
                {
                    error.Play();
                    Console.WriteLine("Jugador " + Nombre1 + " no tiene suficientes fichas cambiaColor,necesita minimo 1");
                    Console.ReadKey();
                    scaner = false;
                }
            }
            else
            {
                if (cambiaColor[1] >= 1)
                {
                    buff.Play();
                    NombreAux = Nombre2;
                    Nombre2 = Nombre1;
                    Nombre1 = NombreAux;
                    cambiaColor[1]--;
                    arrayAux = cambiaColor[1];
                    cambiaColor[1] = cambiaColor[0];
                    cambiaColor[0] = arrayAux;
                    arrayAux = 0;
                    arrayAux = puntajes[1];
                    puntajes[1] = puntajes[0];
                    puntajes[0] = arrayAux;
                    arrayAux = 0;
                    arrayAux = bombaY[1];
                    bombaY[1] = bombaY[0];
                    bombaY[0] = arrayAux;
                    arrayAux = 0;
                    arrayAux = fichaTraidora[1];
                    fichaTraidora[1] = fichaTraidora[0];
                    fichaTraidora[0] = arrayAux;
                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        matriz[0, i] = 0;
                    }
                    juega1 = true;
                }
                else
                {
                    error.Play();
                    Console.WriteLine("Jugador " + Nombre2 + " no tiene suficientes fichas cambiaColor,necesita minimo 1");
                    Console.ReadKey();
                    scaner = false;
                }
            }

        }
        static void Traidora(int[,]matriz,string Nombre1,string Nombre2,int[] fichaTraidora,ref bool juega1,ref bool scaner,int turnos,ref int traidora, SoundPlayer buff, SoundPlayer error)
        {
            if (juega1)
            {
                if (fichaTraidora[0] >= 1)
                {
                    buff.Play();
                    traidora = turnos + 2;
                    fichaTraidora[0]--;
                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        matriz[0, i] = 0;
                    }
                    juega1 = false;
                }
                else
                {
                    error.Play();
                    Console.WriteLine("Jugador " + Nombre1 + " no tiene suficientes fichas Traidoras,necesita minimo 1");
                    Console.ReadKey();
                    scaner = false;
                }

            }
            else
            {
                buff.Play();
                if (fichaTraidora[1] >= 1)
                {
                    traidora = turnos + 2;
                    fichaTraidora[1]--;
                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        matriz[0, i] = 0;
                    }
                    juega1 = true;
                }
                else
                {
                    error.Play();
                    Console.WriteLine("Jugador " + Nombre2 + " no tiene suficientes fichas Traidoras,necesita minimo 1");
                    Console.ReadKey();
                    scaner = false;
                }

            }
        }
        static void GanadorRonda(int win1,int win2,string nombre1,string nombre2,int[] puntajes)
        {
            SoundPlayer winRonda = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\punto1.wav");
            if (win1 > 3)
            {
                puntajes[0]++;
                winRonda.Play();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El ganador de esta ronda es el jugador " + nombre1);
            }
            else if (win2 > 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("El ganador de esta ronda es el jugador " + nombre2);
                winRonda.Play();
                puntajes[1]++;
            }
            else
            {
                Console.WriteLine("esta ronda ha quedado en empate ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        static void GanadorJuego(int[,] matriz,string nombre1,string nombre2,int[] puntajes)
        {
            Console.ReadKey();
            Console.Clear();
            SoundPlayer win = new SoundPlayer("C:\\GameDevelopment\\CuatroEnRaya\\CuatroEnRaya\\Media\\Ganador.wav");
            if (puntajes[0] > puntajes[1]) {
                win.Play();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tEL JUGADOR " + nombre1 + " HA GANADO EL JUEGO 4 EN RAYA!!!\n");
                ImprimirMatriz(matriz, 1);
            }
            else {
                win.Play();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\tEL JUGADOR " + nombre2 + " HA GANADO EL JUEGO 4 EN RAYA!!!\n");
                ImprimirMatriz(matriz, 2);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static Tuple<int, int> Mapeador(int[,] matriz)
        {
            
            int fichaWin1 = 0, fichaWin2 = 0;
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(1); j++)
                    {
                    if (matriz[i, j] == 1 || matriz[i, j] == 3) fichaWin1++;
                    else
                    {
                        if (fichaWin1 < 4) fichaWin1 = 0;
                    }
                    if (matriz[i, j] == 2 || matriz[i, j] == 3) fichaWin2++;
                    else
                    {
                        if (fichaWin2 < 4) fichaWin2 = 0;
                    }
                }
                if (fichaWin1 < 4) fichaWin1 = 0;
                if (fichaWin2 < 4) fichaWin2 = 0;
                }
                
           if(fichaWin1<4 && fichaWin2 < 4)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        if (matriz[i, j] == 1 || matriz[i, j] == 3) fichaWin1++;
                        else
                        {
                            if (fichaWin1 < 4) fichaWin1 = 0;
                        }
                        if (matriz[i, j] == 2 || matriz[i, j] == 3) fichaWin2++;
                        else
                        {
                            if (fichaWin2 < 4) fichaWin2 = 0;
                        }
                    }
                    if (fichaWin1 < 4) fichaWin1 = 0;
                    if (fichaWin2 < 4) fichaWin2 = 0;
                }
            }


            if (fichaWin1 < 4 && fichaWin2 < 4) MatrizDiagonal(matriz,ref fichaWin1, ref fichaWin2);
            return Tuple.Create(fichaWin1, fichaWin2);
        }

        static void MatrizDiagonal(int[,] matriz,ref int fichaWin1,ref int fichaWin2)
        {
        
                int i = matriz.GetLength(0);

                int j = 0;

                do
                {
                    if (i == 0) j++;
                    if (i > 0) i--;
                    fichaWin1 = 0;
                    fichaWin2 = 0;
                CicloInterno(matriz, i, j, 1,ref fichaWin1,ref fichaWin2);
                }
                while ((i >= 0) && (j < (matriz.GetLength(1) - 1)) && fichaWin1 < 4 && fichaWin2 <4);


            if (fichaWin1 < 4 && fichaWin2 < 4) {
                i = matriz.GetLength(0);

                j = matriz.GetLength(1) - 1;
                do
                {
                    if (i == 0) j--;
                    if (i > 0) i--;
                    fichaWin1 = 0;
                    fichaWin2 = 0;
                    CicloInterno(matriz, i, j, 2, ref fichaWin1,ref fichaWin2);
                }
                while ((i >= 0) && j > 0 && fichaWin1 < 4 && fichaWin2 < 4);
            }



        }

        static void CicloInterno(int[,] matriz, int I, int J, int opcion,ref int fichaWin1,ref int fichaWin2)
        {
            if (opcion == 1)
            {
                int i = I++;
                int j = J++;

                while (i <= (matriz.GetLength(0) - 1) && j <= (matriz.GetLength(1) - 1) && fichaWin1 < 4 && fichaWin2 < 4)
                {
                    if (matriz[i, j] == 1 || matriz[i, j] == 3) fichaWin1++;
                    else
                    {
                        if (fichaWin1 < 4) fichaWin1 = 0;
                    }
                    if (matriz[i, j] == 2 || matriz[i, j] == 3) fichaWin2++;
                    else
                    {
                        if (fichaWin2 < 4) fichaWin2 = 0;
                    }
                    i++; j++;
                }
                
            }
            else
            {
                int i = I;
                int j = J;

                while (i <= (matriz.GetLength(0) - 1) && j >= 0 && fichaWin1 < 4 && fichaWin2 < 4)
                {
                    if (matriz[i, j] == 1 || matriz[i, j] == 3) fichaWin1++;
                    else
                    {
                        if (fichaWin1 < 4) fichaWin1 = 0;
                    }
                    if (matriz[i, j] == 2 || matriz[i, j] == 3) fichaWin2++;
                    else
                    {
                        if (fichaWin2 < 4) fichaWin2 = 0;
                    }
                    i++; j--;
                }
               
            }
        }
    }
}
