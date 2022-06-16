using ExcelDataReader;
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

        public string[,] matrizTr;

        public void cargarMatriz(IExcelDataReader excelreader)
        {
            estados = new List<string>();
            alfabeto = new List<char>();

            int fila = 0, columna;
            while (excelreader.Read())
            {
                if (fila == 0) //Fila con alfabeto
                {
                    for (columna = 1; columna < excelreader.FieldCount; columna++)
                    {
                        if (excelreader.GetValue(columna) != null)
                        {
                            string caracter = excelreader.GetValue(columna).ToString();
                            AddAlfabeto(Char.Parse(caracter));
                        }
                    }
                }
                if (fila > 0)
                {
                    if (excelreader.GetValue(0) != null)
                    {
                        string estado = excelreader.GetValue(0).ToString();
                        AddEstado(estado);
                    }
                }
                fila++;
            }

            fila = 0;
            matrizTr = new string[estados.Count, alfabeto.Count];
            excelreader.Reset();

            do
            {
                while (excelreader.Read())
                {
                    if (fila > 0)
                    {
                        for (columna = 1; columna < excelreader.FieldCount; columna++)
                        {
                            if (excelreader.GetValue(columna) != null)
                            {
                                AddTransicion(excelreader.GetValue(columna).ToString(), fila - 1, columna - 1);
                            }
                        }
                    }
                    fila++;
                }

            } while (excelreader.NextResult());
        }

        private void AddAlfabeto(char caracter)
        {
            if (!alfabeto.Contains(caracter))
            {
                alfabeto.Add(caracter);
            }
        }
        private void AddEstado(string estado)
        {
            if (!estados.Contains(estado))
            {
                estados.Add(estado);
            }
        }
        private void AddTransicion(string transicion, int estado, int alfabeto)
        {
            matrizTr[estado, alfabeto] = transicion;
        }
    }
}
