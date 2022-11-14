using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    /// <summary>
    /// aaaaaaaaaaaaa
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

                    //Súponiendo que está dentro del main
                    default:
                        //recorrer sentencia actual

                        break;
                }
            }
        }

        public void InfixPosfix(Tokens[] infix)
        {
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
        }

    }
}
