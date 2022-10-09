using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Tokens
    {
        protected string tipo;
        protected string lexema;
        protected int valor;
        protected int linea;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Lexema
        {
            get { return lexema; }
            set { lexema = value; }
        }

        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public int Linea
        {
            get { return linea; }
            set { linea = value; }
        }

        public Tokens(string tipo, string lexema, int valor, int linea)
        {
            this.tipo = tipo;
            this.lexema = lexema;
            this.valor = valor;
            this.linea = linea;
        }

    }
}
