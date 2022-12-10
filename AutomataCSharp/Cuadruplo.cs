using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Cuadruplo
    {
        private string apuntador;
        private string operador;
        private string operando1;
        private string operando2;
        private string resultado;

        public string variabletemporal;

        private int counter;

        public Cuadruplo(string ap, string op, string op1, string op2, string res)
        {
            this.apuntador = ap;
            this.operador = op;
            this.operando1 = op1;
            this.operando2 = op2;

            if (res == null)
            {
                this.resultado = "T" + counter.ToString();
                this.variabletemporal = this.resultado;
                counter++;
            }
            else
            {
                this.resultado = res;
            }
        }

    }
}
