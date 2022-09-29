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
        public LinkedList<Error> syntaxError = new LinkedList<Error>();

        private Dictionary<int, string> vartype = new Dictionary<int, string>();
        private Dictionary<int, string> accesstype = new Dictionary<int, string>();
        private Dictionary<int, string> arisymbol = new Dictionary<int, string>();
        private Dictionary<int, string> assignsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> logicsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> relsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> boolval = new Dictionary<int, string>();
        private Dictionary<int, string> valuetypes = new Dictionary<int, string>();
        private bool hasSyntaxErrors;

        private LinkedListNode<Tokens> AddSyntaxError(LinkedListNode<Tokens> p, int e, string s)
        {
            string type = "";
            switch (e)
            {
                case -600: type = "Error de Sintaxis"; break;
                default:
                    type = "Se esperaba " + s;
                    break;
            }
            
            Error error = new Error(type, e, 0);
            error.Linea = p.Value.Linea;
            syntaxError.AddLast(error);
            hasSyntaxErrors = true;
            return p;
        }

        public Syntax()
        {
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
        }

        public void AnalizadorSintactico()
        {
            foreach (Tokens item in listaTokens) { TokenList.AddLast(item); }

            LinkedListNode<Tokens> head = new LinkedListNode<Tokens>(new Tokens("HEAD", "NODO", 0, 0));
            TokenList.AddFirst(head);

            //Apuntador
            LinkedListNode<Tokens> p = TokenList.First;

            hasSyntaxErrors = false;
            while (p != null && p.Next != null)
            {
                switch (p.Next.Value.Valor)
                {
                    case -41: p = Class(p); break;
                    case -44: p = Namespace(p); break;
                    case -86: p = Libraries(p); break;
                    default:
                        if (vartype.ContainsKey(p.Value.Valor))
                        {
                            p = VartypeDeclaration(p);
                        }
                        else if (p.Value.Valor == -1 && p.Value.Lexema != "Console")
                        {
                            p = VarDeclaration(p);
                        }
                        else if (p.Value.Lexema == "Console")
                        {

                        }
                        else p = AddSyntaxError(p, -600, "0");
                        break;
                }
                p = p.Next;
            }
        }

        #region Statements
        private LinkedListNode<Tokens> VartypeDeclaration(LinkedListNode<Tokens> p)
        {
            return p;
        }

        private LinkedListNode<Tokens> VarDeclaration(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            return p;
        }

        #endregion

        #region Espacios
        private LinkedListNode<Tokens> Libraries(LinkedListNode<Tokens> p)
        {
            //Entrada: using
            int lineaactual = p.Value.Linea;
            if ((p = p.Next) != null && p.Value.Lexema == "System")
            {
                if ((p = p.Next)!= null && p.Value.Lexema == ";") return p;
                else p = AddSyntaxError(p, -605, ";");
            }
            else p = AddSyntaxError(p, -601, "Identificador");
            return p;
        }

        private LinkedListNode<Tokens> Namespace(LinkedListNode<Tokens> p)
        {
            //Entrada namespace
            return p;
        }

        private LinkedListNode<Tokens> Class(LinkedListNode<Tokens> p)
        {
            //Entrada class
            return p;
        }

        #endregion
    }
}