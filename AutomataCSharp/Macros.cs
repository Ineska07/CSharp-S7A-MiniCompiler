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
                        Line = "REAL4 '" + var.Name + "'";
                        this.TextoMacro += codetabs + Line + '\n';
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
            this.TextoMacro += ".CODE\n.386";
        }

        public void Start()
        {
            string Line;
            Line =  "BEGIN: \n";
            Line += codetabs + "MOV AX, @DATA\n";
            Line += codetabs + "MOV DS, AX\n";
            Line += codetabs + "CALL COMPILADOR\n";
            Line += codetabs + "MOV AX, 4C00H\n";
            Line += codetabs + "INT 21H\n";
            Line += codetabs + "COMPILADOR PROC\n";

            codetabs += "\t";
            this.Macro = Line;
        }

        public void End()
        {
            codetabs = "\t\t";

            string Line;
            Line =  "COMPILADOR END\n";
            Line += "END BEGIN\n";

            this.Macro = Line;
        }

        public void SumaResta(Cuadruplo C)
        {
            string Line;
            string operador = string.Empty;
            switch (C.OP)
            {
                case "+": operador = "ADD"; break;
                case "-": operador = "SUB"; break;
            }

            Line =  codetabs +  "PUSH AX\n";
            Line += codetabs +  "MOV AX, " + C.OP1 +"\n";
            Line += codetabs +  operador + " " + C.OP2 + "\n";
            Line += codetabs +  "MOV " + C.RES + ", AX\n";
            Line += codetabs +  "POP AX\n";

            this.Macro = Line;
        }

        public void MultDiv(Cuadruplo C)
        {
            string operador = string.Empty;
            switch (C.OP)
            {
                case "*": operador = "MUL"; break;
                case "/": operador = "DIV"; break;
            }

            string Line;
            Line =  codetabs + "PUSH AX\n";
            Line += codetabs + "PUSH BX\n";
            Line += codetabs + "MOV AX, " + C.OP1 + "\n";
            Line += codetabs + "MOV BX, " + C.OP2 + "\n";
            Line += codetabs + "I"+ operador +" BX\n";
            Line += codetabs + "MOV " + C.RES + ", AX\n";
            Line += codetabs + "POP BX\n";
            Line += codetabs + "POP AX\n";

            this.Macro = Line;
        }

        public void Relacional(Cuadruplo C)
        {
            string operador = string.Empty;
            switch (C.OP)
            {
                case "<": operador = "JGE"; break;
                case "<=": operador = "JG"; break;
                case ">": operador = "JLE"; break;
                case ">=": operador = "JL"; break;
                case "==": operador = "JNE"; break;
                case "!=": operador = "JE"; break;
            }

            string Line;
            Line = codetabs + "LOCAL LABEL1\n";
            Line += codetabs + "LOCAL SALIR\n";
            Line += codetabs + "PUSH AX\n";
            Line += codetabs + "MOV AX, " + C.OP1 + "\n";
            Line += codetabs + "CMP AX, " + C.OP2 + "\n";
            Line += codetabs + operador + " LABEL1\n";
            Line += codetabs + "MOV " + C.RES + ", 1\n\n";

            Line += codetabs + "JMP SALIR\n";
            Line += codetabs + "LABEL1:\n";
            Line += codetabs + "\tMOV " + C.RES + ", 1\n";
            Line += codetabs + "SALIR:\n";
            Line += codetabs + "\tPOP AX\n";

            this.Macro = Line;
        }

        public void Asignacion(Cuadruplo C)
        {
            string Line;
            Line =  codetabs + "PUSH AX\n";
            Line += codetabs + "MOV AX, " + C.OP1 + "\n";
            Line += codetabs + "MOV "+ C.RES + ", AX\n";
            Line += codetabs + "OP AX\n";

            this.Macro = Line;
        }
        public void WriteLine(Cuadruplo C)
        {
            /*
            MOV AH, 09H
		    LEA DL, MESSAGE
		    INT 21H
             */

            string Line;
            Line = codetabs + "MOV AH, 09H\n";
            Line = codetabs + "LEA DL, " + C.OP1 + "\n";
            Line = codetabs + "INT 21H\n";

            this.Macro = Line;
        }
        public void ReadLine(Cuadruplo C)
        {
            /*
             LEA DX,VA@LOR
             MOV AH,0AH
             INT 21H
             MOV BH,0
             MOV BL,VA@LOR[1]
             MOV AL,"$"
             MOV VA@LOR[2+BX],AL 
             */
        }

        public void Salto(Cuadruplo C)
        {
            /*MOV AL,VALOR1
   	        CMP AL,1
   	        JE  DESTINO*/

            string Line;
            Line = codetabs + "MOV AL " + C.OP1 +"\n";
            Line = codetabs + "CMP AL, 1\n";
            Line = codetabs + "JE " + C.RES + "\n";

            this.Macro = Line;
        }
    }
}
