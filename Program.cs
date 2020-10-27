using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace T12_Menu_Par_Imp_TablMult
{
    /*
     * @author Erick Escamilla Charco
     */
    public class MathUtils{
        public static bool esPar(int number) {
            return number % 2 == 0 ? true : false;
        }
    }//--FIN: public class MathUtils
    public class InputReader
    {
        public InputReader() {
        }

        public (int limInf, int limSup) ReadIntRange() {
            int limInf, limSup, tmp;
            string errMsg = "El límite DEBE SER UN ENTERO, inténtelo nuevamente.";

            limInf = ReadOption("Ingrese el límite inferior (ENTERO): ", errMsg, false);
            limSup = ReadOption("Ingrese el límite superior (ENTERO): ", errMsg, false);
            if (limSup < limInf)
            {
                Console.WriteLine("El límite superior {0} es < límite inferior {1}, se procede a la CORRECIÓN AUTOMÁTICA...", limSup, limInf);
                tmp = limInf;
                // limSup, limInf swapping  <-->
                limInf = limSup;
                limSup = tmp;
                Console.WriteLine("Límite inferior CORREGIDO: {0}", limInf);
                Console.WriteLine("Límite superior CORREGIDO: {0}", limSup);
            }
            return (limInf, limSup);
        }//--fin: public 

        public int ReadOption(string msg, string errMsg, bool readImmediately) {
            int opt = -1;
            string inputStr;
            bool error = false;

            do {
                if (error) {
                    Console.Error.WriteLine(errMsg);
                }

                try {
                    Console.WriteLine(msg);
                    if (readImmediately) {
                        inputStr =  Char.ToString(Console.ReadKey().KeyChar);
                    }
                    else {
                        inputStr = Console.ReadLine();
                    }

                    opt = Int32.Parse(inputStr);
                    error = false;
                }
                catch (Exception e) {
                    opt = -1;
                    error = true;
                }

            } while (error);

            return opt;
        }

    }//--FIN: public class InputReader

    public class Controller {

        private InputReader _inputReader;
        public Controller() {
            _inputReader = new InputReader();
        }

        public void NumerosPares() {
            /**/
            (int limInf, int limSup) rango;
            char continuar = 'n';
            do {
                Console.Clear();
                Console.WriteLine(">>Números Pares.");
                rango = _inputReader.ReadIntRange();

                Console.WriteLine("Listado de números PARES en el rango [{0}] - [{1}].", rango.limInf, rango.limSup);
                /*En el caso de los números PARES, detectar si el límite Inferior NO ES PAR,
                 de ser así (no par) aumentar su valor en 1, para comenzar el listado en el 
                 número par más cercano al límite inferior indicado por el usuario.*/
                if (!MathUtils.esPar(rango.limInf))
                {
                    rango.limInf += 1;
                }
                Console.WriteLine("------------------------");
                for (int i = rango.limInf; i <= rango.limSup; i += 2)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("\t¿Desea mostrar un nuevo rango de números PARES? [y/n]: ");
                continuar = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continuar).Equals('y'));

        }//--fin: public void NumerosPares

        public void NumerosImpares() {
            (int limInf, int limSup) rango;
            char continuar = 'n';
            do {
                Console.Clear();
                Console.WriteLine(">>Números IMPARES.");
                rango = _inputReader.ReadIntRange();
                Console.WriteLine("Listado de números IMPARES en el rango [{0}] - [{1}].", rango.limInf, rango.limSup);
                /*En el caso de los números IMPARES, se debe detectar si el límite Inferior es PAR, 
                 de ser así, incrementar su valor en 1 (uno)*/
                if (MathUtils.esPar(rango.limInf)) {
                    rango.limInf += 1;
                }
                Console.WriteLine("--------------------------------");
                for (int i = rango.limInf; i <= rango.limSup; i+=2) {
                    Console.WriteLine(i);
                }
                Console.WriteLine("--------------------------------");
                Console.WriteLine("\t¿Desea mostrar un nuevo rango de números IMPARES? [y/n]: ");
                continuar = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continuar).Equals('y'));
        }//--fin: public void NumerosImpares

        // TODO: research Action<>, Func<> official MS documentation.
        public void TablaMultiplicar() {
            int numero;
            char continuar = 'n';
            do {
                Console.Clear();
                numero = _inputReader.ReadOption("¿Qué tabla de multiplicar desea mostrar? [2, 3, 4, etc.]: ", "La tabla debe ser indicada por un NÚMERO ENTERO, inténtelo de nuevo, por favor.", false);
                Console.WriteLine("Tabla de multiplicar del {0}.", numero);
                Console.WriteLine("------------------------");
                for (int i = 1; i <= 10; i++) {
                    Console.WriteLine("{0} x {1} = {2}", numero, i, numero * i);
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("\t¿Desea mostrar otra Tabla de multiplicar? [y/n]: ");
                continuar = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continuar).Equals('y'));
        }//--fin: public void TablaMultiplicar
    }//--FIN: public class Controller


    public class App {

        private InputReader _inputReader;
        private Controller _controller;
        public App() {
            _inputReader = new InputReader();
            _controller = new Controller();
        }
        public void execute() {
            int option = -1;

            do {
                Console.Clear();
                Console.WriteLine("####--Menú--####");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("\t1. Números pares.");
                Console.WriteLine("\t2. Números impares.");
                Console.WriteLine("\t3. Tabla de multiplicar.");
                Console.WriteLine("\t4. Salir.");

                option = _inputReader.ReadOption("Seleccione una opción.", "La opción ingresada NO ES VÁLIDA, debe especificar un valor ENTERO.", true);

                switch (option) {
                    case 1:
                        _controller.NumerosPares();
                        break;
                    case 2:
                        _controller.NumerosImpares();
                        break;
                    case 3:
                        _controller.TablaMultiplicar();
                        break;
                    case 4:
                        Console.WriteLine("\nHasta la próxima, gracias por utilizar Multitron Super v1.0");
                        break;
                    default:
                        Console.WriteLine("Opción NO VÁLIDA, ingrese un entero en el rango 1 a 4 inclusive.");
                        break;
                }

            } while (option != 4);

            Thread.Sleep(2000);

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.execute();
        }
    }
}
