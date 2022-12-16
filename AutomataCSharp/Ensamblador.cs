using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Ensamblador
    {
        private Dictionary<string, int> Operadores = new Dictionary<string, int>()
        {
            //Otros
            {"WriteLine", 3},{"ReadLine", 3},

            //Lógicos
            {"&&", 2},  {"||", 2},

            //Relacionales
            {"==", 2}, {"!=", 2}, {"<=", 2}, {"<", 2}, {">", 2}, {">=", 2},

            //Aritméticos
            {"/", 1 }, {"*", 1}, {"-", 1}, {"+", 1},

            //igualdad
            {"=", 0},
        };
        public LinkedList<Cuadruplo> TablaCuadruplos = new LinkedList<Cuadruplo>();
        public LinkedList<Variable> Variables = new LinkedList<Variable>();
        public LinkedList<Variable> VariablesExtra = new LinkedList<Variable>();

        Types SistemaTipos = new Types();

        int tempvarcount = 0;

        public Ensamblador(LinkedList<string> Polish, LinkedList<Variable> VariablesDeclaradas)
        {
            Cuadruplos(Polish, VariablesDeclaradas);

            foreach (Variable var in VariablesDeclaradas)
            {
                Variables.AddFirst(var);
            }

            CrearEnsamblador();
        }

        #region Cuadruplos
        private void Cuadruplos(LinkedList<string> Polish, LinkedList<Variable> VariablesDeclaradas)
        {
            Stack<string> vars = new Stack<string>();
            string apuntador = string.Empty;
            int msgcount = 0;
            int intcount = 0;

            foreach (string item in Polish)
            {
                Cuadruplo c = new Cuadruplo(apuntador, null, null, null, null);

                if (item.StartsWith("BRI")) //BRI
                {
                    string apunta = item.Split('>').Last();

                    c.AP = apuntador;
                    c.OP = "BRI";
                    c.RES = apunta;
                }
                else if (item.StartsWith("BRF")) //BRF
                {
                    string apunta = item.Split('>').Last();
                    string operando = vars.Pop();

                    c.AP = apuntador;
                    c.OP = "BRF";
                    c.OP1 = operando;
                    c.RES = apunta;
                }
                else if (item.Length > 1 && item.StartsWith(">")) //APUNTADOR
                {
                    apuntador = item.Split('>').Last();
                    continue;
                }
                else //EVALUACIÓN DEL POSFIJO
                {
                    if (!Operadores.ContainsKey(item))
                    {
                        vars.Push(item);

                        if (item.StartsWith(('"'.ToString())))
                        {
                            Variable var = new Variable("string", "Message" + msgcount.ToString(), item);
                            msgcount++;
                            VariablesExtra.AddLast(var);
                        }
                        continue;
                    }
                    else
                    {
                        //Tomar operador
                        string operador = item;

                        if (operador == "WriteLine")
                        {
                            string Var1 = vars.Pop();
                            c.OP = operador;
                            c.OP1 = Var1;
                        }
                        else if (operador == "ReadLine")
                        {
                            string Var1 = vars.Pop();
                            c.OP = operador;
                            c.OP1 = Var1;
                            c.RES = Var1;
                        }
                        else if (operador == "=")
                        {
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

                            c.OP = operador;
                            c.OP1 = Var2;
                            c.RES = Var1;
                        }
                        else
                        {
                            //Toma los elementros de la pila
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

                            if (operador == "<=" || operador == "<" || operador == ">" || operador == ">=" || operador == "==" || operador == "!=")
                            {
                                string VarType2 = GetVarType(Var2, VariablesDeclaradas);
                                Variable var = new Variable(VarType2, "VAR"+intcount.ToString() , Var2); intcount++;
                                VariablesExtra.AddLast(var);
                            }

                            //Hacer la linea del cuadruplo
                            c.AP = apuntador;
                            c.OP = operador;
                            c.OP1 = Var1;
                            c.OP2 = Var2;
                            c.COUNT = tempvarcount; tempvarcount++;
                            c.RES = null;
                        }

                        //Meter la variable TX a la pila de variables
                        string newVar = c.variabletemporal;

                        if(newVar != null)
                        {
                            string newVarType = SetVariableType(c, VariablesDeclaradas);
                            Variable var = new Variable(newVarType, c.variabletemporal, null);
                            Variables.AddLast(var);
                        }
                        vars.Push(newVar);
                    }
                }
                TablaCuadruplos.AddLast(c);
                apuntador = string.Empty;
            }

            if (apuntador != string.Empty)
            {
                int restantes = Int32.Parse(apuntador);
                while (Int32.Parse(apuntador) > 0)
                {
                    Cuadruplo c = new Cuadruplo(restantes.ToString(), null, null, null, null);
                    TablaCuadruplos.AddLast(c);
                    restantes--;
                }
            }
        }

        #region TipoVariableTemporal
        private string SetVariableType(Cuadruplo cu, LinkedList<Variable> VariablesDeclaradas)
        {
            string type = string.Empty;
            string VarOP1 = string.Empty;
            string VarOP2 = string.Empty;

            if (cu.OP1 != null) VarOP1 = GetVarType(cu.OP1, VariablesDeclaradas);
            if (cu.OP2 != null) VarOP2 = GetVarType(cu.OP2, VariablesDeclaradas);

            if (VarOP1 != string.Empty || VarOP2 != string.Empty)
            {
                SistemaTipos.EvaluarTipos(cu.OP, VarOP1, VarOP2);
                type = SistemaTipos.Tipo;
            }
            else if (VarOP1 == string.Empty) type = VarOP2;
            else if (VarOP2 == string.Empty) type = VarOP1;

            return type;
        }
        private string GetVarType(string op, LinkedList<Variable> VariablesDeclaradas)
        {
            string OPtype = string.Empty;

            if(op == "WriteLine" || op == "ReadLine")
            {
                OPtype = "string";
                return OPtype;
            }

            Lexico A = new Lexico();
            A.AnalisisLexico(op + " ");
            switch (A.tokensGenerados.Peek().Valor)
            {
                case -1:
                    foreach (Variable var in VariablesDeclaradas)
                    {
                        if (var.Name == op)
                        {
                            OPtype = var.Type;
                            break;
                        }
                    }
                    break;
                case -2:
                    OPtype = "int";
                    break;
                case -3:
                    OPtype = "double";
                    break;
                case -4:
                    OPtype = "string";
                    break;
                case -5:
                    OPtype = "string";
                    break;
            }
            return OPtype;
        }
        #endregion

        #endregion

        public void CrearEnsamblador()
        {
            Macros MAC = new Macros();

            string filepath = @"D:\Compilador_V02\asm\prueba.asm";

            using (StreamWriter tw = new StreamWriter(filepath, false)) 
            {
                tw.WriteLine("COMMENT ! \tPROYECTO LEYAU2\n" +
                    "\tS7A BIEBRICH CONTRERAS MARÍA INÉS\n" +
                    "\tLENGUAJE C# - 16/12/2022\n" +
                    "\t!");

                //Hacer Pila de Variables
                MAC.Stack(VariablesExtra, Variables);

                //Insertar MACRO en el archivo de texto
                tw.Write(MAC.Macro + "\n");
                MAC.Macro = string.Empty;

                //Insertar Inicio del código
                MAC.Start();
                tw.Write(MAC.Macro + "\n");
                MAC.Macro = string.Empty;

                int Currentindex = 0;
                Cuadruplo next = new Cuadruplo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (Cuadruplo C in TablaCuadruplos)
                {
                    if (Currentindex < TablaCuadruplos.Count - 1)
                    {
                       next = TablaCuadruplos.ElementAt(Currentindex + 1);
                    }

                    switch (C.OP)
                    {
                        //Asignación
                        case "=":
                            MAC.Asignacion(C, VariablesExtra);
                            break;
                        //Aritméticos
                        case "+":
                            MAC.SumaResta(C, VariablesExtra);
                            break;
                        case "-":
                            MAC.SumaResta(C, VariablesExtra);
                            break;
                        case "*":
                            MAC.MultDiv(C);
                            break;
                        case "/":
                            MAC.MultDiv(C);
                            break;
                        //Relacionales
                        case "<":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        case "<=":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        case ">":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        case ">=":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        case "==":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        case "!=":
                            MAC.Relacional(C, next, VariablesExtra);
                            break;
                        //Consola
                        case "WriteLine":
                            MAC.WriteLine(C, VariablesExtra);
                            break;
                        case "ReadLine":
                            MAC.ReadLine(C, VariablesExtra);
                            break;
                        //Salto
                        case "BRI":
                            MAC.BRI(C);
                            break;
                        default: 
                            break;
                    }

                    tw.Write(MAC.Macro + "\n");
                    MAC.Macro = string.Empty;
                    Currentindex++;
                }

                MAC.End();
                tw.Write(MAC.Macro + "\n");
                MAC.Macro = string.Empty;

                tw.Close();
            }
        }
    }
}
