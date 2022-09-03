﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class MatrizTransicion
    {
        public List<string> estados = new List<string>();
        public List<char> alfabeto = new List<char>();
        
        #region MatrizdeTransición
        public string[,] matrizTr = {
           //   0    1     2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20  21   22   23   24   25   26   27   28   29   30   31     32     33   34
           // a-z   0-9    +    -    *    /    %    <    >    =    !    &    |    ?    ^    (    )    {    }    [    ]   ;    :    .    ,    "    '    _    \    @    #    SPA    /t    /n    DESC
      /*q0*/{"q1", "q3", "q16", "q19", "q22", "q12", "q25", "q26", "q27", "q28", "q32", "q34", "q36", "q38", "q40", "q41", "q42", "q43", "q44", "q45", "q46", "q47", "q48", "q49", "q50", "q6", "q9", "-90", "-91", "q51", "q13", "q0", "q0", "q0", "-500"},
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
     /*q51*/{"-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-92","-500"},
        };
        #endregion

        public void Cargar()
        {
            AddAlfabeto();
            AddEstado();
        }


        private void AddAlfabeto() //No sé si sea lo correcto pero ok ._.XD
        {
            alfabeto.Add('+'); alfabeto.Add('-'); alfabeto.Add('*'); alfabeto.Add('/'); alfabeto.Add('%');
            alfabeto.Add('<'); alfabeto.Add('>');alfabeto.Add('='); alfabeto.Add('!'); alfabeto.Add('&');
            alfabeto.Add('|'); alfabeto.Add('?'); alfabeto.Add('^'); 

            alfabeto.Add('('); alfabeto.Add(')'); alfabeto.Add('{'); alfabeto.Add('}'); alfabeto.Add('['); alfabeto.Add(']');

            alfabeto.Add(';'); alfabeto.Add(':'); alfabeto.Add('.'); alfabeto.Add(','); alfabeto.Add('"'); 
            alfabeto.Add('\''); alfabeto.Add('_'); alfabeto.Add('\\');

            alfabeto.Add('@'); alfabeto.Add('#'); alfabeto.Add(' '); alfabeto.Add('\t'); alfabeto.Add('\n');
        }
        private void AddEstado()
        {
            string estado;
            int estadonum;

            for (int i = 0; i <= 51; i++)
            {
                estadonum = i;
                estado = "q" + estadonum.ToString();
                estados.Add(estado);
            }
        }
        private void AddTransicion(string transicion, int estado, int alfabeto)
        {
            matrizTr[estado, alfabeto] = transicion;
        }
    }
}
