using System;

namespace CompiladorCSharp
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">>Bienvenido a mi proyecto de LEYAU2");

            int i = 0;

            Console.WriteLine("Inserte un numero: 2  1  ");
            Console.ReadLine(i);

            if (i == 2)
            {
                Console.WriteLine("Es un lindo numero");
            }
            else
            {
                Console.WriteLine("Buen numero   Es un lindo numero");
            }
            Console.WriteLine("<<Programa Finalizado. Felicidades! :D");
        }
    }
}

