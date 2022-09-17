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
        private Dictionary<int, string> vartype, accesstype, arisymbol, assignsymbol, logicsymbol, relsymbol, boolval;

        int currenttoken, nexttoken;

        public void StartSyntax()
        {
            AddSyntax(vartype, accesstype, arisymbol, assignsymbol, logicsymbol, relsymbol, boolval);
            AnalizadorSintactico();
        }

        public void AddSyntax(Dictionary<int, string> var, Dictionary<int, string> acc, Dictionary<int, string> ar, Dictionary<int, string> asign, Dictionary<int, string> lo, Dictionary<int, string> rel, Dictionary<int, string> bools)
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
        private void ID(Tokens item) //Nombre de variable
        {
            //< id >::= id | < id >.< id >
            if (item.Valor != -1) AddError(item, -601);
        }

        private void Value(Tokens item) //Valor de variables
        {
            //<value>::= value
            if (!(item.Valor < 0 && item.Valor >= -5)) AddError(item, -602);
        }
       
        private void Block(Tokens item) //Bloques {}
        {
            //< block >::= {cualqier cosa que no sea clase o espacio del tipo* }
            do
            {

            } while (!(item.Valor == 18)); //cierre de bloque

        }
        private void Statement(Tokens item, Tokens next)
        {
            // <variabledec>::= <variabletype> <id> {,<id>} {=<value>};
            ResetCurrentItem(item, next);

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
        }
        private void Libraries(Tokens item, Tokens next)
        {
            //<libraries>::=using System {.<id>} ;
        }

        private void Class(int type, Tokens item, Tokens next)
        {
            //<class>::= <accesstype> {static} class >id> { : <id>} <block>

            if (type == 1) //class Nombre
            {

            }
            else //public class Nombre
            {


            }

        }
        private void Namespace(Tokens item, Tokens next)
        {
            //<namespace>::=  namespace <id><block>
        }
        private void Interface(Tokens item, Tokens next)
        {
            //<interface>::= interface <id><block>
        }

        #region SentenciasCiclos
        private void For(Tokens item)
        {
            //<for>::=  for ((<variabledec>|<id>); <condicional>; <id>++) <block>
        }
        private void DoWhile(Tokens item)
        {
            //<do>::= do <block> while (<conditional>);
        }
        private void While(Tokens item)
        {
            //<while>::= while (<conditional> | <boolvalue> ) <block>
        }
        private void If(Tokens item)
        {
            //<if>::= if <condicional> <block> {else if <conditional> <block>}* {else <block>}*
        }
        private void Switch(Tokens item)
        {
            //<switch>::= switch ( < conditionL > ) < caseblock >
            //<caseblock>::= {(case <value> | default) : {(<variabledec> | <statement> | <function>)*} break ;}*
        }
        private void ForEach(Tokens item)
        {
            //<foreach>::= foreach (<vartype> <id> in <id> ) <block>

        }
        private void TryCatch(Tokens item)
        {
            //< trycatch >::= try < block > catch (Exception<id>) < block >
        }
        #endregion

        #endregion
    }
}
