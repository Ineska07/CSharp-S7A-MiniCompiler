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

            while(i < 5) //C0
            {
                if (i == 4 || i < 6) //A1 -- BNF>> B1
                {
                    Console.WriteLine("Ya casi!");
                    int y = 5 + 2;
                    //BRI>> D1
                }
                else //B1
                {
                    Console.WriteLine("Aun no");
                }
                i++; //D1
                //BRI C0
            }
        }
    }
}
