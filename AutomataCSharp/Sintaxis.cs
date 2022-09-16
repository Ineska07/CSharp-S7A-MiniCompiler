using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    //AQUI ESTÁN LAS REGLAS GENERALES
    /*
    <returning>:= <variabletype> | void
    <id>::= id | <id>.<id>
    <value>::= value

    <parameter>::=<vartype><id>{,<vartype><id>}
    <block>::= { (<variabledec> | <statement> | <function>)* }

    <variabledec>::= <variabletype> <id> {,<id>} {=<value>} ;
    <class>::= <accesstype> {static} class >id> { : <id>} <block>
    <namespace>::=  namespace <id><block>
    <interface>::= interface <id><block>
    <function>::= <accesstype> <returning> <id> ( {<parameter>} )<block>

    <for>::=  for ((<variabledec>|<id>); <condicional>; <id>++) <block>
    <do>::= do <block> while (<conditional>);
    <while>::= while (<conditional> | <boolvalue> ) <block>
    <if>::= if <condicional> <block> {else if <conditional> <block>}* {else <block>}*
    <switch>::= switch ( <conditionL> ) <caseblock>
    <caseblock>::= {(case <value> | default) : {(<variabledec> | <statement> | <function>)*} break ;}* 
    <foreach>::= foreach ( <id> in <id> ) <block>
    <trycatch>::= try <block> catch (Exception <id>) <block>

    <return>::= return (<id> | <value> | <operation> | <boolvalue>) ;
    <operation>::= (<id> | <value>) <arisymbol> (<id> | <value>) {<operation>}*
    <conditional> ::= ( <id> <logicop> <id> { && | || <id> <logicop> <id>*} )

    <libraries>::=using System {.<id>} ;
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
                        break;
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
                case -603: type = "Se esperaba ;"; break;
            }

            Tokens tempError = new Tokens(type, item.Lexema, error, item.Linea);
            listasyntaxErrores.Enqueue(tempError);
        }

        #region ReglasTokens
        private void ID(Tokens item) //Nombre de variable
        {
            if (item.Valor != -1) AddError(item, -601);
        }

        private void Value(Tokens item) //Valor de variables
        {
             //Si no es identificador, tronasion
            
        }
        private void Block(Tokens item) //Bloques {}
        {
            do
            {
                switch (item.Valor)
                {
                    case 1: //sentencias
                        break;
                    case 2: //funciones
                        break;
                    case 3: //}
                        break;
                    default: // tronasion
                        break;
                }

            } while (!(item.Valor == 18)); //cierre de bloque

        }

        private void VariableType(Tokens item)
        {
            //Inicia con tipo de variable
            ID(item);
            //siguiente token , ID
            // = value
            //; fin de instrucción
        }
        private void AccessType(Tokens item)
        {

        }
        private void ArithmeticSymbol(Tokens item)
        {

        }
        private void BoolValue(Tokens item)
        {

        }
        private void LogicOperando(Tokens item)
        {

        }

        private void Statement(Tokens item)
        {

        }
        private void Parameters(Tokens item)
        {

        }

        private void Libraries(Tokens item)
        {
            //Using
            //Va por siguiente token
            //Funcion Identificador
            //No c
        }

        private void Class(Tokens item)
        {
            //Using
            //Va por siguiente token
            //Funcion Identificador
            //No c
        }
        private void Namespace(Tokens item)
        {
            //Using
            //Va por siguiente token
            //Funcion Identificador
            //No c
        }
        private void Interface(Tokens item)
        {
            //Using
            //Va por siguiente token
            //Funcion Identificador
            //No c
        }

        private void For(Tokens item)
        {
        }
        private void DoWhile(Tokens item)
        {
        }
        private void While(Tokens item)
        {
        }
        private void If(Tokens item)
        {
        }
        private void ForEach(Tokens item)
        {
        }
        private void TryCatch(Tokens item)
        {
        }
        #endregion
    }
}
