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
        protected int token;
        protected int line;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Token
        {
            get { return token; }
            set { token = value; }
        }

        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        public Variable(string tipo, int valor, int line)
        {
            this.type = tipo;
            this.token = valor;
            this.line = line;
        }
    }
}
