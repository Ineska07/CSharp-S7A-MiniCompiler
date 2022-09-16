using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    #region Reglas
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
    #endregion

    class Sintaxis : Lexico
    {
        private Dictionary<string, int[]> typetokens = new Dictionary<string, int[]>();
        public Queue<Tokens> listasyntaxErrores = new Queue<Tokens>();

        public void StartSyntax()
        {
            AddRules(typetokens);
        }
        private void AddRules(Dictionary<string, int[]> typetokens)
        {
            typetokens.Add("<variabletype>", new int[] { -54, -57, -56, -59, -55, -60, -58 }); // int | double | string | char | bool | var | char | float
            typetokens.Add("<arisymbol>", new int[] { -6, -7, -8, -9, -15, -16, -11, -12, -14, -13, -10 });  // + | - | * | / | ++ | -- |+= | -= | /= | += | %
            typetokens.Add("<boolvalue>", new int[] { -54, -57, -56, -59, -55, -60, -58 });  // true false
            typetokens.Add("<logicop>", new int[] { -54, -57, -56, -59, -55, -60, -58 });    // < | >= | <=  |  ||  | && | == | != || !
            typetokens.Add("<accesstype>", new int[] { -54, -57, -56, -59, -55, -60, -58 }); // public | private | protected | sealed | partial 

        }
        public void AnalizadorSintactico()
        {

            foreach (Tokens item in tokensGenerados)
            {
                int currenttoken = item.Valor;

                switch (item.Valor)
                {
                    case -1: //variabletype
                        ID(currenttoken);
                        continue;
                    case 2: //accesstype
                        continue;
                    case 3: //Namespace
                        continue;
                    case 4: //Class
                        continue;
                    case 5: //Librerias
                        continue;
                    case 6: //Librerias
                        continue;
                    default: //Tronasion
                        Tokens tempError = new Tokens("ERROR", item.Lexema, -600, item.Linea); //ERROR: que vergas es esto
                        listasyntaxErrores.Enqueue(tempError);
                        break;
                }
            }
        }

        #region ReglasTokens
        private int ID(int token) //Nombre de variable
        {
            if(token != -1) token = -601; //Si no es identificador, tronasion
            return token;
        }
        private void Value() //Valor de variables
        {

        }
        private void Block(int token) //Bloques {}
        {
            token = 17; //{;
            do
            {
                switch (token)
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

            } while (!(token == 18)); //cierre de bloque

        }

        private void VariableType()
        {

        }
        private void AccessType()
        {

        }
        private void ArytmeticSymbol()
        {

        }
        private void BoolValue()
        {

        }
        private void LogicOperando()
        {

        }

        private void Statement()
        {

        }
        private void Parameters()
        {

        }

        private void Libraries()
        {
            //Using
            //Va por siguiente token
            //Funcion Identificador
            //No c
        }
        #endregion
    }
}
