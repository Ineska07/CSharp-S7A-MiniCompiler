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
            {"=", 7 },

            {"WriteLine", 6 },
            {"ReadLine", 6 },

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
        List<string> Polish = new List<string>(); //La lista irá por cada línea y se añade a esta lista, habrá un tempPolish
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

        public LinkedListNode<Tokens> SentencePolish(LinkedListNode<Tokens> p)
        {
            //Apundador...?
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
            PunteroCount++;
            string tempPolish = string.Empty;
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();

            //entra if - NO METER AL POLISH
            p = p.Next;
            p = p.Next; //Pal ( xd

            while(p.Next.Value.Lexema != ")") //en ( empezar a crear el posfijo, como no hay errores solo se pone un while
            {
                p = p.Next; //Porque empieza el while con el (
                infijo.AddLast(p.Value);
            }

            tempPolish = InfixPosfix(infijo.ToArray());
            //llega ), insertar BRF

            //recorrer sentencias hasta el else...?
            //Llega else, mete apuntador
            //AQUI ME REVOLVI AAAAAAAAAA
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
            foreach (Tokens item in res) currentpolish += item.Lexema + " ";
            return currentpolish;
        }

    }
}
