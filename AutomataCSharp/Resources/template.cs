using System;

namespace CompiladorCSharp 
{
    class Programa
    {
        static void Main(string[] args)
        {
            int i = 0;
            int x = 4;

            while(i != 5)
            {
                Console.WriteLine("Inserte un numero: ");
                Console.ReadLine(i);

                if (i == 5)
                {
                    Console.WriteLine("Correcto!");
                    x++;
                }
                else
                {
                    Console.WriteLine("Numero Incorrecto!");
                    x--;
                }
                Console.WriteLine(x);
            }
            Console.WriteLine("Programa Finalizado. Felicidades! :D");
        }
    }
}
