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
        private string aux;
        string codetabs = "\t\t";

        public string Macro
        {
            get { return TextoMacro; }
            set { TextoMacro = value; }
        }
        public string MacroAuxiliar
        {
            get { return aux; }
            set { aux = value; }
        }

        public void Stack(LinkedList<Variable> VS, LinkedList<Variable> V)
        {
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
                        Line = var.Name + " DB, 0";
                        this.TextoMacro += codetabs + Line + '\n';
                        break;
                    case "double":
                        Line = var.Name + " DB, 0";
                        this.TextoMacro += codetabs + Line + '\n';
                        break;
                    case "string":
                        Line = var.Name + " DB " + var.Value + ",'$'"; //cadena db 'Hola Mundo','$'
                        this.TextoMacro += codetabs + Line + '\n';
                        break;
                    case "bool":
                        break;
                }
            }
            this.TextoMacro += ".CODE\n.386";
        }

        public void Start()
        {
            string Line;
            Line =  "BEGIN: \n";
            Line += codetabs + "MOV AX, @DATA\n";
            Line += codetabs + "MOV DS, AX\n";

            codetabs += "\t";
            this.Macro = Line;
        }

        public void End()
        {
            string Line;
            Line  = codetabs + "MOV AX, 4C00H\n";
            Line += codetabs + "INT 21H\n";
            Line += "END BEGIN\n";
            Line += "END\n";

            this.Macro = Line;
        }

        public void Asignacion(Cuadruplo C, LinkedList<Variable> V)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "\tSALTO" + C.AP + ":\n";
            }

            string variable = C.OP1;

            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            Line += codetabs + "MOV AL, " + variable + "\n";
            Line += codetabs + "MOV " + C.RES + ", AL\n";

            this.Macro = Line;
        }

        public void SumaResta(Cuadruplo C, LinkedList<Variable> V)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "\tSALTO" + C.AP + ":\n";
            }

            string variable = C.OP1;
            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            string operador = string.Empty;
            switch (C.OP)
            {
                case "+": operador = "ADD"; break;
                case "-": operador = "SUB"; break;
            }

            Line +=  codetabs +  "MOV AL, " + variable +"\n";
            Line += codetabs +  operador + " AL, " + C.OP2 + "\n";
            Line += codetabs +  "MOV " + C.RES + ", AL\n";

            this.Macro = Line;
        }

        public void MultDiv(Cuadruplo C)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "SALTO\t" + C.AP + ":\n";
            }

            string operador = string.Empty;
            switch (C.OP)
            {
                case "*": operador = "MUL"; break;
                case "/": operador = "DIV"; break;
            }

            if(C.OP == "/")
                Line = codetabs + "MOV DX, 0\n";
            Line +=  codetabs + "MOV AL, " + C.OP1 + "\n";
            Line += codetabs + "MOV BL, " + C.OP2 + "\n";
            Line += codetabs + "I"+ operador +" BL\n";
            Line += codetabs + "MOV " + C.RES + ", AL\n";

            this.Macro = Line;
        }

        public void Relacional(Cuadruplo C, Cuadruplo BRF)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "\tSALTO" + C.AP + ":\n";
            }

            string operador = string.Empty;
            switch (C.OP)
            {
                case "<": operador = "JL"; break;   //Lower
                case "<=": operador = "JLE"; break; //Lower-Equal
                case ">": operador = "JG"; break;   //Greater
                case ">=": operador = "JGE"; break; //Greater-Equal
                case "==": operador = "JE"; break;  //Equal
                case "!=": operador = "JNE"; break; //Not-Equal
            }
            
            Line += codetabs + "MOV AL " + C.OP1 + "\n";
            Line += codetabs + "CMP " + C.OP2 + ", AL\n";
            Line += codetabs + operador + " SALTO" + BRF.RES + "\n";

            this.Macro = Line;
        }

        public void WriteLine(Cuadruplo C, LinkedList<Variable> V)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "\tSALTO" + C.AP + ":\n";
            }

            string variable = C.OP1;
            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            Line += codetabs + "MOV AH, 09H\n";
            Line += codetabs + "LEA DL, " + variable + "\n";
            Line += codetabs + "INT 21H\n\n";

            //Salto de Línea
            Line += codetabs + "WRITELN\n";

            this.Macro = Line;
        }

        public void ReadLine(Cuadruplo C, LinkedList<Variable> V)
        {
            string Line = string.Empty;
            if (!C.AP.Equals(""))
            {
                Line = "\tSALTO" + C.AP + ":\n";
            }

            string variable = C.OP1;
            foreach (Variable String in V)
            {
                if (String.Value == variable)
                {
                    variable = String.Name;
                    break;
                }
            }

            /*
             ;esto lee
                mov ah, 1
                int 21h
                mov DESTINO, al
            */

            Line += codetabs + "MOV AH, 1\n";
            Line += codetabs + "INT 21H\n";
            Line += codetabs + "MOV " + C.RES + ", AL\n";

            //Salto de Línea
            Line += codetabs + "WRITELN\n";

            this.Macro = Line;
        }

        public void BRI(Cuadruplo C)
        {
            this.Macro = codetabs + "JMP SALTO" + C.RES + "\n";
        }

        public void ASCIIDecimal()
        {

        }
    }
}
