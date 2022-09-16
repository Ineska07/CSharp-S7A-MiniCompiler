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
       
        public void AnalizadorSintactico()
        {
            foreach (Tokens item in tokensGenerados)
            {
                int currenttoken = item.Valor;

                switch (currenttoken)
                {
                    case -1: //Identificador
                        Statement(item); continue;

                    //POR TIPO----------------------
                    case -41: Class(item); continue;
                    case -43: Interface(item); continue;
                    case -44: Namespace(item); continue;

                    //POR ACCESO--------------------
                    case -45: //public
                        AccessType(item);  continue;
                    case -46: //protected
                        AccessType(item); continue;
                    case -47: //internal
                        AccessType(item); continue;
                    case -48: //private
                        AccessType(item); continue;
                    case -49: //abstract
                        AccessType(item); continue;
                    case -50: //sealed
                        AccessType(item); continue;
                    case -51: //static
                        AccessType(item); continue;
                    case -52: //partial
                        AccessType(item); continue;
                    case -53: //override
                        AccessType(item); continue;

                    //POR TIPO DE VARIABLE----------
                    case -54: //int
                        Statement(item); continue;
                    case -55: //bool
                        Statement(item); continue;
                    case -56: //string
                        Statement(item); continue;
                    case -57: //double
                        Statement(item); continue;
                    case -58: //float
                        Statement(item); continue;
                    case -59: //char

                    case -86: //Librerias
                        Libraries(item); continue;
                    default: //ERROR: que vergas es esto
                        AddError(item, -600);
                        continue;
                }
            }
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
                case -605: type = "Se esperaba "; break;
                case -606: type = "Variable no especificada"; break;
                case -607: type = "Acceso no especificado"; break;
            }

            Tokens tempError = new Tokens(type, item.Lexema, error, item.Linea);
            listasyntaxErrores.Enqueue(tempError);
        }

        #region ReglasTokens

        #region Predefinidos
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
        private void VariableType(Tokens item)
        {
            if (!(item.Valor < -53 && item.Valor >= -63)) AddError(item, -606);
        }
        private void AccessType(Tokens item)
        {

        }
        private void ArithmeticSymbol(Tokens item)
        {

        }
        private void BoolValue(Tokens item)
        {
            if (!(item.Lexema == "true" || item.Lexema == "false")) AddError(item, -603);
        }
        private void LogicOperando(Tokens item)
        {

        }
        #endregion

        private void Block(Tokens item) //Bloques {}
        {
            //< block >::= { (< variabledec > | < statement > | < function >) * }
            do
            {
                switch (item.Valor)
                {
                    case -1: //Identificador
                        Statement(item); continue;

                    //POR TIPO DE VARIABLE----------
                    case -54: //int
                        Statement(item); continue;
                    case -55: //bool
                        Statement(item); continue;
                    case -56: //string
                        Statement(item); continue;
                    case -57: //double
                        Statement(item); continue;
                    case -58: //float
                        Statement(item); continue;
                    case -59: //char

                    default: //ERROR: que vergas es esto
                        AddError(item, -600);
                        break;
                }

            } while (!(item.Valor == 18)); //cierre de bloque

        }
        private void Statement(Tokens item)
        {
            // <variabledec>::= <variabletype> <id> {,<id>} {=<value>} ;
            int valortoken = item.Valor;
            
        }

        private void Function(Tokens item)
        {
            //<function>::= <accesstype> <returning> <id> ( {<parameter>} )<block>
            //<returning>:= <variabletype> | void
        }
        private void Parameters(Tokens item)
        {
            //<parameter>::=<vartype><id>{,<vartype><id>}
            switch (item.Valor)
            {
                case -1:
                    ID(item); break;
            }
        }
        private void Libraries(Tokens item)
        {
            //<libraries>::=using System {.<id>} ;
        }

        private void Class(Tokens item)
        {
            //<class>::= <accesstype> {static} class >id> { : <id>} <block>
        }
        private void Namespace(Tokens item)
        {
            //<namespace>::=  namespace <id><block>
        }
        private void Interface(Tokens item)
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
