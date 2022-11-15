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

        private LinkedList<Tokens> Tokens = new LinkedList<Tokens>();
        private LinkedList<string> LinkedPolish = new LinkedList<string>(); //Para la lista real del Polish que se usa en la programación

        List<string> Polish = new List<string>(); //Para la tablita del Polish en la interfaz
        List<string> Pointers = new List<string>();

        int PunteroCount = 0; //Contador para saber el apuntador

        public Intermedio(Queue<Tokens> tokens)
        {
            foreach (Tokens item in tokens) 
            { 
                Tokens.AddLast(item); 
            }
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
                    case "class":
                    case "using":
                    case "namespace":
                    case "static":
                        while (p.Value.Linea == currentline) p = p.Next; 
                        break;
                    case "{":
                        p = p.Next;
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
            //Entra con primera cosa que irá en el Polish 
            string tempPolish = string.Empty;
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();

            while (p.Value.Lexema != ";")
            {
                infijo.AddLast(p.Value);
                p = p.Next;
            }
            //tempPolish = InfixPosfix(infijo.ToArray());

            return p;
        }

        public LinkedListNode<Tokens> WhilePolish(LinkedListNode<Tokens> p)
        {
            CrearApuntador(p, "C");

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
            //Apuntador IF - NO METER IF AL POLISH
            CrearApuntador(p, "A");

            string tempPolish = string.Empty;
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();

            p = p.Next;
            while(p.Next.Value.Lexema != ")") //en ( empezar a crear el posfijo, como no hay errores solo se pone un while
            {
                p = p.Next;
                infijo.AddLast(p.Value);
            }
            tempPolish = InfixPosfix(infijo.ToArray());

            p = p.Next; //p == "{"
            PunteroCount++;

            //Crear Apuntador - Se le pondrá valor una vez entre el else o salto.
            LinkedListNode<Tokens> BNF = p;
            Tokens tempBNF = new Tokens(string.Empty, "BRF>> ", 0, p.Value.Linea);

            //Se recorren las sentencias
            while (p.Value.Lexema != "}")
            {
                p = p.Next;
            }

            //Llega }
            PunteroCount--;
            LinkedListNode<Tokens> BRI = p;
            Tokens tempBRI = new Tokens(string.Empty, "BRI>> ", 0, p.Value.Linea); //Se guarda para cuando sepamos el apuntador

            LinkedListNode<Tokens> d = p.Next; //es temporal y apunta al siguiente para saber si es else o no
            if(d.Value.Lexema == "else")
            {
                CrearApuntador(p, "B");

                //En BNF del IF saltará a BX
                string apuntador = "B" + PunteroCount.ToString();
                tempBNF.Lexema += apuntador;

                //Se añade nombre del apuntador al BNF del IF
                BNF.Value = tempBNF;

                //Añadir apuntador a la lista
                Pointers.Add(apuntador);

                p = p.Next; //Apunta a ELSE
                if (p.Next.Value.Lexema == "if") //Si es un else if
                {
                    p = p.Next;
                    p = IfPolish(p);
                }
                else //es ELSE solito
                {
                    p = p.Next;
                    p = p.Next; //Entra a primera cosa después del {
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
                }
            }
            else //Es una sentencia
            {
                CrearApuntador(p, "D");
                string apuntador = "D" + PunteroCount.ToString();
                tempBRI.Lexema += apuntador;
                BRI.Value = tempBNF;
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
            foreach (Tokens item in res.Reverse()) 
            {
                LinkedPolish.AddLast(item.Lexema);
                currentpolish += item.Lexema + " "; 
            }
            return currentpolish;
        }

        public void CrearApuntador(LinkedListNode<Tokens> p, string ID)
        {
            LinkedListNode<Tokens> apuntador = p;
            apuntador.Value = new Tokens(string.Empty, ID + PunteroCount.ToString(), 0, p.Value.Linea);
            Pointers.Add(apuntador.Value.Lexema);
        }
    }
}
