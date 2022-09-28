using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    /*
    Statement para x = input
    Condiciones if y for
    Input y Output general
    Analisis semántico
    No evaluar cada token en el principal ._.XD
    Conversiones a string e int;
    Paréntesis para operaciones y condicionales
    Bloqueo en statements del tipo int i;
     */

    //NOTA 1: tokensGenenerados aun no se elimina, toca cambiar por LinkedList (listaTokens)

    class Sintaxis : Lexico
    {
        public Queue<Tokens> listasyntaxErrores = new Queue<Tokens>();
        private Dictionary<int, string> vartype = new Dictionary<int, string>();
        private Dictionary<int, string> accesstype = new Dictionary<int, string>();
        private Dictionary<int, string> arisymbol = new Dictionary<int, string>();
        private Dictionary<int, string> assignsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> logicsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> relsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> boolval = new Dictionary<int, string>();
        private Dictionary<int, string> ciclo = new Dictionary<int, string>();
        private Dictionary<int, string> valuetypes = new Dictionary<int, string>();

        public void StartSyntax()
        {
            AddSyntax(vartype, accesstype, arisymbol, assignsymbol, logicsymbol, relsymbol, boolval, ciclo, valuetypes);
            AnalizadorSintactico();
        }

        public void AddSyntax(Dictionary<int, string> var, Dictionary<int, string> acc, Dictionary<int, string> ar, Dictionary<int, string> asign, Dictionary<int, string> lo, Dictionary<int, string> rel, Dictionary<int, string> bools, Dictionary<int, string> cy, Dictionary<int, string> values)
        {
            values.Add(-1, "Identificador");
            values.Add(-2, "Entero");
            values.Add(-3, "Decimal");
            values.Add(-4, "Cadena");
            values.Add(-5, "Caracter");

            //Aritmético
            ar.Add(-6, "+");
            ar.Add(-7, "-");
            ar.Add(-8, "*");
            ar.Add(-9, "/");
            ar.Add(-10, "%");

            //Asignativos
            asign.Add(-11, "+=");
            asign.Add(-12, "-=");
            asign.Add(-13, "*=");
            asign.Add(-14, "/=");
            asign.Add(-15, "++");
            asign.Add(-16, "--");

            //Relacional
            rel.Add(-17, "<");
            rel.Add(-18, ">");
            rel.Add(-19, "=");
            rel.Add(-20, ">=");
            rel.Add(-21, "<=");
            rel.Add(-22, "==");
            rel.Add(-23, "!");
            rel.Add(-24, "!=");

            //Logico
            lo.Add(-25, "&");
            lo.Add(-26, "&&");
            lo.Add(-27, "|");
            lo.Add(-28, "||");
            lo.Add(-29, "??");
            lo.Add(-30, "^");

            //Alcance
            acc.Add(-45, "public");
            acc.Add(-46, "protected");
            acc.Add(-47, "internal");
            acc.Add(-48, "private");
            acc.Add(-49, "abstract");
            acc.Add(-50, "sealed");
            acc.Add(-51, "static");
            acc.Add(-52, "partial");
            acc.Add(-53, "override");

            //Tipos de Variables
            var.Add(-54, "int");
            var.Add(-55, "bool");
            var.Add(-56, "string");
            var.Add(-57, "double");
            var.Add(-58, "float");
            var.Add(-59, "char");
            var.Add(-60, "var");
            var.Add(-61, "object");
            var.Add(-62, "enum");
            var.Add(-63, "struct");

            bools.Add(-90, "true");
            bools.Add(-91, "false");

            cy.Add(-69, "do");
            cy.Add(-73, "for");
            cy.Add(-74, "foreach");
            cy.Add(-75, "if");
            cy.Add(-82, "switch");
            cy.Add(-85, "try");
            cy.Add(-89, "while");
        }

        public void AnalizadorSintactico()
        {
            Tokens item = tokensGenerados.Peek();
            do
            {
                switch (item.Valor)
                {
                    case -41: item = Class(item); break;
                    case -44: item = Namespace(item); break;
                    case -86: item = Libraries(item); break;
                    default:
                        if (vartype.ContainsKey(item.Valor) || item.Valor == -1)
                        {
                            item = Statement(item);
                            if (item.Lexema == ";") break;
                        }
                        else if (item.Lexema == "Console")
                        {
                            item = InOut(2, item);
                        }
                        else if (accesstype.ContainsKey(item.Valor))
                        {
                            if (item.Lexema == "static")
                            {
                                item = MainMethod(item);
                            }
                            else { AddError(item, -600); break; }
                        }
                        else { AddError(item, -600); break; }
                        continue;
                }
                item = GetNextItem(item);
                if (item == null) break;
            } while ((item = GetNextItem(item)) != null);
        }

        private Tokens GetNextItem(Tokens item)
        {
            int index = tokensGenerados.ToArray().ToList().IndexOf(item);
            Tokens[] listaTokens = tokensGenerados.ToArray();

            try { Tokens next = listaTokens[index + 1]; return next; }
            catch (IndexOutOfRangeException) { return null; }
        }

        private void AddError(Tokens item, int error)
        {
            string type = string.Empty;
            switch (error)
            {
                case -600: type = "Error de Sintaxis"; break;
                case -601: type = "Se esperaba Identificador"; break;
                case -602: type = "Se esperaba un valor"; break;
                case -603: type = "Se esperaba un booleano"; break;
                case -604: type = "Se esperaba un operando"; break;
                case -605: type = "Se esperaba " + item.Lexema; break;
                case -606: type = "Variable no especificada"; break;
                case -607: type = "Acceso no especificado"; break;
                case -608: type = "Se esperaba un operador"; break;
                case -609: type = "Se esperaba un operador relacional"; break;
            }

            Tokens tempError = new Tokens(type, item.Lexema, error, item.Linea);
            listasyntaxErrores.Enqueue(tempError);
        }

        private Tokens Block(int type, Tokens item) //Bloques {}
        {
            //ENTRADA: { 
            do
            {
                item = GetNextItem(item);
                switch (type)
                {
                    case 1: //Funciones
                        if (vartype.ContainsKey(item.Valor) || item.Valor == 1)
                        {
                            item = Statement(item);
                            if (item.Lexema == ";") continue;
                        }
                        else if (item.Lexema == "Console")
                        {
                            InOut(3, item);
                        }
                        else
                        {
                            switch (item.Lexema)
                            {
                                case "if":
                                    item = If(item);
                                    break;
                                case "while":
                                    item = While(item);
                                    break;
                                case "for":
                                    item = For(item);
                                    break;
                                default: AddError(item, -600); break;
                            }
                        }
                        break;

                    case 2: //Clases
                        if (vartype.ContainsKey(item.Valor) || item.Valor == 1) Statement(item);
                        else if (accesstype.ContainsKey(item.Valor))
                        {
                            if (item.Lexema == "static") MainMethod(item);
                            else AddError(item, -600);

                        }
                        else AddError(item, -600);
                        break;

                    case 3: // block de Namespace
                        switch (item.Valor)
                        {
                            case -41: item = Class(item); continue;
                            default: AddError(item, -600); break;
                        }
                        break;
                }

            } while (item.Lexema != "}");
            if (item.Lexema == "}") return item;
            return item;
        }
        private Tokens Statement(Tokens item)
        {
            string[] variables;
            if (item.Valor == -1) //x = a
            {
                //x++
                item = GetNextItem(item);
                if (assignsymbol.ContainsKey(item.Valor) || item.Valor == -19)
                {
                    if (item.Lexema == "++" || item.Lexema == "--")
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == ";")
                        {
                            return item;
                        }
                        else AddError(item, -605);
                    }
                    else if (item.Lexema == "=")
                    {
                        item = GetNextItem(item);
                        if (item.Valor == -1) //int x = a
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return item; //x = a;
                            else if (arisymbol.ContainsKey(item.Valor)) Operation(item); //x = a +...
                            else AddError(item, -605);
                        }
                        else if (valuetypes.ContainsKey(item.Valor)) //x = 4
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return item; //int x = a;
                            else if (arisymbol.ContainsKey(item.Valor)) Operation(item); //int x = a +...
                            else AddError(item, -605);
                        }
                        else if (item.Lexema == "Console")
                        {
                            InOut(1, item);
                            return item;
                        }
                    }
                    else //x *= 2;
                    {
                        item = GetNextItem(item);
                        if (valuetypes.ContainsKey(item.Valor)) //Numeros
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return item; //x += 2;
                            else AddError(item, -605);
                        }
                        else AddError(item, -602);
                    }
                }
            }
            else if (vartype.ContainsKey(item.Valor)) //int
            {
                item = GetNextItem(item);
                if (item.Valor == -1) // int x
                {
                    item = GetNextItem(item);
                    if (assignsymbol.ContainsKey(item.Valor) || item.Lexema == "=") // int x =
                    {
                        item = GetNextItem(item);
                        if (valuetypes.ContainsKey(item.Valor)) //int x = a
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return item; //x = a;
                            else if (arisymbol.ContainsKey(item.Valor)) Operation(item); //x = a +...
                            else AddError(item, -605);
                        }
                        else AddError(item, -602);
                    }
                    else if (item.Lexema == ",")
                    {
                        Varios(item);
                        if (item.Lexema == ";") return item;
                    }
                    else if (item.Lexema == ";") return item;
                }
                else AddError(item, -601);
            }
            return item;
        }

        private Tokens Libraries(Tokens item)
        {
            //ENTRADA: using
            int lineaactual = item.Linea;
            item = GetNextItem(item);
            if (item.Lexema == "System")
            {
                while (item.Linea == lineaactual)
                {
                    item = GetNextItem(item);
                    if (item.Lexema == ";") return item;
                }

            }
            else AddError(item, -601);
            return item;
        }

        private Tokens Class(Tokens item)
        {
            //<class>::= <accesstype> {static} class >id> { : <id>} <block>
            //Entrada: class
            item = GetNextItem(item);
            if (item.Valor == -1)
            {
                item = GetNextItem(item);
                if (item.Lexema == "{")
                {
                    item = Block(2, item);
                    return item;
                }
                else AddError(item, -605);
            }
            else AddError(item, -601);
            return item;
        }
        private Tokens Namespace(Tokens item)
        {
            //ENTRADA: namespace
            item = GetNextItem(item);
            if (item.Valor == -1)
            {
                item = GetNextItem(item);
                if (item.Lexema == "{")
                {
                    item = Block(2, item);
                    if (item.Lexema == "}") return item;
                    else AddError(item, -605);
                }
                else AddError(item, -605);
            }
            else AddError(item, -601);
            return item;
        }

        private Tokens MainMethod(Tokens item)
        {
            //ENTRADA: static
            item = GetNextItem(item);

            if (item.Lexema == "void")
            {
                item = GetNextItem(item);
                if (item.Lexema == "Main")
                {
                    item = GetNextItem(item);
                    if (item.Lexema == "(")
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "string")
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == "[")
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == "]")
                                {
                                    item = GetNextItem(item);
                                    if (item.Lexema == "args")
                                    {
                                        item = GetNextItem(item);
                                        if (item.Lexema == ")")
                                        {
                                            item = GetNextItem(item);
                                            if (item.Lexema == "{")
                                            {
                                                item = Block(1, item);
                                                if (item.Lexema == "}") return item; else AddError(item, -605);
                                            }
                                            else AddError(item, -605);
                                        }
                                        else AddError(item, -605);
                                    }
                                    else AddError(item, -600);
                                }
                                else AddError(item, -605);
                            }
                            else AddError(item, -605);
                        }
                        else AddError(item, -606);
                    }
                    else AddError(item, -605);
                }
                else AddError(item, -601);
            }
            else AddError(item, -600);

            return item;
        }

        private Tokens For(Tokens item)
        {
            //Entrada: for
            item = GetNextItem(item);
            if (item.Lexema == "(")
            {
                item = GetNextItem(item);
                if (item.Lexema == "int")
                {
                    item = GetNextItem(item);
                    if (item.Valor == -1)
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "=")
                        {
                            item = GetNextItem(item);
                            if (item.Valor == -2)
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == ";")
                                {
                                    item = GetNextItem(item);
                                    if (item.Valor == -1)
                                    {
                                        item = GetNextItem(item);
                                        if (item.Lexema == "<" || item.Lexema == ">" || item.Lexema == "<=" || item.Lexema == ">=")
                                        {
                                            item = GetNextItem(item);
                                            if (item.Valor == -2)
                                            {
                                                item = GetNextItem(item);
                                                if (item.Lexema == ";")
                                                {
                                                    item = GetNextItem(item);
                                                    if (item.Valor == -1)
                                                    {
                                                        item = GetNextItem(item);
                                                        if (item.Lexema == "++" || item.Lexema == "--")
                                                        {
                                                            item = GetNextItem(item);
                                                            if (item.Lexema == ")")
                                                            {
                                                                item = GetNextItem(item);
                                                                if (item.Lexema == "{")
                                                                {
                                                                    Block(1, item);
                                                                    if (item.Lexema == "}") return item;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return item;
    }
        private Tokens While(Tokens item)
        {
            //<while>::= while (<conditional> | <boolvalue> ) <block>
            item = GetNextItem(item);
            if (item.Lexema == "(")
            {
                item = GetNextItem(item);
                if (item.Valor == -1)
                {
                    //Conditional
                    item = GetNextItem(item);
                    if (item.Lexema == ")")
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "{")
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == "}") return item;
                            else AddError(item, -605);
                        }
                    }
                }
                else if (boolval.ContainsKey(item.Valor))
                {
                    item = GetNextItem(item);
                    if (item.Lexema == ")")
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "{")
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == "}") return item;
                            else AddError(item, -605);
                        }
                    }
                } else AddError(item, -605);
            }
            return item;
    }
        private Tokens If(Tokens item)
        {
            item = GetNextItem(item);
            if (item.Lexema == "(")
            {
                item = GetNextItem(item);
                if (item.Valor == -1)
                {
                    Conditional(item);
                    if (item.Lexema == ")")
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "{")
                        {
                            Block(1, item);
                            if (item.Lexema == "}")
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == "else")
                                {
                                    item = GetNextItem(item);
                                    if (item.Lexema == "if")
                                    {
                                        if (item.Lexema == "(")
                                        {
                                            item = GetNextItem(item);
                                            if (item.Valor == -1)
                                            {
                                                Conditional(item);
                                                if (item.Lexema == ")")
                                                {
                                                    item = GetNextItem(item);
                                                    if (item.Lexema == "{")
                                                    {
                                                        Block(1, item);
                                                        if (item.Lexema == "}")
                                                        {
                                                            item = GetNextItem(item);
                                                            if (item.Lexema == "else")
                                                            {
                                                                item = GetNextItem(item);
                                                                if (item.Lexema == "{")
                                                                {
                                                                    Block(1, item);
                                                                    if (item.Lexema == "}") return item;
                                                                }
                                                            }
                                                            }
                                                            else return item;
                                                    }
                                                        AddError(item, -605);
                                                    } AddError(item, -605);
                                                } AddError(item, -605);
                                            } AddError(item, -601);
                                        } AddError(item, -605);
                                    } else return item;
                            } AddError(item, -605);
                            } AddError(item, -605);
                        } AddError(item, -601);
                    } AddError(item, -605);
                } AddError(item, -605);
            return item;
        }
        private void Conditional(Tokens item)
        {
            //ENTRADA: ID
            item = GetNextItem(item);
            if (relsymbol.ContainsKey(item.Valor)) // ==
            {
                item = GetNextItem(item); ; // == id
                if (item.Valor == -1)
                {
                    item = GetNextItem(item); // == id &&
                    if (logicsymbol.ContainsKey(item.Valor))
                    {
                        item = GetNextItem(item); // == id && id
                        Conditional(item);
                        if (item.Lexema == ")") return;
                    }
                    else return;
                } else AddError(item, -601);
            } else AddError(item, -604);
        }
        private void Operation(Tokens item)
        {
            //ENTRADA: ID
            item = GetNextItem(item);
            if (arisymbol.ContainsKey(item.Valor)) //id +
            {
                item = GetNextItem(item); // id + id
                if (item.Valor == -1)
                {
                    item = GetNextItem(item); // id + id +
                    if (arisymbol.ContainsKey(item.Valor))
                    {
                        item = GetNextItem(item); // id + id + id
                        Operation(item);
                    }
                    else return;
                }
                else if(item.Lexema == "(")
                {
                    item = GetNextItem(item); // id + id + id
                    Operation(item);
                }
                else AddError(item, -601);
            }
            else AddError(item, -604);
        }
        private void Varios(Tokens item)
        {
            //Entrada: ,
            item = GetNextItem(item);
            if (item.Valor == -1) // a, b
            {
                item = GetNextItem(item);
                if (item.Lexema == ",")
                {
                    Varios(item);
                }
                else if (item.Lexema == ";") //int x , y;
                {
                    return;
                }
                else if (assignsymbol.ContainsKey(item.Valor))
                {
                    if (item.Lexema == "=")
                    {
                        item = GetNextItem(item);
                        if (item.Valor == -1) //int x = a
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return; //x = a;
                            else if (arisymbol.ContainsKey(item.Valor)) Operation(item); //x = a +...
                            else AddError(item, -605);
                        }
                        else if (valuetypes.ContainsKey(item.Valor)) //x = 4
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return; //int x = a;
                            else if (arisymbol.ContainsKey(item.Valor)) Operation(item); //int x = a +...
                            else AddError(item, -605);
                        }
                    }
                    else //x *= 2;
                    {
                        item = GetNextItem(item);
                        if (item.Valor == -2 || item.Valor == -2) //Numeros
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return; //x += 2;
                            else AddError(item, -605);
                        }
                        else if (item.Valor == -1) //Variable
                        {
                            item = GetNextItem(item);
                            if (item.Lexema == ";") return; //x += a;
                            else AddError(item, -605);
                        }
                        else AddError(item, -602);
                    }
                }
                else
                {
                    AddError(item, -600);
                }
            }
        }

        private Tokens InOut(int type, Tokens item)
        {
            //Entrada: Console
            switch (type)
            {
                case 1: //Input: Console.ReadLine(); ENTRA VARIABLE
                    item = GetNextItem(item);

                    break;
                case 2:
                    item = GetNextItem(item);
                    if(item.Lexema == ".")
                    {
                        if (item.Lexema == "WriteLine")
                        {
                            if (item.Lexema == "(")
                            {
                                if (valuetypes.ContainsKey(item.Valor)) //Cadena o cosa
                                {//Peta si pongo un + verda?
                                    if (item.Lexema == ")")
                                    {
                                        if (item.Lexema == ";")
                                        {
                                            return item;
                                        }
                                    }
                                }
                            }
                        }
                        else if (item.Lexema == "ReadLine")
                        {
                            if (item.Lexema == "(")
                            {
                                if (item.Lexema == ")")
                                {
                                    return item;
                                }
                            }
                        }
                    }
                    break;
            }

            return item;
        }

        private void AddVariable(Tokens item)
        {
            //Variable |  Nombre  |   Tipo   |  Valor  | Linea
        }

    }
}
