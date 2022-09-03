using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Lexico : MatrizTransicion
    {
        public Queue<Tokens> tokensGenerados = new Queue<Tokens>();
        public Queue<Tokens> listaErrores = new Queue<Tokens>();
        private Dictionary<int, string> tokens = new Dictionary<int, string>();
        private Dictionary<int, string> reservadas = new Dictionary<int, string>();
        private Dictionary<int, string> error = new Dictionary<int, string>();

        public void Inicializar()
        {
            Add(tokens, reservadas);
            Cargar();
        }
        private void Add(Dictionary<int, string> tokens, Dictionary<int, string> reservadas)
        {
            tokens.Add(-1, "Identificador");
            tokens.Add(-2, "Entero");
            tokens.Add(-3, "Decimal");
            tokens.Add(-4, "Cadena");
            tokens.Add(-5, "Caracter");

            //Aritmético
            tokens.Add(-6, "+");
            tokens.Add(-7, "-");
            tokens.Add(-8, "*");
            tokens.Add(-9, "/");
            tokens.Add(-10, "%");

            //Asignativos
            tokens.Add(-11, "+=");
            tokens.Add(-12, "-=");
            tokens.Add(-13, "*=");
            tokens.Add(-14, "/=");
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
            tokens.Add(-90, "_");
            tokens.Add(-91, "\\");

            error.Add(-500, "Error");
            error.Add(-501, "Desconocido");
            error.Add(-502, "ID Invalido");
            error.Add(-503, "Se esperaba ID");
            error.Add(-504, "Se esperaba E");
            error.Add(-505, "Se esperaba D");
            error.Add(-506, "No cierra caracter");
            error.Add(-507, "No cierra cadena");

            //Clases
            reservadas.Add(-41, "class");
            reservadas.Add(-42, "extends");
            reservadas.Add(-43, "interface");
            reservadas.Add(-44, "namespace");

            //Alcance
            reservadas.Add(-45, "public");
            reservadas.Add(-46, "protected");
            reservadas.Add(-47, "internal");
            reservadas.Add(-48, "private");
            reservadas.Add(-49, "abstract");
            reservadas.Add(-50, "sealed");
            reservadas.Add(-51, "static");
            reservadas.Add(-52, "partial");
            reservadas.Add(-53, "override");

            //Tipos de Variables
            reservadas.Add(-54, "int");
            reservadas.Add(-55, "bool");
            reservadas.Add(-56, "string");
            reservadas.Add(-57, "double");
            reservadas.Add(-58, "float");
            reservadas.Add(-59, "char");
            reservadas.Add(-60, "var");
            reservadas.Add(-61, "object");
            reservadas.Add(-62, "enum");
            reservadas.Add(-63, "struct");

            //Sentencias
            reservadas.Add(-64, "args");
            reservadas.Add(-65, "break");
            reservadas.Add(-66, "case");
            reservadas.Add(-67, "catch");
            reservadas.Add(-68, "continue");
            reservadas.Add(-69, "do");
            reservadas.Add(-70, "get");
            reservadas.Add(-71, "goto");
            reservadas.Add(-72, "else");
            reservadas.Add(-73, "for");
            reservadas.Add(-74, "foreach");
            reservadas.Add(-75, "if");
            reservadas.Add(-76, "in");
            reservadas.Add(-77, "join");
            reservadas.Add(-78, "new");
            reservadas.Add(-79, "null");
            reservadas.Add(-80, "return");
            reservadas.Add(-81, "set");
            reservadas.Add(-82, "switch");
            reservadas.Add(-83, "this");
            reservadas.Add(-84, "throw");
            reservadas.Add(-85, "try");
            reservadas.Add(-86, "using");
            reservadas.Add(-87, "void");
            reservadas.Add(-88, "where");
            reservadas.Add(-89, "while");
        }

        public void Analizar(string codigo)
        {
            string tempPalabra = string.Empty;
            string tempEstado;

            string estadoActual = "q0";
            int lineaCodigo = 1;
            int columna;
            bool esCadena = false;
            bool inError = false;

            for (int indice = 0; indice < codigo.Length; indice++)
            {
                char caracterActual = siguienteCaracter(codigo, indice);

                if (caracterActual.Equals('\n')){inError = false; esCadena = false;}

                if (estados.IndexOf(estadoActual) < 0)
                {
                    if (Int32.Parse(estadoActual) <= -500) //Detección de Errores
                    {
                        if (inError == true) continue;
                        AddErrorList(Int32.Parse(estadoActual), tempPalabra += caracterActual, lineaCodigo);
                        estadoActual = "q0";
                        inError = true;
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
                tempPalabra += caracterActual;
            }
        }

        public void analizarTexto(string codigo)
        {
            string tempPalabra = string.Empty;
            string tempEstado;

            string estadoActual = "q0";
            int lineaCodigo = 1;
            bool escomentario = false;
            bool comentariomultilinea = false;
            bool esCadena = false;
            bool inError = false; 

            for (int indice = 0; indice < codigo.Length; indice++)
            {
                char caracterActual = siguienteCaracter(codigo, indice);
                char caractersig = siguienteCaracter(codigo, indice + 1);

                if (caracterActual.Equals('\n'))
                {
                    if (comentariomultilinea == true) continue; 
                    estadoActual = BuscarToken(estadoActual, tempPalabra, -1, esCadena);
                    if (esCadena) estadoActual = "-507";
                    esCadena = false;
                    Pretoken(estadoActual, tempPalabra, lineaCodigo);
                    lineaCodigo++;
                    escomentario = false;
                    estadoActual = "q0";
                    tempPalabra = string.Empty;
                    continue;
                }
                else if (caracterActual == ' ' || caracterActual.Equals('\t'))
                {
                    if (escomentario == true || comentariomultilinea == true) continue;
                    if(esCadena == true) { tempPalabra += caracterActual; continue; }
                    estadoActual = BuscarToken(estadoActual, tempPalabra, -1, esCadena);
                    Pretoken(estadoActual, tempPalabra, lineaCodigo);
                    estadoActual = "q0";
                    tempPalabra = string.Empty;
                    continue;
                }
                
                if (alfabeto.IndexOf(caracterActual) == -1 && !esCadena) //CARACTER DESCONOCIDO
                {
                    estadoActual = "-501";
                    int temp = Int32.Parse(estadoActual);
                    AddErrorList(temp, caracterActual.ToString(), lineaCodigo);
                    tempPalabra = string.Empty;
                    continue;
                }

                #region Coso de Cadenas
                if (caracterActual.Equals('"')) {
                    esCadena = !esCadena;
                    estadoActual = "q5";
                   }
                if (!esCadena) estadoActual = transicion(estados.IndexOf(estadoActual), alfabeto.IndexOf(caracterActual));
                #endregion

                #region Condiciones para Comentarios
                if (estadoActual == "q100" || (caracterActual == '/' && caractersig == '/')) //Comentario simple
                { escomentario = true; continue; }

                if ((caracterActual == '/' && caractersig == '*') || estadoActual == "q101") //Comentario Multilinea
                { comentariomultilinea = true; continue; }

                if (caracterActual == '*' && caractersig == '/' || estadoActual == "q103") // Fin Comentario Multilinea
                { comentariomultilinea = false; estadoActual = "q0"; indice++; continue; }

                #endregion

                if (estados.IndexOf(estadoActual) < 0)
                {
                    if (Int32.Parse(estadoActual) <= -500) //Detección de Errores
                    {
                        if (inError == true) continue;
                        AddErrorList(Int32.Parse(estadoActual), tempPalabra += caracterActual, lineaCodigo);
                        estadoActual = "q0";
                        inError = true;
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
                tempPalabra += caracterActual;
            }
        }
        private string BuscarToken(string estadoActual, string tempPalabra, int key, bool esCadena)
        {
            if (!esCadena)
            {
                if (tempPalabra.StartsWith("'") && tempPalabra.EndsWith("'")) key = -5;
                else if (tempPalabra.StartsWith('"'.ToString()) && tempPalabra.EndsWith('"'.ToString())) key = -4;
                else if (tempPalabra.StartsWith("'") && !tempPalabra.EndsWith("'")) key = -506;
                else
                {
                    foreach (var token in tokens)
                    {
                        if (token.Value == tempPalabra) key = token.Key;
                    }
                }
                return key.ToString();
            }
            else
            {
                return estadoActual;
            }
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
                    else if (error.ContainsKey(temp))
                    {
                        AddErrorList(temp, lexema, linea);
                    }
                }
            }
        }

        private void AddErrorList(int temp, string lexema, int linea)
        {
            string type = string.Empty;

            switch (temp)
            {
                case -500: type = "S. Desconocido"; break;
                case -501: type = "Inválido"; break;
                case -502: type = "Se esperaba Número"; break;
                case -503: type = "Se esperaba Decimal"; break;
                case -504: type = "Cadena no cerrada"; break;
                case -505: type = "Caracter no cerrado"; break;
                case -506: type = "Caracter inválido"; break;
                default: type = "ERROR DESCONOCICO"; break;
            }
            Tokens tempError = new Tokens(type, lexema, temp, linea);
            listaErrores.Enqueue(tempError);
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
                case -10:type = "Aritmético"; break;

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
        }

        private int EncontrarReservada(string lexema, int key)
        {
            foreach (var palabrareservada in reservadas)
            {
                if (palabrareservada.Value == lexema) key = palabrareservada.Key;
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
            try {return Convert.ToChar(texto.Substring(indice, 1)); }
            catch (ArgumentOutOfRangeException) {return texto[texto.Length - 1]; }
        }
    }
}
