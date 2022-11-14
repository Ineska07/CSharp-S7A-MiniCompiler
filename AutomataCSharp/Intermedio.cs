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
            {"=", 8 },

            {"WriteLine", 7},
            {"ReadLine", 7 },

            {"&&", 6 },
            {"||", 6},
            {"==", 5 },
            {"!=", 4 },
            {"<=", 4 },
            {"<", 4 },
            {">", 4 },
            {">=", 4 },

            {"/", 2 },
            {"*", 2 },
            {"-", 1 },
            {"+", 1 },
        };

        private Dictionary<string, string> Pointers = new Dictionary<string, string>()
        {
            {"if", "A" },
            {"else", "B" },
            {"while", "C" },
            {"if", "D" },
            //A ver si se usan en los apuntadores
        };

        private LinkedList<Tokens> Tokens = new LinkedList<Tokens>();
        private LinkedList<Tokens> LinkedPolish = new LinkedList<Tokens>(); //Para la lista real del Polish que se usa en la programación
        List<string> Polish = new List<string>(); //Para la tablita del Polish en la interfaz
        int PunteroCount = 0; //Contador para saber el apuntador


        public Intermedio(Queue<Tokens> tokens)
        {
            foreach (Tokens item in tokens) { Tokens.AddLast(item); }

            LinkedListNode<Tokens> head = new LinkedListNode<Tokens>(new Tokens("HEAD", "NODO", 0, 1));
            Tokens.AddFirst(head);

            CreatePolish();
        }

        private void CreatePolish()
        {
            LinkedListNode<Tokens> p = Tokens.First;

            while (p != null)
            {
                int currentline = p.Value.Linea;
                switch (p.Value.Lexema)
                {
                    //Se irá hasta el main en dado caso de que haya, en donde se supone que están las sentencias
                    case "class":
                    case "using":
                    case "namespace":
                    case "static":
                        while (p.Value.Linea == currentline) p = p.Next; 
                        break;

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
        }

        public LinkedListNode<Tokens> SentencePolish(LinkedListNode<Tokens> p)
        {
            //Apuntador...?
            //Nomás recorre la sentencia ._.XD

            return p;
        }

        public LinkedListNode<Tokens> WhilePolish(LinkedListNode<Tokens> p)
        {
            //entra while - NO METER AL POLISH
            //Meter Apuntador
            //en ( empezar a crear el posfijo, como no hay errores solo se pone un while
            //llega ), y llega } insertar BRF
            //recorrer sentencias hasta que llegue el }
            //termina el while, saca el BRI

            return p;
        }

        public LinkedListNode<Tokens> IfPolish(LinkedListNode<Tokens> p)
        {
            //EL BNF no se pone hasta saber si entra un else o else if, o solo salida

            PunteroCount++;
            string tempPolish = string.Empty;
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();

            //entra if - NO METER AL POLISH
            p = p.Next;
            while(p.Next.Value.Lexema != ")") //en ( empezar a crear el posfijo, como no hay errores solo se pone un while
            {
                p = p.Next;
                infijo.AddLast(p.Value);
            }
            tempPolish = InfixPosfix(infijo.ToArray());

            p = p.Next; //p == "{"

            //Se recorren las sentencias
            while(p.Value.Lexema != "}")
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
            return p;
        }

        public string InfixPosfix(Tokens[] infix)
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

            //Impresion del posfijo actual
            foreach (Tokens item in res) 
            {
                LinkedPolish.AddLast(item);
                currentpolish += item.Lexema + " "; 
            }
            return currentpolish;
        }

    }
}
