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
        public LinkedList<Cuadruplo> TablaCuadruplos = new LinkedList<Cuadruplo>();

        int tempvarcount = 0;

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
                Cuadruplo c = new Cuadruplo(null, null, null, null, null);

                if (item.StartsWith("BRI")) //BRI
                {
                    string apunta = item.Split('>').Last();

                    c.AP = apuntador;
                    c.OP = "BRI";
                    c.RES = apunta;
                }
                else if (item.StartsWith("BRF")) //BRF
                {
                    string apunta = item.Split('>').Last();
                    string operando = vars.Pop();

                    c.AP = apuntador;
                    c.OP = "BRF";
                    c.OP1 = operando;
                    c.RES = apunta;
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
                        continue;
                    }
                    else
                    {
                        //Tomar operador
                        string operador = item;

                        
                        if (operador == "WriteLine")
                        {
                            string Var1 = vars.Pop();
                            c.OP = operador;
                            c.OP1 = Var1;
                        }
                        else if (operador == "=")
                        {
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

                            c.AP = apuntador;
                            c.OP = operador;
                            c.OP1 = Var1;
                            c.OP2 = Var2;
                            c.RES = Var1;
                        }
                        else
                        {
                            //Toma los elementros de la pila
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

                            //Hacer la linea del cuadruplo
                            c.AP = apuntador;
                            c.OP = operador;
                            c.OP1 = Var1;
                            c.OP2 = Var2;
                            c.COUNT = tempvarcount; tempvarcount++;
                            c.RES = null;
                        }

                        //Meter la variable TX a la pila de variables
                        string newVar = c.variabletemporal;
                        vars.Push(newVar);
                    }
                }
                TablaCuadruplos.AddLast(c);
                apuntador = string.Empty;
            }

            int restantes = Int32.Parse(apuntador);
            while (restantes > 0)
            {
                Cuadruplo c = new Cuadruplo(restantes.ToString(), null, null, null, null);
                TablaCuadruplos.AddLast(c);
                restantes--;
            }
        }

        public void CrearEnsamblador()
        {

        }
    }
}
