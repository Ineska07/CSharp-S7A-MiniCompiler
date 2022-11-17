using System;

namespace CompiladorCSharp 
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
                if (i == 4 || i < 6)
                {
                    Console.WriteLine("Ya casi!");
                    int y = 5 + 2;
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
