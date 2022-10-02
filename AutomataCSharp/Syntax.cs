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
        public bool semError = false;

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
            /*NullReferenceException con p al momento de error de ultima cosa (ej: ;)*/
            LinkedListNode<Tokens> tempP = p;
            if (p == null) tempP = p.Previous;

            string type;
            switch (e)
            {
                case -600: type = "Error de Sintaxis"; break;
                default:
                    type = "Se esperaba " + s;
                    break;
            }
            
            Error error = new Error(type, e, tempP.Value.Linea);
            syntaxError.AddLast(error);
            hasSyntaxErrors = true;
            return tempP;
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
                    case -41:
                        p = p.Next;
                        p = Class(p); 
                        break;
                    case -44:
                        p = p.Next;
                        p = Namespace(p); 
                        break;
                    case -86:
                        p = p.Next;
                        p = Libraries(p); 
                        break;
                    case -45:
                        p = p.Next;
                        p = MainMethod(p);
                        break;
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
                            p = ConsoleSentence(p);
                        }
                        else if (p.Value.Lexema == "if")
                        {
                            p = If(p);
                        }
                        else if (p.Value.Lexema == "while")
                        {
                            p = While(p);
                        }
                        else p = AddSyntaxError(p, -600, "0");
                        break;
                }
            }
        }

        #region Statements
        private LinkedListNode<Tokens> VartypeDeclaration(LinkedListNode<Tokens> p)
        {
            //Entrada: int
            p = p.Next;


            return p;
        }

        private LinkedListNode<Tokens> VarDeclaration(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            p = p.Next;
            return p;
        }

        private LinkedListNode<Tokens> ConsoleSentence(LinkedListNode<Tokens> p)
        {
            //Entrada Console
            p = p.Next;
            if(p.Value.Lexema == ".")
            {
                if (p.Value.Lexema == "WriteLine")
                {
                    if (p.Value.Lexema == "WriteLine")
                    {
                        p = p.Next;
                        if (p.Value.Lexema == "(")
                        {
                            p = PrintAssignment(p);

                            if (p.Value.Lexema == ")")
                            {
                                p = p.Next;
                                if (p.Value.Lexema == ";")
                                {
                                    return p;
                                }
                            }
                        }
                    }
                }
                else if (p.Value.Lexema == "ReadLine")
                {
                    p = p.Next;
                    if (p.Value.Lexema == "(")
                    {
                        p = p.Next;
                        if (p.Value.Lexema == ")")
                        {
                            p = p.Next;
                            if (p.Value.Lexema == ";")
                            {
                                return p;
                            }
                        }
                    }
                }
            }

            return p;
        }
        #endregion

        #region Espacios
        private LinkedListNode<Tokens> Libraries(LinkedListNode<Tokens> p)
        {
            //Entrada: using
            p = p.Next;
            if (p != null && p.Value.Lexema == "System")
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == ";") return p;
                else p = AddSyntaxError(p, -605, ";");
            }
            else p = AddSyntaxError(p, -601, "Nombre de Librería");

            return p;
        }

        private LinkedListNode<Tokens> Namespace(LinkedListNode<Tokens> p)
        {
            //Entrada namespace
            p = p.Next;
            if (p != null && p.Value.Valor == -1)
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "{") 
                {
                    p = Block(p);
                    return p; 
                }
                else p = AddSyntaxError(p, -605, "{");
            }
            else p = AddSyntaxError(p, -601, "Identificador");
            return p;
        }
        private LinkedListNode<Tokens> Block(LinkedListNode<Tokens> p)
        {
            while (p != null)
            {
                switch (p.Next.Value.Valor)
                {
                    case -34: break; //}
                    //AnalisisSintactico() si se puede ._.XD
                }
               if (p.Value.Lexema == "}") break;
            }
          return p;
        }

        private LinkedListNode<Tokens> Class(LinkedListNode<Tokens> p)
        {
            //entrada class
            p = p.Next;
            if (p != null && p.Value.Valor == -1)
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "{")
                {
                    p = Block(p);
                    return p;
                }
            }
            else p = AddSyntaxError(p, -601, "Identificador");
            return p;
        }
        private LinkedListNode<Tokens> MainMethod(LinkedListNode<Tokens> p)
        {
            //Entrada static
            return p;
        }

        #endregion

        private LinkedListNode<Tokens> If(LinkedListNode<Tokens> p)
        {
            //Entrada if
            return p;
        }
        private LinkedListNode<Tokens> While(LinkedListNode<Tokens> p)
        {
            //Entrada while
            p = p.Next;
            if (p.Value.Lexema == "(")
            {
                p = p.Next;
                if (p.Value.Valor == -1)
                {
                    p = Conditional(p);
                    p = p.Next;
                    if (p.Value.Lexema == ")")
                    {
                        p = p.Next;
                        if (p.Value.Lexema == "{")
                        {
                            p = p.Next;
                            if (p.Value.Lexema == "}") return p;
                        }
                    }
                }
                else if (boolval.ContainsKey(p.Value.Valor))
                {
                    p = p.Next;
                    if (p.Value.Lexema == ")")
                    {
                        p = p.Next;
                        if (p.Value.Lexema == "{")
                        {
                            p = p.Next;
                            if (p.Value.Lexema == "}") return p;
                        }
                    }
                }
            }
            return p;
        }
        private LinkedListNode<Tokens> PrintAssignment(LinkedListNode<Tokens> p)
        {
            //Entrada (
            return p;
        }

        private LinkedListNode<Tokens> Conditional(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            p = p.Next;
            if (relsymbol.ContainsKey(p.Value.Valor))
            {
                p = p.Next; // == id
                if (p.Value.Valor == -1 || p.Value.Valor == -2 || p.Value.Valor == -3)
                {
                    p = p.Next; // == id &&
                    if (logicsymbol.ContainsKey(p.Value.Valor))
                    {
                        p = p.Next; // == id && id
                        p = Conditional(p);
                        if (p.Value.Lexema == ")") return p;
                    }
                }
            }
            return p;
        }
    }
}