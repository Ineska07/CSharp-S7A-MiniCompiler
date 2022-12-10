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
            {"=", 16 },  {"WriteLine", 15}, {"ReadLine", 14 }, {"&&", 13 },  {"||", 12},
            {"==", 11 }, {"!=", 10 }, {"<=", 9 }, {"<", 8 }, {">", 7 }, {">=", 5},
            {"/", 4 }, {"*", 3 }, {"-", 2 }, {"+", 1 },
        };

        private LinkedList<Cuadruplo> TablaCuadruplos;

        public Ensamblador(LinkedList<string> Polish, LinkedList<Variable> Variables)
        {
            Cuadruplos(Polish);
        }
        #region PilaVariables

        private void Cuadruplos(LinkedList<string> Polish)
        {
            //Algoritmo similar a la evaluación del Polish

            //Recorrer el Polish - solo tiene operandos y operadores
            foreach(string item in Polish)
            {
                if (Operadores.ContainsKey(item))
                {

                }
            }
        }

        #endregion

        public void CrearEnsamblador()
        {

        }
    }
}
