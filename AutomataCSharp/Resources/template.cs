using System;

namespace CompiladorCSharp 
    // Esto es un programa de prueba
{
    class Programa
    {
        static void Main(string[] args)
        {
            string x = "Hola Mundo!";
            int i;
            Console.WriteLine(x);

            while(i < 5)
            {
                if (i == 4)
                {
                    Console.WriteLine("Ya casi!");
                }
                else
                {
                    Console.WriteLine("Aun no");
                }
                i++;
            }
        }
    }
}
