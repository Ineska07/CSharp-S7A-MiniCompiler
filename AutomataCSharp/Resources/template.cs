using System;

namespace CompiladorCSharp 
{
    class Programa
    {
        static void Main(string[] args)
        {
            int i;

            while(i < 5)
            {
                if (i == 4)
                {
                    Console.WriteLine("Ya casi! - Valor de i = " + i);
                }
                else
                {
                    Console.WriteLine("Valor de i = " + i);
                }
                i++;
            }
            Console.WriteLine("Programa Finalizado");
        }
    }
}
