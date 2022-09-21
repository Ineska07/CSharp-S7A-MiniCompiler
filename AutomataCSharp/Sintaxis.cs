using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    //AQUI ESTÁN LAS REGLAS FALTANTES
    /*
    <return>::= return (<id> | <value> | <operation> | <boolvalue>) ;
    <operation>::= (<id> | <value>) <arisymbol> (<id> | <value>) {<operation>}*
    <conditional> ::= ( <id> <logicop> <id> { && | || <id> <logicop> <id>*} )
    <accvariable>::= <accesstype><variabledec>

    <objectdec> <id> = new <objectid> ( ) ;
    <objectid>::= <id> | object
    <objectmet>::= <objectid> . <function>
     */

    class Sintaxis : Lexico
    {
        public Queue<Tokens> listasyntaxErrores = new Queue<Tokens>();
        //public LinkedList<Variable> listaVariables = new LinkedList<Variable>();
        private Dictionary<int, string> vartype = new Dictionary<int, string>();
        private Dictionary<int, string> accesstype = new Dictionary<int, string>();
        private Dictionary<int, string> arisymbol = new Dictionary<int, string>();
        private Dictionary<int, string> assignsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> logicsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> relsymbol = new Dictionary<int, string>();
        private Dictionary<int, string> boolval = new Dictionary<int, string>();
        private Dictionary<int, string> ciclo = new Dictionary<int, string>();
        private Dictionary<int, string> valuetypes = new Dictionary<int, string>();

        int currenttoken;

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
            foreach (Tokens item in tokensGenerados)
            {
                currenttoken = item.Valor;

                switch (currenttoken)
                {
                    case -41: Class(item); continue;
                    case -44: Namespace(item); continue;
                    case -86: Libraries(item); continue;
                    default: 
                        if (vartype.ContainsKey(currenttoken) || currenttoken == 1)
                        {
                            Statement(item);
                        }
                        else if (accesstype.ContainsKey(currenttoken))
                        {
                            if (item.Lexema == "static")
                            {
                                MainMethod(item);
                            }
                            else AddError(item, -600);
                        }
                        else AddError(item, -600); //ERROR: que vergas es esto
                        continue;
                }
            }
        }

        private Tokens GetNextItem(Tokens item)
        {
            int index = tokensGenerados.ToArray().ToList().IndexOf(item);
            Tokens[] listaTokens = tokensGenerados.ToArray();

            Tokens next = listaTokens[index + 1];
            return next;
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

        #region Reglas
        private void Block(int type, Tokens item) //Bloques {}
        {
            //ENTRADA: { 
            do
            {
                switch (type)
                {
                    case 1: //Block dentro de funciones
                        break;
                    case 2: // block de clases
                        break;
                    case 3: // block de Namespace
                        break;
                }

            } while (!(item.Lexema == "}"));

        }
        private void Statement(Tokens item)
        {
            //ENTRADA: x || <variabletype>
            // <variabledec>::= <variabletype> <id> {,<id>} {=<value>};

            if (item.Valor == -1) //x = a
            {
                //x++
                if (assignsymbol.ContainsKey(item.Valor))
                {
                    if (item.Lexema == "++" || item.Lexema == "--") //x++;
                    {
                        if (item.Lexema == ";")
                        {

                        }
                        else
                        {

                        }
                    }
                    else if (item.Lexema == "=")
                    {

                    }
                    else //x *= 2;
                    {

                    }
                }
            }
            else if(vartype.ContainsKey(item.Valor)) //int x
            {
                if (item.Valor == -1) // int x
                {
                    if (assignsymbol.ContainsKey(item.Valor)) // int x =
                    {
                        //int x = a
                        //int x = 4
                    }
                    else if(item.Lexema == ",")
                    {
                        Varios(item);
                    }
                    else if (item.Lexema == ";")
                    {
                        return;
                    }
                }
            }

        }

        private void Libraries(Tokens item)
        {
            //ENTRADA: using
            //<libraries>::=using System {.<id>} ;

            int lineaactual = item.Linea;
            item = GetNextItem(item);
            if (item.Lexema == "System")
            {
                while (item.Linea == lineaactual)
                {
                    item = GetNextItem(item);
                    if (item.Lexema == ";") return;
                }
                
            }
            else AddError(item, -601);
        }

        private void Class(Tokens item)
        {
            //<class>::= <accesstype> {static} class >id> { : <id>} <block>
            //Entrada: class
            item = GetNextItem(item);
            
        }
        private void Namespace(Tokens item)
        {
            //ENTRADA: namespace
            item = GetNextItem(item);
            if (item.Valor == -1)
            {
                item = GetNextItem(item);
                if (item.Lexema == "{") Block(1, item);
                else AddError(item, -605);

            } else AddError(item, -601);
        }

        private void MainMethod(Tokens item)
        {
            //ENTRADA: static
            item = GetNextItem(item);

            if(item.Lexema == "void")
            {
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

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {

            }
        }

        #region SentenciasCiclos
        private void For(Tokens item, Tokens next)
        {
            //<for>::=  for ((<variabledec>|<id>); <condicional>; <id>++) <block>
        }
        private void While(Tokens item, Tokens next)
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
                            if (item.Lexema == "}") return;
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
                            if (item.Lexema == "}") return;
                            else AddError(item, -605);
                        }
                    }
                } else AddError(item, -605);
            }
        }
        private void If(Tokens item, Tokens next)
        {
            //<if>::= if <condicional> <block> {else if <conditional> <block>}* {else <block>}*
        }
        private void ForEach(Tokens item, Tokens next)
        {
            //<foreach>::= foreach (<vartype> <id> in <id> ) <block>
            item = GetNextItem(item);
            if (item.Lexema == "(")
            {
                if (vartype.ContainsKey(item.Valor))
                {
                    if(item.Valor == -1)
                    {
                        item = GetNextItem(item);
                        if (item.Lexema == "in")
                        {
                            item = GetNextItem(item);
                            if (item.Valor == -1)
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == ")")
                                {
                                    item = GetNextItem(item);
                                    if (item.Lexema == "{")
                                    {
                                        Block(2, item);
                                    } else AddError(item, -605);
                                } else AddError(item, -605);
                            } else AddError(item, -601);
                        } else AddError(item, -600);
                    } else AddError(item, -601);
                } else AddError(item, -606);
            }

        }
        private void TryCatch(Tokens item, Tokens next)
        {
            //ENTRADA: try
            item = GetNextItem(item);
            if (item.Lexema == "{")
            {
                Block(2, item);
            }
            if(item.Lexema == "}")
            {
                item = GetNextItem(item);
                if (item.Lexema == "catch")
                {
                    item = GetNextItem(item);
                    if (item.Lexema == "(")
                    {
                        item = GetNextItem(item);
                        if (item.Valor == -1)
                        {
                            if (item.Valor == -1)
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == ")")
                                {
                                    item = GetNextItem(item);
                                    if (item.Lexema == "{")
                                    {
                                        Block(2, item);
                                    }
                                    else AddError(item, -605);
                                }else AddError(item, -605);
                            }
                            else if (item.Lexema == ")")
                            {
                                item = GetNextItem(item);
                                if (item.Lexema == "{")
                                {
                                    Block(2, item);
                                } else AddError(item, -605);
                            } else AddError(item, -605);
                        } else AddError(item, -601);
                    } else AddError(item, -605);
                } else AddError(item, -600);
            } else AddError(item, -605);
            //< trycatch >::= try < block > catch (Exception<id>) < block >
        }
        #endregion

        private void Conditional(Tokens item)
        {
            //ENTRADA: ID
            item = GetNextItem(item);
            if (relsymbol.ContainsKey(item.Valor)) // ==
            {
                item = GetNextItem(item); ; // == id
                if (item.Valor == -1)
                {
                    item = GetNextItem(item); ; // == id &&
                    if (logicsymbol.ContainsKey(item.Valor))
                    {
                        item = GetNextItem(item); // == id && id
                        Conditional(item);
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
                } else AddError(item, -601);
            }
            else AddError(item, -604);
        }
        private void Varios(Tokens item)
        {
            //Entrada: ,

            item = GetNextItem(item);
            if (item.Valor == -1)
            {
                if (item.Lexema == ",")
                {
                    Varios(item);
                }
                else if (item.Lexema == ";" || assignsymbol.ContainsKey(item.Valor)) //int x , y = 0;
                {
                    return;
                }
            }
            else
            {

            }

            //int x , y;
        }

        private void InOut(int type, Tokens item)
        {
            //Entrada: 
            switch (type)
            {
                case 1: //Input: Console.ReadLine();
                    break;
                case 2: //Output: Console.WriteLine();

                    break;
            }
        }

        #endregion
    }
}
