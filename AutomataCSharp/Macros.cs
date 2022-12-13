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

        public void Stack(LinkedList<Variable> VS, LinkedList<Variable> V)
        {
            /* .MODEL SMALL
                STACK 100h
                .DATA */

            this.TextoMacro = "\n.MODEL SMALL\nSTACK 100h\n.DATA\n";

            foreach (Variable String in VS)
            {
                V.AddLast(String);
            }

            foreach (Variable var in V)
            {
                string Line;
                switch (var.Type)
                {
                    case "int":
                        Line = var.Name + " DW '0'";
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

        public void SumaResta(Cuadruplo C, LinkedList<Variable> V)
        {
            string variable = C.OP1;
            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            string Line;
            string operador = string.Empty;
            switch (C.OP)
            {
                case "+": operador = "ADD"; break;
                case "-": operador = "SUB"; break;
            }

            Line =  codetabs +  "PUSH AX\n";
            Line += codetabs +  "MOV AX, " + variable +"\n";
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

        public void Relacional(Cuadruplo C, Cuadruplo BNF)
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
            Line = codetabs + "LOCAL A" + BNF.RES +"\n";
            Line += codetabs + "LOCAL SALIR\n";
            Line += codetabs + "PUSH AX\n";
            Line += codetabs + "MOV AX, " + C.OP1 + "\n";
            Line += codetabs + "CMP AX, " + C.OP2 + "\n";
            Line += codetabs + operador + " A" + BNF.RES +"\n";
            Line += codetabs + "MOV " + C.RES + ", 1\n\n";

            Line += codetabs + "JMP " + BNF.RES + ":\n";
            Line += codetabs + "\tMOV " + C.RES + ", 1\n";
            Line += codetabs + "SALIR:\n";
            Line += codetabs + "\tPOP AX\n";

            this.Macro = Line;
        }

        public void Salto(Cuadruplo C)
        {
            /*MOV AL,VALOR1
   	        CMP AL,1
   	        JE  DESTINO*/

            string Line;
            Line = codetabs + "MOV AL " + C.OP1 + "\n";

            this.Macro = Line;
        }

        public void Asignacion(Cuadruplo C, LinkedList<Variable> V)
        {
            string variable = C.OP1;

            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            string Line;
            Line =  codetabs + "PUSH AX\n";
            Line += codetabs + "MOV AX, " + variable + "\n";
            Line += codetabs + "MOV "+ C.RES + ", AX\n";
            Line += codetabs + "OP AX\n";

            this.Macro = Line;
        }
        public void WriteLine(Cuadruplo C, LinkedList<Variable> V)
        {
            /*
            MOV AH, 09H
		    LEA DL, MESSAGE
		    INT 21H
             */

            string variable = C.OP1;
            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            string Line;
            Line = codetabs + "MOV AH, 09H\n";
            Line += codetabs + "LEA DL, " + variable + "\n";
            Line += codetabs + "INT 21H\n";

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

            string Line;
            Line =  codetabs + "LEA DX, " + C.OP1 + "\n";
            Line += codetabs + "MOV AH, 0AH\n";
            Line += codetabs + "INT 21H\n";
            Line += codetabs + "MOV BH, 0\n";
            Line += codetabs + "MOV BL, " + C.OP1 + "[1]\n";
            Line += codetabs + "MOV AL, '$'\n";
            Line += codetabs + "MOV  " + C.OP1 + "[2+BX], AL\n";

            this.Macro = Line;
        }

       

        public void ASCIIDecimal()
        {

        }
    }

    /*
     ASCTODEC	    	MACRO NUM,CADNUM
        LOCAL D1
        LOCAL D2
        LOCAL D3
        LOCAL D4
        LOCAL D5
                PUSH CX
                MOV NEGATIVO,0
                MOV NUM,0
                MOV COUNT,0
                MOV BX,0
                LEA SI,CADNUM
                MOV AL,[SI]
                MOV CX,SI
                CMP AL,45
                JNE D1
               	INC CX
                INC SI
                MOV NEGATIVO,1
        D1:
                MOV DL,48
                CMP [SI],DL
                JB D2
                MOV DL,57
                CMP [SI],DL
                JA D2
                INC BX
                INC SI
                JMP D1
        D2:
                DEC BX
                MOV SI,CX
                MOV CX,1
        D3:
                MOV AL,[BX+SI]
                XOR AL,30h
                MOV AH,0
                MUL CX
                ADD NUM,AX
                CMP BX,0
                JE D4
                DEC BX
                MOV AX,CX
                MUL BUF
                MOV CX,AX
                JMP D3
        D4:
                CMP NEGATIVO,0
                JE D5
                NOT NUM
                INC NUM
        D5:
                POP CX
    ENDM
     */
}
