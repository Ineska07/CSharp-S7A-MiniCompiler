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
        protected int line;

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
        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        public Variable(string tipo, string name, string valor, int line)
        {
            this.type = tipo;
            this.name = name;
            this.val = valor;
            this.line = line;
        }
    }
}
