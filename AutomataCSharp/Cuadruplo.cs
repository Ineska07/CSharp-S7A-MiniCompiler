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
        public int COUNT
        {
            get { return counter; }
            set {counter = value; }
        }

        public string AP
        {
            get { return apuntador; }
            set { apuntador = value; }
        }

        public string OP
        {
            get { return operador; }
            set { operador = value; }
        }

        public string OP1
        {
            get { return operando1; }
            set { operando1 = value; }
        }

        public string OP2
        {
            get { return operando2; }
            set { operando2 = value; }
        }

        public string RES
        {
            get { return resultado; }
            set 
            {
                if (value == null)
                {
                    this.resultado = "T" + counter.ToString();
                    this.variabletemporal = this.resultado;
                }
                else
                {
                    this.resultado = value;
                }
            }
        }

        public Cuadruplo(string ap, string op, string op1, string op2, string res)
        {
            this.apuntador = ap;
            this.operador = op;
            this.operando1 = op1;
            this.operando2 = op2;
            this.resultado = res;
        }

    }
}
