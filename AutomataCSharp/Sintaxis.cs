using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Sintaxis : Lexico
    {
        private Dictionary<string, int[]> typetokens = new Dictionary<string, int[]>();
        public Queue<Tokens> listasyntaxErrores = new Queue<Tokens>();
        public Queue<Tokens> linetext = new Queue<Tokens>();

        public void StartSyntax()
        {
            AddRules(typetokens);
        }
        private void AddRules(Dictionary<string, int[]> typetokens)
        {
            typetokens.Add("<variabletype>", new int[] {-54, -57, -56, -59, -55, -60, -58}); // int | double | string | char | bool | var | char | float
            typetokens.Add("<arisymbol>", new int[] {-6, -7, -8, -9, -15, -16, -11, -12, -14, -13, -10});  // + | - | * | / | ++ | -- |+= | -= | /= | += | %
            typetokens.Add("<boolvalue>", new int[] { -54, -57, -56, -59, -55, -60, -58 });  // true false
            typetokens.Add("<logicop>", new int[] { -54, -57, -56, -59, -55, -60, -58 });    // < | >= | <=  |  ||  | && | == | != || !
            typetokens.Add("<accesstype>", new int[] { -54, -57, -56, -59, -55, -60, -58 }); // public | private | protected | sealed | partial 

        }
        public void AnalizadorSintactico(int linecount)
        {
            int lineaCodigo = 1;
            int[] txtline;

            for (; lineaCodigo <= linecount; lineaCodigo++ )
            {
                
            }
        }

        private void block() //Bloques {}
        {

        }
        private void id() //Nombre de variable
        {

        }
        private void value() //Valor de variables
        {

        }
    }
    
}
