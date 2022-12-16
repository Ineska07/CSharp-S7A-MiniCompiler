using System;

namespace CompiladorCSharp 
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">>Bienvenido a mi proyecto de LEYAU2");

            int i = 0;

            Console.WriteLine(">>>Inserte un número: 1  2  3  4  5  6  7");
            Console.ReadLine(i);

            int x = i + 2;

            if(i == 1)
            {
                Console.WriteLine("jaja cheatin't");
            }
            else
            {
                Console.WriteLine(">>Sumando dos, son 4  >>Uno mas dos son tres");
            }
            Console.WriteLine(">>Programa Finalizado. Felicidades! :D");
        }
    }
}
