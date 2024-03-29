﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Lexico
    {
        public Queue<Tokens> tokensGenerados = new Queue<Tokens>();
        public LinkedList<Tokens> listaTokens = new LinkedList<Tokens>();
        public Queue<Tokens> listaErrores = new Queue<Tokens>();
        private Dictionary<int, string> tokens = new Dictionary<int, string>();
        private Dictionary<int, string> reservadas = new Dictionary<int, string>();
        private Dictionary<int, char> alfabetoindex = new Dictionary<int, char>(); //Detección de columnas

        public List<string> ErrorL = new List<string>();
        public bool hasLexicErrors = false;

        public List<string> estados = new List<string>();

        public string[,] matrizTr = {
           //   0    1     2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20  21   22   23   24   25   26   27   28   29   30   31     32     33   34
           // a-z   0-9    +    -    *    /    %    <    >    =    !    &    |    ?    ^    (    )    {    }    [    ]   ;    :    .    ,    "    '    _    \    @    #    SPA    \t    \n    DESC
      /*q0*/{"q1", "q3", "q16", "q19", "q22", "q12", "q25", "q26", "q27", "q28", "q32", "q34", "q36", "q38", "q40", "q41", "q42", "q43", "q44", "q45", "q46", "q47", "q48", "q49", "q50", "q6", "q9", "-92", "-93", "q51", "q13", "q0", "q0", "q0", "-500"},
      /*q1*/{"q1","q2","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","q1","-501","-1","-1","-1","-1","-1","-500"},
      /*q2*/{"-501","q2","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-501","-501","-1","-1","-1","-1","-1","-500"},
      /*q3*/{"-502","q3","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","q4","-2","-2","-2","-2","-2","-2","-2","-2","-2","-2","-500"},
      /*q4*/{"-503","q5","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-503","-500"},
      /*q5*/{"-503","q5","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-3","-503","-503","-3","-3","-503","-503","-503","-503","-3","-3","-3","-500"},
      /*q6*/{"q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q8","q7","q7","q7","q7","q7","q7","q7","-504","q7"},
      /*q7*/{"q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q7","q8","q7","q7","q7","q7","q7","q7","q7","-504","q7"},
      /*q8*/{"-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-4","-500"},
      /*q9*/{"q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","q10","-506","-505","q10"},
     /*q10*/{"-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","-506","q11","-506","-506","-506","-506","-506","-506","-505","-506"},
     /*q11*/{"-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-5","-500"},
     /*q12*/{"-9","-9","-501","-501","q14","q13","-501","-501","-501","q24","-501","-501","-501","-501","-501","-9","-9","-9","-9","-9","-9","-9","-9","-9","-9","-9","-9","-501","-501","-501","-9","-9","-9","-9","-500"},
     /*q13*/{"q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q13","q0","q13"},
     /*q14*/{"q14","q14","q14","q14","q15","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q0","q14","q14","q14","q14"},
     /*q15*/{"q14","q14","q14","q14","q14","q0","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q14","q0","q14","q14","q14","q14"},
     /*q16*/{"-6","-6","q17","-6","-6","-6","-6","-6","-6","q18","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6","-6"},
     /*q17*/{"-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15","-15"},
     /*q18*/{"-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11","-11"},
     /*q19*/{"-7","-7","-7","q20","-7","-7","-7","-7","-7","q21","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-7","-500"},
     /*q20*/{"-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-500"},
     /*q21*/{"-12","-12","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-16","-500"},
     /*q22*/{"-8","-8","-8","-8","-8","-8","-8","-8","-8","q23","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-8","-500"},
     /*q23*/{"-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-13","-500"},
     /*q24*/{"-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-14","-500"},
     /*q25*/{"-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-10","-500"},
     /*q26*/{"-17","-17","-17","-17","-17","-17","-17","-17","-17","q30","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-17","-500"},
     /*q27*/{"-18","-18","-18","-18","-18","-18","-18","-18","-18","q29","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-18","-500"},
     /*q28*/{"-19","-19","-19","-19","-19","-19","-19","-19","-19","q31","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-19","-500"},
     /*q29*/{"-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-20","-500"},
     /*q30*/{"-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-21","-500"},
     /*q31*/{"-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-22","-500"},
     /*q32*/{"-23","-23","-23","-23","-23","-23","-23","-23","-23","q33","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-23","-500"},
     /*q33*/{"-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-24","-500"},
     /*q34*/{"-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","q35","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-25","-500"},
     /*q35*/{"-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-26","-500"},
     /*q36*/{"-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","q37","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-27","-500"},
     /*q37*/{"-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-28","-500"},
     /*q38*/{"-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","q39","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-501","-506","-506","-506","-501","-501","-501","-501","-500"},
     /*q39*/{"-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-29","-500"},
     /*q40*/{"-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-30","-500"},
     /*q41*/{"-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-31","-500"},
     /*q42*/{"-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-32","-500"},
     /*q43*/{"-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-33","-500"},
     /*q44*/{"-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-34","-500"},
     /*q45*/{"-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-35","-500"},
     /*q46*/{"-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-36","-500"},
     /*q47*/{"-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-37","-500"},
     /*q48*/{"-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-38","-500"},
     /*q49*/{"-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-39","-500"},
     /*q50*/{"-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-40","-500"},
     /*q51*/{"-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-94","-500"},
        };

        public Lexico()
        {
            string estado;
            int estadonum;

            for (int i = 0; i <= 51; i++)
            {
                estadonum = i;
                estado = "q" + estadonum.ToString();
                estados.Add(estado);
            }

            alfabetoindex.Add(2, '+');
            alfabetoindex.Add(3, '-');
            alfabetoindex.Add(4, '*');
            alfabetoindex.Add(5, '/');
            alfabetoindex.Add(6, '%');
            alfabetoindex.Add(7, '<');
            alfabetoindex.Add(8, '>');
            alfabetoindex.Add(9, '=');
            alfabetoindex.Add(10, '!');
            alfabetoindex.Add(11, '&');
            alfabetoindex.Add(12, '|');
            alfabetoindex.Add(13, '?');
            alfabetoindex.Add(14, '^');
            alfabetoindex.Add(15, '(');
            alfabetoindex.Add(16, ')');
            alfabetoindex.Add(17, '{');
            alfabetoindex.Add(18, '}');
            alfabetoindex.Add(19, '[');
            alfabetoindex.Add(20, ']');
            alfabetoindex.Add(21, ';');
            alfabetoindex.Add(22, ':');
            alfabetoindex.Add(23, '.');
            alfabetoindex.Add(24, ',');
            alfabetoindex.Add(25, '"');
            alfabetoindex.Add(26, '\'');
            alfabetoindex.Add(27, '_');
            alfabetoindex.Add(28, '\\');
            alfabetoindex.Add(29, '@');
            alfabetoindex.Add(30, '#');
            alfabetoindex.Add(31, ' ');
            alfabetoindex.Add(32, '\t');
            alfabetoindex.Add(33, '\n');
            tokens.Add(-1, "Identificador"); tokens.Add(-2, "Entero"); tokens.Add(-3, "Decimal");
            tokens.Add(-4, "Cadena"); tokens.Add(-5, "Caracter");

            //Aritmético
            tokens.Add(-6, "+");
            tokens.Add(-7, "-");
            tokens.Add(-8, "*");
            tokens.Add(-9, "/");
            tokens.Add(-10, "%");

            //Asignativos
            tokens.Add(-15, "++");
            tokens.Add(-16, "--");

            //Relacional
            tokens.Add(-17, "<");
            tokens.Add(-18, ">");
            tokens.Add(-19, "=");
            tokens.Add(-20, ">=");
            tokens.Add(-21, "<=");
            tokens.Add(-22, "==");
            tokens.Add(-23, "!");
            tokens.Add(-24, "!=");

            //Logico
            tokens.Add(-25, "&");
            tokens.Add(-26, "&&");
            tokens.Add(-27, "|");
            tokens.Add(-28, "||");
            tokens.Add(-29, "??");
            tokens.Add(-30, "^");

            //Simbolos
            tokens.Add(-31, "(");
            tokens.Add(-32, ")");
            tokens.Add(-33, "{");
            tokens.Add(-34, "}");
            tokens.Add(-35, "[");
            tokens.Add(-36, "]");
            tokens.Add(-37, ";");
            tokens.Add(-38, ":");
            tokens.Add(-39, ".");
            tokens.Add(-40, ",");
            tokens.Add(-92, "_");
            tokens.Add(-93, "\\");
            tokens.Add(-94, "@");

            //Clases
            reservadas.Add(-41, "class");
            reservadas.Add(-43, "interface");
            reservadas.Add(-44, "namespace");

            //Alcance
            reservadas.Add(-45, "public");
            reservadas.Add(-48, "private");
            reservadas.Add(-51, "static");
            reservadas.Add(-52, "partial");
            reservadas.Add(-53, "override");

            //Tipos de Variables
            reservadas.Add(-54, "int");
            reservadas.Add(-55, "bool");
            reservadas.Add(-56, "string");
            reservadas.Add(-57, "double");

            //Sentencias
            reservadas.Add(-64, "args");
            reservadas.Add(-65, "break");
            reservadas.Add(-69, "do");
            reservadas.Add(-72, "else");
            reservadas.Add(-73, "for");
            reservadas.Add(-74, "foreach");
            reservadas.Add(-75, "if");
            reservadas.Add(-78, "new");
            reservadas.Add(-79, "null");
            reservadas.Add(-80, "return");
            reservadas.Add(-81, "set");
            reservadas.Add(-86, "using");
            reservadas.Add(-87, "void");
            reservadas.Add(-89, "while");
            reservadas.Add(-90, "true");
            reservadas.Add(-91, "false");


        }

        public void AnalisisLexico(string codigo)
        {
            string tempPalabra = string.Empty;

            string estadoActual = "q0";
            int lineaCodigo = 1;

            for (int indice = 0; indice < codigo.Length; indice++)
            {
                bool comentariol = false, comentariovl = false;
                char caracterActual = siguienteCaracter(codigo, indice);
                estadoActual = transicion(estados.IndexOf(estadoActual), ColumnaAlfabeto(caracterActual, alfabetoindex));

                if (estadoActual == "q13") { comentariol = true; tempPalabra = ""; }
                if (estadoActual == "q14" || estadoActual == "q15") { comentariovl = true; tempPalabra = ""; }
                if (estadoActual == "q0" && comentariovl) { tempPalabra = ""; comentariovl = false; continue; }
                if (comentariol || comentariovl) continue;

                if (estados.IndexOf(estadoActual) < 0)
                {
                    if (Int32.Parse(estadoActual) <= -500)
                    {
                        AddErrorList(Int32.Parse(estadoActual), tempPalabra += caracterActual, lineaCodigo);
                        estadoActual = "q0";
                        tempPalabra = string.Empty;
                        continue;
                    }
                    else
                    {
                        Pretoken(estadoActual, tempPalabra, lineaCodigo);
                        estadoActual = "q0";
                        tempPalabra = string.Empty;
                        indice--;
                    }
                    continue;
                }
                if (caracterActual.Equals('\n')) { lineaCodigo++; }
                if (estadoActual != "q0") tempPalabra += caracterActual;
            }
        }

        private int ColumnaAlfabeto(char caracterActual, Dictionary<int, char> alfabeto)
        {
            int columna = 0;

            if (char.IsLetter(caracterActual))
            {
                columna = 0;
            }
            else if (char.IsDigit(caracterActual))
            {
                columna = 1;
            }
            else if (alfabetoindex.ContainsValue(caracterActual))
            {
                foreach (var simbolo in alfabeto)
                { if (simbolo.Value == caracterActual) { columna = simbolo.Key; break; } }
            } else columna = 34;

            return columna;
        }
        private void Pretoken(string estadoActual, string lexema, int linea)
        {
            if (!string.IsNullOrEmpty(lexema))
            {
                if (estadoActual.StartsWith("q"))
                {
                    return;
                }
                else
                {
                    int temp = Int32.Parse(estadoActual);
                    if (tokens.ContainsKey(temp))
                    {
                        AddTokenList(temp, lexema, linea);
                    }
                    else if (temp <= -500)
                    {
                        AddErrorList(temp, lexema, linea);
                    }
                }
            }
        }

        private void AddErrorList(int temp, string lexema, int linea)
        {
            string type;
            switch (temp)
            {
                case -500: type = "Símbolo Desconocido"; break;
                case -501: type = "Inválido"; break;
                case -502: type = "Se esperaba Número"; break;
                case -503: type = "Se esperaba Decimal"; break;
                case -504: type = "Cadena no cerrada"; break;
                case -505: type = "Caracter no cerrado"; break;
                case -506: type = "Caracter inválido"; break;
                default: type = "ERROR DESCONOCICO"; break;
            }

            ErrorL.Add("Linea " + linea.ToString() + " ERROR " + temp.ToString() + ": " + type);
            hasLexicErrors = true;
        }

        private void AddTokenList(int temp, string lexema, int linea)
        {
            string type = string.Empty;

            switch (temp) {
                case -1:
                    if (reservadas.ContainsValue(lexema))
                    {
                        temp = EncontrarReservada(lexema, temp);
                        type = "Reservada";
                    } else type = "Identificador"; break;

                case -2: type = tokens[temp]; break;
                case -3: type = tokens[temp]; break;
                case -4: type = tokens[temp]; break;
                case -5: type = tokens[temp]; break;

                case -6: type = "Aritmético"; break;
                case -7: type = "Aritmético"; break;
                case -8: type = "Aritmético"; break;
                case -9: type = "Aritmético"; break;
                case -10: type = "Aritmético"; break;

                case -11: type = "Asignativo"; break;
                case -12: type = "Asignativo"; break;
                case -13: type = "Asignativo"; break;
                case -14: type = "Asignativo"; break;
                case -15: type = "Asignativo"; break;
                case -16: type = "Asignativo"; break;

                case -17: type = "Relacional"; break;
                case -18: type = "Relacional"; break;
                case -19: type = "Relacional"; break;
                case -20: type = "Relacional"; break;
                case -21: type = "Relacional"; break;
                case -22: type = "Relacional"; break;

                case -23: type = "Lógico"; break;
                case -24: type = "Lógico"; break;
                case -25: type = "Lógico"; break;
                case -26: type = "Lógico"; break;
                case -27: type = "Lógico"; break;
                case -28: type = "Lógico"; break;
                case -29: type = "Lógico"; break;
                case -30: type = "Lógico"; break;

                case -31: type = "Simbolo"; break;
                case -32: type = "Simbolo"; break;
                case -33: type = "Simbolo"; break;
                case -34: type = "Simbolo"; break;
                case -35: type = "Simbolo"; break;
                case -36: type = "Simbolo"; break;
                case -37: type = "Simbolo"; break;
                case -38: type = "Simbolo"; break;
                case -39: type = "Simbolo"; break;
                case -40: type = "Simbolo"; break;
                case -90: type = "Simbolo"; break;
                case -91: type = "Simbolo"; break;
                case -92: type = "Simbolo"; break;

            }
            Tokens tempToken = new Tokens(type, lexema, temp, linea);
            tokensGenerados.Enqueue(tempToken);
            listaTokens.AddLast(tempToken);
        }

        private int EncontrarReservada(string lexema, int key)
        {
            foreach (var palabrareservada in reservadas)
            {
                if (palabrareservada.Value == lexema)
                {
                    key = palabrareservada.Key;
                    break;
                }
            }
            return key;
        }

        private string transicion(int Estado, int Alfabeto_)
        {
            if (Estado == -1) Estado = 0;
            return matrizTr[Estado, Alfabeto_];
        }

        private char siguienteCaracter(string texto, int indice)
        {
            try { return Convert.ToChar(texto.Substring(indice, 1)); }
            catch (ArgumentOutOfRangeException) { return texto[texto.Length - 1]; }
        }
    }
}

