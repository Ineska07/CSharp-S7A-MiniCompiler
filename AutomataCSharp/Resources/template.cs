using System;

namespace CompiladorCSharp 
{
    class Programa
    {
        static void Main(string[] args)
        {
            int i = 0;
            int x = 4

            while(i != 5)
            {
                Console.WriteLine("Inserte un numero: ");
                Console.ReadLine(i);

                if (i == 4)
                {
                    Console.WriteLine("Casi pero no");
                    x++;
                }
                else
                {
                    Console.WriteLine(i + " no es correcto");
                    x--;
                }
                Console.WriteLine(x);
            }
            Console.WriteLine("Programa Finalizado. Felicidades! :D");
        }
    }
}
