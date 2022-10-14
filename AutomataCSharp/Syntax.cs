using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutomataCSharp
{
    class Syntax : Lexico
    {
        #region Inicio
        private Types sistematipos = new Types();
        private LinkedList<Tokens> TokenList = new LinkedList<Tokens>();
        private LinkedList<Variable> variableList = new LinkedList<Variable>();

        private int currentline = 0;
        private string Posfijo = string.Empty;

        private Dictionary<int, string> vartype = new Dictionary<int, string>();
        private Dictionary<int, string> accesstype = new Dictionary<int, string>();
        private Dictionary<int, string> arisymbol = new Dictionary<int, string>();
        private Dictionary<int, string> assignsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> logicsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> relsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> boolval = new Dictionary<int, string>();
        private Dictionary<int, string> valuetypes = new Dictionary<int, string>();

        //ERRORES
        public List<string> ErrorS = new List<string>(); //Lista de Errores
        public bool semError = false;
        private bool hasSyntaxErrors = false;

        public Syntax()
        {
            valuetypes.Add(-1, "ID");
            valuetypes.Add(-2, "Number");
            valuetypes.Add(-3, "Decimal");
            valuetypes.Add(-4, "String");
            valuetypes.Add(-5, "Char");

            //Aritmético
            arisymbol.Add(-6, "+");
            arisymbol.Add(-7, "-");
            arisymbol.Add(-8, "*");
            arisymbol.Add(-9, "/");

            //Asignativos
            assignsymbol.Add(-15, "++");
            assignsymbol.Add(-16, "--");
            assignsymbol.Add(-19, "=");

            //Relacional
            relsymbol.Add(-17, "<");
            relsymbol.Add(-18, ">");
            relsymbol.Add(-20, ">=");
            relsymbol.Add(-21, "<=");
            relsymbol.Add(-22, "==");
            relsymbol.Add(-24, "!=");

            //Logico
            logicsymbol.Add(-26, "&&");
            logicsymbol.Add(-28, "||");

            //Alcance
            accesstype.Add(-45, "public");
            accesstype.Add(-48, "private");

            //Tipos de Variables
            vartype.Add(-54, "int");
            vartype.Add(-55, "bool");
            vartype.Add(-56, "string");
            vartype.Add(-57, "double");

            boolval.Add(-90, "true");
            boolval.Add(-91, "false");
        }
        #endregion

        public void AnalizadorSintactico()
        {
            foreach (Tokens item in listaTokens) { TokenList.AddLast(item); }

            LinkedListNode<Tokens> head = new LinkedListNode<Tokens>(new Tokens("HEAD", "NODO", 0, 1));
            TokenList.AddFirst(head);

            LinkedListNode<Tokens> p = TokenList.First;

            while (p != null && p.Next != null && !hasSyntaxErrors)
            {
                currentline = p.Value.Linea;
                switch (p.Next.Value.Valor)
                {
                    case -41:
                        p = p.Next;
                        p = Class(p); 
                        break;
                    case -44:
                        p = p.Next;
                        p = Namespace(p); 
                        break;
                    case -86:
                        p = p.Next;
                        p = Libraries(p); 
                        break;
                    case -51:
                        p = p.Next;
                        p = MainMethod(p);
                        break;
                    default:
                        if (vartype.ContainsKey(p.Next.Value.Valor))
                        {
                            p = p.Next;
                            p = VartypeDeclaration(p);
                        }
                        else if (p.Next.Value.Valor == -1)
                        {
                            p = p.Next;
                            switch (p.Value.Lexema)
                            {
                                case "Console":
                                    p = ConsoleSentence(p);
                                    break;
                                default:
                                    p = VarDeclaration(p);
                                    break;
                            }
                        }
                        else if (p.Next.Value.Lexema == "while")
                        {
                            p = p.Next;
                            p = While(p);
                        }
                        else if (p.Next.Value.Lexema == "if")
                        {
                            p = p.Next;
                            p = If(p);
                        }
                        else AddError(600, string.Empty);
                        break;
                }
            }
        }

        public void AddError(int errorcode, string symbol)
        {
            string errordesc = string.Empty;
            switch (errorcode)
            {
                case 600: errordesc = "Error de Sintaxis"; break;
                case 601: errordesc = "Se esperaba Identificador"; break;
                case 602: errordesc = "Se esperaba asignacion"; break;
                case 603: errordesc = "Se esperaba " + symbol; break;
                case 604: errordesc = "Se esperaba operando" + symbol; break;

                case 701: errordesc = "Variable " + symbol + " no declarada"; break;
                case 702: errordesc = "La variable " + symbol + " ya está declarada"; break;
                case 703: errordesc = "Tipos de variable incompatibles"; break;
                case 704: errordesc = "La variable " + symbol + " no tiene un valor asignado"; break;
            }

            if (errorcode < 700) hasSyntaxErrors = true;
            else semError = true;

            ErrorS.Add("Linea " + currentline.ToString() + " ERROR " + errorcode.ToString() + ": " + errordesc);
        }

        #region Statements
        private LinkedListNode<Tokens> VartypeDeclaration(LinkedListNode<Tokens> p)
        {
            //entrada int / double / string
            string variabletipo = p.Value.Lexema;
            currentline = p.Value.Linea;
            string variablename;

            p = p.Next;
            if (p != null && p.Value.Valor == -1)
            {
                Tokens nombre = p.Value;
                p = p.Next;
                if (p != null && p.Value.Lexema == ";")
                {
                    if(GetVariable(nombre) != null)
                    {
                        AddError(702, nombre.Lexema);
                    }
                    else
                    {
                        Variable varx = new Variable(variabletipo, nombre.Lexema, null, currentline);
                        variableList.AddLast(varx);
                    }
                    return p;
                }
                else if (p != null && p.Value.Lexema == "=")
                {
                    p = p.Next;
                    if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                    {
                        if (GetVariable(nombre) != null)
                        {
                            AddError(702, nombre.Lexema);
                        }
                        else
                        {
                            Variable varx = new Variable(variabletipo, nombre.Lexema, null, currentline);
                            variableList.AddLast(varx);
                        }
                        p = Assignment(p); //regresa en ;
                    }
                    else AddError(602, string.Empty);
                }
                else AddError(603, "= o asignación");
            }
            return p;
        }

        private LinkedListNode<Tokens> VarDeclaration(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            Tokens nombre = p.Value;
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Lexema == "++" || p.Value.Lexema == "--")
            {
                Tokens operando = p.Value;
                p = p.Next;
                if (p != null && p.Value.Lexema == ";")
                {
                    Variable currentVar = GetVariable(nombre);
                    if(currentVar == null) AddError(701, nombre.Lexema);
                    else { if(currentVar.Type != "int") AddError(703, string.Empty); }

                    return p;
                } else AddError(603, ";");
            }
            else if (p != null && p.Value.Lexema == "=")
            {
                p = p.Next;
                if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                {
                    Variable currentVar = GetVariable(nombre);
                    if (currentVar == null) AddError(701, nombre.Lexema);

                    p = Assignment(p); //regresa en ;
                }
                else AddError(602, string.Empty);
            }
            else AddError(600, string.Empty);
            return p;
        }

        private LinkedListNode<Tokens> ConsoleSentence(LinkedListNode<Tokens> p)
        {
            //Entrada Console
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Lexema == ".")
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "WriteLine")
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == "(")
                    {
                        p = PrintAssignment(p); //Regresa en )

                        if (p != null && p.Value.Lexema == ")")
                        {
                            p = p.Next;
                            if (p != null && p.Value.Lexema == ";")
                            {
                                return p;
                            }
                            else AddError(603, ";");
                        }
                        else AddError(603, ")");
                    }
                    else AddError(603, "(");
                }
                else if (p != null && p.Value.Lexema == "ReadLine")
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == "(")
                    {
                        p = p.Next;
                        if (p != null && p.Value.Lexema == ")")
                        {
                            p = p.Next;
                            if (p != null && p.Value.Lexema == ";")
                            {
                                return p;
                            }
                            else AddError(603, ";");
                        }
                        else AddError(603, ")");
                    }
                    else AddError(603, "(");
                }
                else AddError(600, string.Empty);
            }
            else AddError(600, string.Empty);

            return p;
        }
        #endregion

        #region Espacios
        private LinkedListNode<Tokens> Libraries(LinkedListNode<Tokens> p)
        {
            //Entrada: using
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Lexema == "System")
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == ";") return p;
                else AddError(603, ";");
            }
            else AddError(601, string.Empty);
            return p;
        } //DONE

        private LinkedListNode<Tokens> Namespace(LinkedListNode<Tokens> p)
        {
            //Entrada namespace
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Valor == -1)
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "{")
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == "class")
                    {
                        p = Class(p); //retorna con } de clase
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "}")  return p;
                        else AddError(603, "}");
                    }
                    else if (p != null && p.Value.Lexema == "}") return p;
                    else AddError(603, "}");
                }
                else AddError(603, "{");
            }
            else AddError(601, string.Empty);
            return p;
        } 

        private LinkedListNode<Tokens> Class(LinkedListNode<Tokens> p)
        {
            //entrada class
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Valor == -1)
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "{")
                {
                    p = p.Next;
                    if (p.Value.Lexema == "static")
                    {
                        p = MainMethod(p); //retorna con } de clase
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "}") return p;
                        else AddError(603, "}");

                    } else if (p != null && p.Value.Lexema == "}") return p;
                    else AddError(603, "}");
                }
                else AddError(603, "{");
            }
            else AddError(601, string.Empty);
            return p;
        } 
        private LinkedListNode<Tokens> MainMethod(LinkedListNode<Tokens> p)
        {
            currentline = p.Value.Linea;
            //Entrada static
            p = p.Next;
            if (p != null && p.Value.Lexema == "void")
            {
                p = p.Next;
                if (p != null && p.Value.Lexema == "Main")
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == "(")
                    {
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "string")
                        {
                            p = p.Next;
                            if (p != null && p.Value.Lexema == "[")
                            {
                                p = p.Next;
                                if (p != null && p.Value.Lexema == "]")
                                {
                                    p = p.Next;
                                    if (p != null && p.Value.Lexema == "args")
                                    {
                                        p = p.Next;
                                        if (p != null && p.Value.Lexema == ")")
                                        {
                                            p = p.Next;
                                            if (p != null && p.Value.Lexema == "{")
                                            {
                                                p = Block(p); //Retorna con }
                                                if (p != null && p.Value.Lexema == "}") return p;
                                                else AddError(603, "}");

                                            } else AddError(603, "{");
                                        } else AddError(603, ")");
                                    } else AddError(601, string.Empty);
                                } else AddError(603, "]");
                            } else AddError(603, "[");
                        } else AddError(603, "declaración string");
                    } else AddError(603, "(");
                } else AddError(601, string.Empty);
            } else AddError(603, "tipo de retorno");

            return p;
        }

        #endregion
        private LinkedListNode<Tokens> Block(LinkedListNode<Tokens> p)
        {
            //Entrada {
            while (p.Next != null && !hasSyntaxErrors)
            {
                p = p.Next; //Algunos devuelven la ultima wea de la sentencia y pos XD, aparte de que entra con el {
                currentline = p.Value.Linea;
                if (vartype.ContainsKey(p.Value.Valor))
                {
                    p = VartypeDeclaration(p);
                }
                else if (p.Value.Valor == -1)
                {
                    switch (p.Value.Lexema)
                    {
                        case "Console":
                            p = ConsoleSentence(p);
                            break;
                        case "static":
                            p = MainMethod(p);
                            break;
                        default:
                            p = VarDeclaration(p);
                            break;
                    }
                }
                else if (p != null && p.Value.Lexema == "while")
                {
                    p = While(p);
                }
                else if (p != null && p.Value.Lexema == "if")
                {
                    p = If(p);
                }
                else if (p.Value.Lexema == "}") break;
                else AddError(600, string.Empty);
            }
            return p;
        }
        private LinkedListNode<Tokens> If(LinkedListNode<Tokens> p)
        {
            //Entrada if
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Lexema == "(")
            {
                p = p.Next;
                if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                {
                    p = Conditional(p);
                    if (p != null && p.Value.Lexema == ")")
                    {
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "{")
                        {
                            p = Block(p); //retorna con }
                            if(p != null && p.Value.Lexema == "}")
                            {
                                p = p.Next;
                                if (p.Value.Lexema == "else")
                                {
                                    p = p.Next;
                                    if (p != null && p.Value.Lexema == "if")
                                    {
                                        p = If(p);
                                    }
                                    else if (p != null && p.Value.Lexema == "{")
                                    {
                                        p = Block(p);
                                        if (p != null && p.Value.Lexema == "}") return p;
                                        else AddError(603, "}");
                                    }
                                    else AddError(603, "{");
                                } else return p;
                            }
                            else AddError(603, "}");
                        }
                        else AddError(603, "{");
                    }
                    else AddError(603, ")");
                }
                else AddError(601, string.Empty);
            }
            else AddError(603, "(");
            return p;
        }
        private LinkedListNode<Tokens> While(LinkedListNode<Tokens> p)
        {
            //Entrada while
            currentline = p.Value.Linea;
            p = p.Next;
            if (p != null && p.Value.Lexema == "(")
            {
                p = p.Next;
                if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                {
                    p = Conditional(p);
                    if (p != null && p.Value.Lexema == ")")
                    {
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "{")
                        {
                            p = Block(p);
                            if (p != null && p.Value.Lexema == "}") return p;
                            else AddError(603, "}");
                        }
                        else AddError(603, "{");
                    }
                    else AddError(603, ")");
                }
                else if (p != null && boolval.ContainsKey(p.Value.Valor))
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == ")")
                    {
                        p = p.Next;
                        if (p != null && p.Value.Lexema == "{")
                        {
                            p = Block(p);
                            return p;
                        }
                        else AddError(603, "{");
                    }
                    else AddError(603, ")");
                }
                else AddError(601, string.Empty);
            }
            else AddError(603, "(");
            return p;
        }


        private LinkedListNode<Tokens> PrintAssignment(LinkedListNode<Tokens> p)
        {
            //Entrada (
            p = p.Next;
            while(p != null && p.Value.Lexema != ")")
            {
                if(p != null && valuetypes.ContainsKey(p.Value.Valor))
                {
                    p = p.Next;
                    if (p != null && p.Value.Lexema == "+") { p = p.Next;  continue; }
                    else if (p != null && p.Value.Lexema == ")") break;
                    else AddError(603, "+ o cierre");
                }
                else AddError(601, string.Empty);
            }

            return p;
        }

        private LinkedListNode<Tokens> Conditional(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            Tokens var1 = p.Value;
            Tokens var2;
            Tokens relacion;

            p = p.Next;
            if (p != null && relsymbol.ContainsKey(p.Value.Valor))
            {
                relacion = p.Value;

                p = p.Next; // == id
                if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                {
                    var2 = p.Value;
                    EvaluarCondicion(var1, relacion, var2);

                    p = p.Next; // == id &&
                    if (logicsymbol.ContainsKey(p.Value.Valor))
                    {
                        p = p.Next; // == id && id
                        if (p != null && valuetypes.ContainsKey(p.Value.Valor)) p = Conditional(p);
                        else AddError(603, "relación");

                    }
                    else if (p != null && p.Value.Lexema == ")") return p;
                    else AddError(603, ")");
                }
                else AddError(603, "Valor");
            }
            return p;
        }

        private LinkedListNode<Tokens> Assignment(LinkedListNode<Tokens> p)
        {
            //Entrada ID
            LinkedList<Tokens> infijo = new LinkedList<Tokens>();
            infijo.AddFirst(p.Value);

            while (p != null && p.Value.Lexema != ";")
            {
                p = p.Next;
                if (p != null && arisymbol.ContainsKey(p.Value.Valor))
                {
                    p = p.Next;
                    if (p != null && valuetypes.ContainsKey(p.Value.Valor))
                    {
                        infijo.AddLast(p.Value);
                    }
                    else AddError(603, "Valor");
                }
                if (p != null && p.Value.Lexema == ";") break;
            }

            Posfijo = string.Empty;
            InfixPosfix(infijo.ToArray());

            return p;
        }

        #region SEMANTICA
        
        public void InfixPosfix(Tokens[] infix)
        {
            Dictionary<int, string> Prioridad = new Dictionary<int, string>(){ {1, "+"}, {2, "-"}, {3, "*"}, {4, "/"} };
            Stack<Tokens> res = new Stack<Tokens>();
            Stack<Tokens> aux = new Stack<Tokens>();

            if (!hasSyntaxErrors)
            {
                for (int i = 0; i < infix.Length; i++)
                {
                    if (valuetypes.ContainsKey(infix[i].Valor))
                    {
                        if((infix[i].Valor) == -1) 
                        {
                            //Error 1: No se podrá comprobar su tipo si alguna veriable no existe
                            if (GetVariable(infix[i]) == null) AddError(701, infix[i].Lexema);
                        }

                        res.Push(infix[i]);
                    }
                    else if (arisymbol.ContainsKey(infix[i].Valor))
                    {
                        aux.Push(infix[i]);

                        if (aux.Count > 1)
                        {

                        }

                    }
                }

                EvaluarPosfijo(res.ToArray());
            }

    }

        public void EvaluarPosfijo(Tokens[] posfix)
        {
            Dictionary<Tokens, string> varlist = new Dictionary<Tokens, string>();

            if (!hasSyntaxErrors && !semError)
            {
                for (int i = 0; i < posfix.Length; i++)
                {
                    if (valuetypes.ContainsKey(posfix[i].Valor))
                    {
                        varlist.Add(posfix[i], GetVarType(posfix[i])); //Cada variable que encuentra la guarda con su tipo
                    }
                    else if (arisymbol.ContainsKey(posfix[i].Valor))
                    {

                    }
                }
            }
        }

        public string GetVarType(Tokens variable)
        {
            string tipo = string.Empty;
            Variable tempVar;

            switch (variable.Valor)
            {
                case -1:
                    tempVar = GetVariable(variable);
                    if (tempVar == null)
                    {
                        AddError(701, variable.Lexema);
                    }
                    else tipo = tempVar.Type;
                    break;
                case -2:
                    tipo = "int";
                    break;
                case -3:
                    tipo = "double";
                    break;
                case -4:
                    tipo = "string";
                    break;
            }

            return tipo;
        }

        public Variable GetVariable(Tokens id)
        {
            foreach (Variable var in variableList)
            {
                if (id.Lexema == var.Name)
                {
                    return var;
                }
            }
            return null;
        }

        private void EvaluarCondicion(Tokens var1, Tokens relacion, Tokens var2)
        {
            if (!hasSyntaxErrors)
            {
                string var1type, var2type;
                var1type = GetVarType(var1);
                var2type = GetVarType(var2);

                if (!semError)
                {
                    bool valido = sistematipos.EvaluarTipos(relacion, var1type, var2type);
                    if (valido == false) AddError(703, string.Empty);
                }
            }
        }
        #endregion
    }
}