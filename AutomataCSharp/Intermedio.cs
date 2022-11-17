using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    /// <NOTITASXD>
    /// El contador aumentará por cada sentencia while o if anidada
    /// Añadir Apuntador en cada while, if, else y D
    /// </summary>
    class Intermedio
    {
        private Dictionary<string, int> Operandos = new Dictionary<string, int>()
        {
            {"=", 6 },

            {"WriteLine", 5},
            {"ReadLine", 5 },

            {"&&", 4 },
            {"||", 4},
            {"==", 3 },
            {"!=", 3 },
            {"<=", 3 },
            {"<", 3 },
            {">", 3 },
            {">=", 3},

            {"/", 2 },
            {"*", 2 },
            {"-", 1 },
            {"+", 1 },
        };

        private LinkedList<Tokens> Tokens = new LinkedList<Tokens>();
        private LinkedList<string> LinkedPolish = new LinkedList<string>(); //Para la lista real del Polish que se usa en la programación
        public List<string> Polish = new List<string>(); //Para la tablita del Polish en la interfaz

        int PunteroCount = 0; //Contador para saber el apuntador

        public Intermedio(Queue<Tokens> tokens)
        {
            foreach (Tokens item in tokens) 
            { 
                Tokens.AddLast(item); 
            }
        }

        public void CreatePolish()
        {
            LinkedListNode<Tokens> p = Tokens.First;
            while (p != null)
            {
                int currentline = p.Value.Linea;
                switch (p.Value.Lexema)
                {
                    case "class":
                    case "using":
                    case "namespace":
                    case "static":
                        while (p.Value.Linea == currentline) p = p.Next; 
                        break;
                    case "{":
                    case "}":
                        p = p.Next;
                        break;
                    case "if":
                        p = IfPolish(p);
                        break;
                    case "while":
                        p = WhilePolish(p);
                        break;
                    default:
                        p = SentencePolish(p);
                        break;
                }
            }
        }

        private LinkedListNode<Tokens> SentencePolish(LinkedListNode<Tokens> p)
        {
            Dictionary<string, int> IGNORE = new Dictionary<string, int>(){ {"int", 1 }, {"double", 1}, { "string", 1 }, { "bool", 1 },
                {"Console", 2 }, {"(", 2 }, {")", 2 } , { ".", 2 }};

            //Entra con primera cosa que irá en el Polish 
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();
            while (p.Value.Lexema != ";")
            {
                if (IGNORE.ContainsKey(p.Value.Lexema))
                {
                    p = p.Next;
                }
                else
                {
                    infijo.AddLast(p.Value);
                    p = p.Next;
                }
            }
            string tempPolish = InfixPosfix(infijo.ToArray());
            Polish.Add(tempPolish);
            return p.Next;
        }

        private LinkedListNode<Tokens> WhilePolish(LinkedListNode<Tokens> p)
        {
            
            return p;
        }

        private LinkedListNode<Tokens> IfPolish(LinkedListNode<Tokens> p)
        {
            //ENTRA: IF
            //CREAR APUNTADOR A
            int indexif = PunteroCount;
            LinkedPolish.AddLast(">>" + indexif.ToString());

            //Aumentar el apuntador par el BNF
            PunteroCount++; int indexBRF = PunteroCount;

            //Crea el Posfijo de la espresión, ya hecha se mete a la pila.
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();
            p = p.Next;
            while (p.Next.Value.Lexema != ")")
            {
                p = p.Next;
                infijo.AddLast(p.Value);
            }
            string tempPolish = InfixPosfix(infijo.ToArray());

            LinkedPolish.AddLast("BRF>> " + indexBRF.ToString());
            tempPolish += "BRF>> " + indexBRF.ToString();
            Polish.Add(tempPolish);

            //Se recorren las sentencia
            p = p.Next;
            p = Block(p.Next); //retorna en }

            //Añade salto incondicional de if
            PunteroCount++; int indexBRI = PunteroCount;
            LinkedPolish.AddLast("BRI>> " + indexBRI.ToString());
            Polish.Add("BRI>> " + indexBRI.ToString());

            p = p.Next;
            if (p.Value.Lexema == "else")
            {
                //Se añade nombre del apuntador al BNF del IF
                LinkedPolish.AddLast(">" + indexBRF.ToString());
                Polish.Add(">" + indexBRF.ToString());

                p = p.Next; //Apunta a ELSE
                if (p.Next.Value.Lexema == "if") //Si es un else if
                {
                    p = p.Next;
                    p = IfPolish(p);
                }
                else //es ELSE solito
                {
                    p = p.Next;
                    p = Block(p);
                }
            }
            else
            {
                LinkedPolish.AddLast(">" + indexBRF.ToString());
                Polish.Add(">" + indexBRF.ToString());
            }
            return p;
        }

        private LinkedListNode<Tokens> Block(LinkedListNode<Tokens> p)
        {
            //entra {
            p = p.Next;
            while (p.Value.Lexema != "}")
            {
                switch (p.Value.Lexema)
                {
                    case "if":
                        p = IfPolish(p);
                        PunteroCount--;
                        break;
                    case "while":
                        p = WhilePolish(p);
                        PunteroCount--;
                        break;
                    default:
                        p = SentencePolish(p);
                        break;
                }
            }
            //Retornar }
            return p; 
        }

        private string InfixPosfix(Tokens[] infix)
        {
            string currentpolish = string.Empty;

            Stack<Tokens> res = new Stack<Tokens>();
            Stack<Tokens> aux = new Stack<Tokens>();

            for (int i = 0; i < infix.Length; i++)
            {
                if (Operandos.ContainsKey(infix[i].Lexema))
                {
                    if (aux.Count > 0)
                    {
                        int prioridadactual = Operandos[infix[i].Lexema];
                        int prioridadtope = Operandos[aux.Peek().Lexema];

                        while (prioridadtope >= prioridadactual && aux.Count > 0)
                        {
                            Tokens temp = aux.Pop();
                            res.Push(temp);
                        }
                    }
                    aux.Push(infix[i]);
                }
                else
                {
                    res.Push(infix[i]);
                }
            }

            while (aux.Count != 0)
                {
                    Tokens temp = aux.Pop();
                    res.Push(temp);
                }
            foreach (Tokens item in res.Reverse()) 
            {
                LinkedPolish.AddLast(item.Lexema);
                currentpolish += item.Lexema + " "; 
            }
            return currentpolish;
        }
    }
}
