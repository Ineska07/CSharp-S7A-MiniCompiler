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
        private Dictionary<int, string> vartype, accesstype, arisymbol, assignsymbol, logicsymbol, relsymbol, boolval, ciclo;

        int currenttoken, nexttoken;

        public void StartSyntax()
        {
            AddSyntax(vartype, accesstype, arisymbol, assignsymbol, logicsymbol, relsymbol, boolval, ciclo);
            AnalizadorSintactico();
        }

        public void AddSyntax(Dictionary<int, string> var, Dictionary<int, string> acc, Dictionary<int, string> ar, Dictionary<int, string> asign, Dictionary<int, string> lo, Dictionary<int, string> rel, Dictionary<int, string> bools, Dictionary<int, string> cy)
        {
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
                Tokens nextitem = GetNextItem(item);
                nexttoken = nextitem.Valor;

                //Toca el ResetToken cada cuanto pero eso pa luego

                switch (currenttoken)
                {
                    case -41: Class(1, item, nextitem); continue;
                    case -43: Interface(item, nextitem); continue;
                    case -44: Namespace(item, nextitem); continue;
                    case -86: Libraries(item, nextitem); continue;
                    default: 
                        if (vartype.ContainsKey(currenttoken) || currenttoken == 1)
                        {
                            Statement(item, nextitem);
                        }
                        else if(accesstype.ContainsKey(currenttoken))
                        {
                            if (vartype.ContainsKey(nexttoken) || nextitem.Lexema == "void" || nextitem.Lexema == "static")
                            {
                                //private {static} void
                                Function(item, nextitem);
                            }
                            else if (nexttoken == -41)
                            {
                                //private class
                                Class(2, item, nextitem);
                            }
                        }
                        else AddError(item, -600); //ERROR: que vergas es esto
                        continue;
                }
            }
        }

        private void ResetCurrentItem(Tokens item, Tokens next)
        {
            item = next;
            next = GetNextItem(item);
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
        private bool Value(Tokens item) //Valor de variables
        {
            //<value>::= value
            if (!(item.Valor < 0 && item.Valor >= -5)) 
            {
                AddError(item, -602);
                return false;
            } else return true;
        }
       
        private void Block(int type, Tokens item, Tokens next) //Bloques {}
        {
            //ENTRADA: { 
            do
            {
                switch (type)
                {
                    case 1: //Clases / namespaces / interfaces
                        if (vartype.ContainsKey(currenttoken) || currenttoken == 1)
                        {
                            Statement(item, next);
                        }
                        else if (accesstype.ContainsKey(currenttoken))
                        {
                            if (vartype.ContainsKey(nexttoken) || next.Lexema == "void" || next.Lexema == "static")
                            {
                                //private {static} void()
                                //public static int x = 12;
                            }
                            else if (nexttoken == -41)
                            {
                                //private class
                                Class(2, item, next);
                            }
                        }
                        else AddError(item, -600);
                        break;

                    case 2: //Funciones y ciclos
                        if (vartype.ContainsKey(currenttoken) || currenttoken == 1)
                        {
                            Statement(item, next);
                        }
                        else if (accesstype.ContainsKey(currenttoken))
                        {
                            if (ciclo.ContainsKey(nexttoken))
                            {
                                ResetCurrentItem(item, next);
                                switch (item.Lexema)
                                {
                                    case "do":
                                        DoWhile(item, next);
                                        break;
                                    case "for":
                                        For(item, next);
                                        break;
                                    case "foreach":
                                        ForEach(item, next);
                                        break;
                                    case "if":
                                        If(item, next);
                                        break;
                                    case "switch":
                                        Switch(item, next);
                                        break;
                                    case "try":
                                        TryCatch(item, next);
                                        break;
                                    case "while":
                                        While(item, next);
                                        break;
                                }
                            } else AddError(item, -600);
                        }
                        else AddError(item, -600);
                        break;

                    case 3: //Cases y otras weas
                        SwitchCases(item, next);
                        break;
                }

            } while (!(item.Valor == 18)); //cierre de bloque

        }
        private void Statement(Tokens item, Tokens next)
        {
            //ENTRADA: x || <variabletype>
            // <variabledec>::= <variabletype> <id> {,<id>} {=<value>};

            if (item.Valor == -1) //x =...
            {
                
            }
            else //int x =...
            {
                
            }
        }

        private void Function(Tokens item, Tokens next)
        {
            //<function>::= <accesstype> <returning> <id> ( {<parameter>} )<block>
            ResetCurrentItem(item, next);
        }
        private void Parameters(Tokens item)
        {
            //<parameter>::=<vartype><id>{,<vartype><id>}
            //Entrada: (

        }
        private void Libraries(Tokens item, Tokens next)
        {
            string libreria = string.Empty;
            //ENTRADA: using
            //<libraries>::=using System {.<id>} ;

            if (next.Valor == -1 && next.Lexema == "System")
            {
                while (true)
                {
                    libreria += item.Lexema;
                    ResetCurrentItem(item, next);
                    if (next.Lexema == ";")
                    {
                        //AddLibrary - Añadir en semántico
                        break;
                    }
                }
                
            }
            else AddError(item, -601);
        }

        private void Class(int type, Tokens item, Tokens next)
        {
            //<class>::= <accesstype> {static} class >id> { : <id>} <block>

            if (type == 1) //class Nombre
            {
                if (next.Valor == -1)
                {
                    ResetCurrentItem(item, next); //Actual: ID
                    if (next.Valor == -17) //Inicio de Bloque
                    {
                        ResetCurrentItem(item, next); //Actual: {
                        Block(1, item, next);
                    }
                    else AddError(item, -605);
                }
                else AddError(item, -601);
            }
            else //public class Nombre
            {


            }
        }
        private void Namespace(Tokens item, Tokens next)
        {
            //ENTRADA: namespace
            //<namespace>::=  namespace <id><block>
            if (next.Valor == -1)
            {
                ResetCurrentItem(item, next); //Actual: ID
                if (next.Valor == -17) //Inicio de Bloque
                {
                    ResetCurrentItem(item, next); //Actual: {
                    Block(1, item, next);
                }
                else AddError(item, -605);
            }
            else AddError(item, -601);
        }
        private void Interface(Tokens item, Tokens next)
        {
            if (next.Valor == -1)
            {
                ResetCurrentItem(item, next); //Actual: ID
                if (next.Valor == -17) //Inicio de Bloque
                {
                    ResetCurrentItem(item, next); //Actual: {
                    Block(1, item, next);
                }
                else AddError(item, -605);
            }
            else AddError(item, -601);
        }

        #region SentenciasCiclos
        private void For(Tokens item, Tokens next)
        {
            //<for>::=  for ((<variabledec>|<id>); <condicional>; <id>++) <block>
        }
        private void DoWhile(Tokens item, Tokens next)
        {
            //<do>::= do <block> while (<conditional>);
            ResetCurrentItem(item, next);
            if (item.Lexema == "{")
            {
                Block(2, item, next);
            }
            if (item.Lexema == "}")
            {
                ResetCurrentItem(item, next);
                if (item.Lexema == "while")
                {
                    ResetCurrentItem(item, next);
                    if (item.Lexema == "(")
                    {
                        ResetCurrentItem(item, next);
                        if(boolval.ContainsKey(item.Valor))
                        {
                            ResetCurrentItem(item, next);
                            if (item.Lexema == ")")
                            {
                                ResetCurrentItem(item, next);
                                if (item.Lexema == ";") return;
                                else AddError(item, -605);
                            }
                        }
                        else if(item.Valor == -1)
                        {
                            //Conditional(item, next);
                            ResetCurrentItem(item, next);
                            if (item.Lexema == ")")
                            {
                                ResetCurrentItem(item, next);
                                if (item.Lexema == ";") return;
                                else AddError(item, -605);
                            }
                        }
                    } else AddError(item, -605);
                } else AddError(item, -600);
            } else AddError(item, -605);
        }
        private void While(Tokens item, Tokens next)
        {
            //<while>::= while (<conditional> | <boolvalue> ) <block>
            ResetCurrentItem(item, next);
            if(item.Lexema == "(")
            {
                ResetCurrentItem(item, next);
                if (item.Valor == -1)
                {
                    //Conditional
                    ResetCurrentItem(item, next);
                    if (item.Lexema == ")")
                    {
                        ResetCurrentItem(item, next);
                        if (item.Lexema == "{")
                        {
                            ResetCurrentItem(item, next);
                            if (item.Lexema == "}") return;
                            else AddError(item, -605);
                        }
                    }
                }
                else if (boolval.ContainsKey(item.Valor))
                {
                    ResetCurrentItem(item, next);
                    if (item.Lexema == ")")
                    {
                        ResetCurrentItem(item, next);
                        if (item.Lexema == "{")
                        {
                            ResetCurrentItem(item, next);
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
        private void Switch(Tokens item, Tokens next)
        {
            //<switch>::= switch ( <id> ) < caseblock >
            //<caseblock>::= {(case <value> | default) : {(<variabledec> | <statement> | <function>)*} break ;}*
            ResetCurrentItem(item, next);
            if (item.Lexema == "(") 
            {
                if (item.Lexema == "(" && next.Lexema == ")") AddError(item, -601);
                else
                {
                    ResetCurrentItem(item, next);
                    if (item.Valor == -1)
                    {
                        ResetCurrentItem(item, next);
                        if (item.Lexema != ")") AddError(item, -600);
                        else
                        {
                            ResetCurrentItem(item, next);
                            if (item.Lexema != "{") AddError(item, -605);
                            else
                            {
                                ResetCurrentItem(item, next);
                                Block(3, item, next);
                            }
                        }
                    }
                    else AddError(item, -601);
                }
            }
        }
        private void SwitchCases(Tokens item, Tokens next)
        {
            //<caseblock>::= {(case <value> | default) : {(<variabledec> | <statement> | <function>)*} break ;}*
            while (true)
            {
                ResetCurrentItem(item, next);
                if (item.Lexema != "case") AddError(item, -600);
                else
                {
                    ResetCurrentItem(item, next);
                    if (item.Lexema != ":") AddError(item, -600);
                    else
                    {
                        CaseBlock(item, next);
                        if (item.Lexema == "default") break;
                    }
                }
            }
        }
        private void CaseBlock(Tokens item, Tokens next)
        {
            ResetCurrentItem(item, next);
            switch (item.Valor)
            {

                default:
                    if (next.Lexema == "break")
                    {
                        ResetCurrentItem(item, next);
                        return;
                    }
                    else AddError(item, -600);
                    break;
            }

            if (next.Lexema == "break") 
            {
                ResetCurrentItem(item, next);
                return; 
            }
        }
        private void ForEach(Tokens item, Tokens next)
        {
            //<foreach>::= foreach (<vartype> <id> in <id> ) <block>
            ResetCurrentItem(item, next);
            if (item.Lexema == "(")
            {
                if (vartype.ContainsKey(item.Valor))
                {
                    if(item.Valor == -1)
                    {
                        ResetCurrentItem(item, next);
                        if (item.Lexema == "in")
                        {
                            ResetCurrentItem(item, next);
                            if (item.Valor == -1)
                            {
                                ResetCurrentItem(item, next);
                                if (item.Lexema == ")")
                                {
                                    ResetCurrentItem(item, next);
                                    if (item.Lexema == "{")
                                    {
                                        Block(2, item, next);
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
            ResetCurrentItem(item, next);
            if (item.Lexema == "{")
            {
                Block(2, item, next);
            }
            if(item.Lexema == "}")
            {
                ResetCurrentItem(item, next);
                if (item.Lexema == "catch")
                {
                    ResetCurrentItem(item, next);
                    if (item.Lexema == "(")
                    {
                        ResetCurrentItem(item, next);
                        if (item.Valor == -1)
                        {
                            if (item.Valor == -1)
                            {
                                ResetCurrentItem(item, next);
                                if (item.Lexema == ")")
                                {
                                    ResetCurrentItem(item, next);
                                    if (item.Lexema == "{")
                                    {
                                        Block(2, item, next);
                                    }
                                    else AddError(item, -605);
                                }else AddError(item, -605);
                            }
                            else if (item.Lexema == ")")
                            {
                                ResetCurrentItem(item, next);
                                if (item.Lexema == "{")
                                {
                                    Block(2, item, next);
                                } else AddError(item, -605);
                            } else AddError(item, -605);
                        } else AddError(item, -601);
                    } else AddError(item, -605);
                } else AddError(item, -600);
            } else AddError(item, -605);
            //< trycatch >::= try < block > catch (Exception<id>) < block >
        }
        #endregion

        private void Conditional(Tokens item, Tokens next)
        {
            //ENTRADA: ID
            ResetCurrentItem(item, next);
            if (relsymbol.ContainsKey(item.Valor)) // ==
            {
                ResetCurrentItem(item, next); // == id
                if (item.Valor == -1)
                {
                    ResetCurrentItem(item, next); // == id &&
                    if (logicsymbol.ContainsKey(item.Valor))
                    {
                        ResetCurrentItem(item, next); // == id && id
                        Conditional(item, next);
                    }
                    else return;
                } else AddError(item, -601);
            } else AddError(item, -604);
        }
        private void Operation(Tokens item, Tokens next)
        {
            //ENTRADA: ID
            ResetCurrentItem(item, next);
            if (arisymbol.ContainsKey(item.Valor)) //id +
            {
                ResetCurrentItem(item, next); // id + id
                if (item.Valor == -1)
                {
                    ResetCurrentItem(item, next); // id + id +
                    if (arisymbol.ContainsKey(item.Valor))
                    {
                        ResetCurrentItem(item, next); // id + id + id
                        Operation(item, next);
                    }
                    else return;
                } else AddError(item, -601);
            }
            else AddError(item, -604);
        }

        #endregion
    }
}
