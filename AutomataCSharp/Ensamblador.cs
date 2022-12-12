﻿using System;
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
            {"WriteLine", 3}, {"ReadLine", 3}, 

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

        Types SistemaTipos = new Types();

        int tempvarcount = 0;

        public Ensamblador(LinkedList<string> Polish, LinkedList<Variable> VariablesDeclaradas)
        {
            //Poner valor a las variables ya declaradas

            Cuadruplos(Polish, VariablesDeclaradas);

            foreach (Variable var in VariablesDeclaradas)
            {
                Variables.AddFirst(var);
            }

            foreach(Cuadruplo C in TablaCuadruplos)
            {
                if(C.OP == "=")
                {
                    foreach (Variable var in VariablesDeclaradas)
                    {
                        if (var.Name == C.OP1) var.Value = C.OP2;
                    }
                }
            }

            CrearEnsamblador();
        }

        #region Cuadruplos
        private void Cuadruplos(LinkedList<string> Polish, LinkedList<Variable> VariablesDeclaradas)
        {
            Stack<string> vars = new Stack<string>();
            string apuntador = string.Empty;

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
                        else if (operador == "=")
                        {
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

                            c.OP = operador;
                            c.OP1 = Var1;
                            c.OP2 = Var2;
                            c.RES = Var1;
                        }
                        else
                        {
                            //Toma los elementros de la pila
                            string Var2 = vars.Pop();
                            string Var1 = vars.Pop();

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
                            Variable var = new Variable(newVarType, c.variabletemporal, c.OP1);
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

            //Crear archivo .txt par escribir la traduccion
            string filepath = @"D:\Compilador_V02\PROYECTO.txt";

            using (StreamWriter tw = new StreamWriter(filepath, false)) 
            {
                tw.WriteLine("; Bienvenido al Proyecto\n");

                //Hacer Pila de Variables
                MAC.Stack(Variables);

                //Insertar MACRO en el archivo de texto
                tw.Write(MAC.Macro + "\n");
                MAC.Macro = string.Empty;

                //Insertar Inicio del código
                tw.Write(MAC.Macro + "\n");

                tw.Close();

                foreach (Cuadruplo C in TablaCuadruplos)
                {

                }
            }
        }
    }
}
