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

            while(i < 5) //>>0
            {
                if (i == 4 || i < 6) //1 -- BNF>> 2
                {
                    Console.WriteLine("Ya casi!");
                    int y = 5 + 2;
                    //BRI>> 3
                }
                else //2
                {
                    Console.WriteLine("Aun no");
                }
                i++; //3
                //BRI 0
            }
        }
    }
}
