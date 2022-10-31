using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    //Generador de Código intermedio
    class Intermedio
    {
        private Dictionary<string, int> Operandos = new Dictionary<string, int>();
        LinkedList<Variable> ListaVariables = new LinkedList<Variable>();

        public Intermedio(LinkedList<Variable> variableList)
        {
            ListaVariables = variableList;
        }

        public void InfixPosfix(Tokens[] infix)
        {
            Dictionary<int, int> Prioridad = new Dictionary<int, int>() { { -6, 1 }, { -7, 2 }, { -8, 3 }, { -9, 4 } };
            Stack<Tokens> res = new Stack<Tokens>();
            Stack<Tokens> aux = new Stack<Tokens>();

            for (int i = 0; i < infix.Length; i++)
            {
                if (Operandos.ContainsKey(infix[i].Lexema))
                {
                    if (aux.Count > 0)
                    {
                        int prioridadactual = Prioridad[infix[i].Valor];
                        int prioridadtope = Prioridad[aux.Peek().Valor];

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
