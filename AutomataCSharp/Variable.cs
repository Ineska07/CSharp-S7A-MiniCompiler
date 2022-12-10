using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Variable
    {
        protected string type;
        protected string name;
        protected string val;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name;}
            set { name = value; }
        }
        public string Value
        {
            get { return val; }
            set { val = value; }
        }

        public Variable(string tipo, string name, string valor)
        {
            this.type = tipo;
            this.name = name;
            this.val = valor;
        }
    }
    class Cuadruplo
    {
        private string operador;
        private string operando1;
        private string operando2;
        private string resultado;

        private int counter;

        public Cuadruplo(string op, string op1, string op2, string res)
        {
            this.operador = op;
            this.operando1 = op1;
            this.operando2 = op2;
            
            if (res == null)
            {
                this.resultado = "T" + counter.ToString();
                counter++;
            }
            else
            {
                this.resultado = res;
            }
        }

    }
}
