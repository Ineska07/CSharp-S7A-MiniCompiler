using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Macros
    {
        private string TextoMacro;
        string codetabs = "\t\t";

        public string Macro
        {
            get { return TextoMacro; }
            set { TextoMacro = value; }
        }

        public void Stack(LinkedList<Variable> V)
        {
            /* .MODEL SMALL
                STACK 100h
                .DATA */

            this.TextoMacro = "\n.MODEL SMALL\nSTACK 100h\n.DATA\n";

            foreach (Variable var in V)
            {
                string Line;
                switch (var.Type)
                {
                    case "int":
                        Line = "DB '" + var.Name + "'";
                        this.TextoMacro += codetabs + Line + '\n';
                        break;
                    case "double":
                        break;
                    case "string":
                        Line = var.Name + " DB " + var.Value + ",'$'"; //cadena db 'Hola Mundo','$'
                        this.TextoMacro += codetabs + Line + '\n';
                        break;
                    case "bool":
                        //No hace nada xd
                        break;
                }
            }
            //Final del stack
            this.TextoMacro += ".CODE\n.386\n";

            /*  .CODE
                .386 */
        }

        public void Start()
        {

        }
        public void WriteLine(Cuadruplo C)
        {
            /*
            MOV AH, 2
		    MOV DL, 0AH
		    INT 21H

		    MOV AH, 2
		    MOV DL, 0DH
		    INT 21H
             */
        }
        public void ReadLine(Cuadruplo C)
        {

        }
    }
}
