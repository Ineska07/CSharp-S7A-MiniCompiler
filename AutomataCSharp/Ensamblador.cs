using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Ensamblador
    {
        private Dictionary<string, int> Operadores = new Dictionary<string, int>()
        {
            //Otros
            {"WriteLine", 3}, {"ReadLine", 3}, 

            //Lógicos
            {"&&", 2},  {"||", 2},

            //Relacionales
            {"==", 2}, {"!=", 2}, {"<=", 2}, {"<", 2}, {">", 2}, {">=", 2},

            //Aritméticos
            {"/", 1 }, {"*", 1}, {"-", 1}, {"+", 1},

            //igualdad
            {"=", 0},
        };
        private LinkedList<Cuadruplo> TablaCuadruplos;

        public Ensamblador(LinkedList<string> Polish, LinkedList<Variable> Variables)
        {
            Cuadruplos(Polish);
        }
        private void Cuadruplos(LinkedList<string> Polish)
        {
            Stack<string> vars = new Stack<string>();
            string apuntador = string.Empty;

            foreach (string item in Polish)
            {
                Cuadruplo c = null;
                if (item.StartsWith("BRI")) //BRI
                {
                    string apunta = item.Split('>').Last();
                    c = new Cuadruplo(apuntador, "BRI", null, null, apunta);
                }
                else if (item.StartsWith("BRF")) //BRF
                {
                    string apunta = item.Split('>').Last();
                    string operando = vars.Pop();

                    c = new Cuadruplo(apuntador, "BRF", operando, null, apunta);
                }
                else if (item.Length > 1 && item.StartsWith(">")) //APUNTADOR
                {
                    apuntador = item.Split('>').Last();
                    continue;
                }
                else //EVALUACIÓN DEL POSFIJO
                {
                    if (!Operadores.ContainsKey(item))
                    {
                        vars.Push(item);
                    }
                    else
                    {
                        //Tomar operador
                        string operador = item;

                        //Toma los elementros de la pila
                        string Var2 = vars.Pop();
                        string Var1 = vars.Pop();

                        //Hacer la linea del cuadruplo
                        #region igualdad
                        if (operador == "=")
                        {
                            c = new Cuadruplo(apuntador, operador, Var1, Var2, Var1);
                        }
                        else
                        {
                            c = new Cuadruplo(apuntador, operador, Var1, Var2, null);
                        }
                        #endregion

                        //Meter la variable TX a la pila de variables
                        string newVar = c.variabletemporal;
                        vars.Push(newVar);
                    }
                }
                TablaCuadruplos.AddLast(c);
                apuntador = string.Empty;
            }

            if (vars.Count == 1)
            {
                string newVar = vars.Peek();
                vars.Push(newVar);
            }
        }

        public void CrearEnsamblador()
        {

        }
    }
}
