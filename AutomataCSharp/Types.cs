using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataCSharp
{
    class Types
    {
        string CT = string.Empty; //Comparacion Tipos
        string tipo = string.Empty;
        #region TabladeTipos
        private string[,] sum =
        {   //+         int         double      string
            /*int*/     {"int",     "double",   "string" },
            /*double*/  {"double",  "double",   "string" },
            /*string*/  {"string",  "string",   "string" },
        };

        private string[,] res =
        {   //-         int         double      string
            /*int*/     {"int",     "double",   "E" },
            /*double*/  {"double",  "double",   "E" },
            /*string*/  {"E",       "E",        "E" }
        };

        private string[,] mul =
        {   //*         int         double      string
            /*int*/     {"int",     "double",   "E" },
            /*double*/  {"double",  "double",   "E" },
            /*string*/  {"E",       "E",        "E" }
        };

        private string[,] div =
        {   ///         int         double      string
            /*int*/     {"double",  "double",   "E" },
            /*double*/  {"double",  "double",   "E" },
            /*string*/  {"E",       "E",        "E" }
        };

        private string[,] than =
        {   //< >       int       double        string
            /*int*/     {"bool",  "bool",       "E" },
            /*double*/  {"bool",  "bool",       "E" },
            /*string*/  {"E",     "E",          "E" }
        };

        private string[,] equals =
        {   //!= ==     int       double        string
            /*int*/     {"bool",  "bool",       "E" },
            /*double*/  {"bool",  "bool",       "E" },
            /*string*/  {"E",     "E",          "bool" }
        };

        private string[,] final = //se usará para asignaciones al final, tipo int x = "hola >>> ERROR
        {   //=         int         double      string
            /*int*/     {"int",     "E",        "E" },
            /*double*/  {"double",  "double",   "E" },
            /*string*/  {"E",       "E",        "string"}
        };

        #endregion

        public string Tipo //Se ocupará en la evaluación del posfijo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public bool EvaluarTipos(Tokens operador, string type1, string type2)
        {
            //Tomará una tabla de sistema de tipos dependiendo del operador
            switch (operador.Valor)
            {
                case -6: //suma
                    GetEstado(sum, type1, type2);
                    break;
                case -7: //resta
                    GetEstado(res, type1, type2);
                    break;
                case -8: //multiplicacion
                    GetEstado(mul, type1, type2);
                    break;
                case -9: //division
                    GetEstado(div, type1, type2);
                    break;

                case -17: //<
                    GetEstado(than, type1, type2);
                    break;
                case -18: //>
                    GetEstado(than, type1, type2);
                    break;
                case -20: //>=
                    GetEstado(than, type1, type2);
                    break;
                case -21: // <=
                    GetEstado(than, type1, type2);
                    break;
                case -22: // ==
                    GetEstado(equals, type1, type2);
                    break;
                case -24: //!=
                    GetEstado(equals, type1, type2);
                    break;

                default: // =
                    GetEstado(final, type1, type2);
                    break;
            }

            if (this.tipo == "E") return false;
            else return true;
        }

        public void GetEstado(string[,] TablaTipos, string type1, string type2)
        {
            int line = 0;
            int column = 0;

            switch (type1)
            {
                case "int": line = 0; break;
                case "double": line = 1; break;
                case "string": line = 2; break;
            }

            switch (type2)
            {
                case "int": column = 0; break;
                case "double": column = 1; break;
                case "string": column = 2; break;
            }

            string estado = TablaTipos[line, column];
            this.tipo = estado;
        }
    }
}
