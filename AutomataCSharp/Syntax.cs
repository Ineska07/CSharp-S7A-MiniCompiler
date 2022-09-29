using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Syntax : Lexico
    {
        private LinkedList<Tokens> TokenList = new LinkedList<Tokens>();
        public LinkedList<Tokens> syntaxError = new LinkedList<Tokens>();

        private Dictionary<int, string> vartype = new Dictionary<int, string>();
        private Dictionary<int, string> accesstype = new Dictionary<int, string>();
        private Dictionary<int, string> arisymbol = new Dictionary<int, string>();
        private Dictionary<int, string> assignsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> logicsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> relsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> boolval = new Dictionary<int, string>();
        private Dictionary<int, string> valuetypes = new Dictionary<int, string>();

        public Syntax()
        {
            foreach (Tokens item in tokensGenerados)
            {
                TokenList.AddLast(item);
            }

            valuetypes.Add(-1, "ID");
            valuetypes.Add(-2, "Number");
            valuetypes.Add(-3, "Decimal");
            valuetypes.Add(-4, "String");
            valuetypes.Add(-5, "Char");

            //Aritmético
            arisymbol.Add(-6, "+");
            arisymbol.Add(-7, "-");
            arisymbol.Add(-8, "*");
            arisymbol.Add(-9, "/");

            //Asignativos
            assignsymbol.Add(-15, "++");
            assignsymbol.Add(-16, "--");
            assignsymbol.Add(-19, "=");

            //Relacional
            relsymbol.Add(-17, "<");
            relsymbol.Add(-18, ">");
            relsymbol.Add(-20, ">=");
            relsymbol.Add(-21, "<=");
            relsymbol.Add(-22, "==");
            relsymbol.Add(-24, "!=");

            //Logico
            logicsymbol.Add(-26, "&&");
            logicsymbol.Add(-28, "||");

            //Alcance
            accesstype.Add(-45, "public");
            accesstype.Add(-48, "private");

            //Tipos de Variables
            vartype.Add(-54, "int");
            vartype.Add(-55, "bool");
            vartype.Add(-56, "string");
            vartype.Add(-57, "double");

            boolval.Add(-90, "true");
            boolval.Add(-91, "false");
            AnalizadorSintactico();
        }
        public void AnalizadorSintactico()
        {
            //Apuntador
            TokenList.AddFirst(new LinkedListNode<Tokens>(new Tokens("cabeza","nodo",0,0)));
            LinkedListNode<Tokens> p = TokenList.First;

            do
            {
                switch (p.Value)
                {

                }



            } while ((p != null && p.Next != null));
        }
    }

    
}
