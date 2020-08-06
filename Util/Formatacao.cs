using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banco.Util
{
    class Formatacao
    {
        public static string DoubleEmReais(double numero)
        {
            return numero.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")); //Type conversion
        }

        public static double RemoveReais(string numero)
        {
            return Convert.ToDouble(Regex.Replace(numero, "[^0-9]", string.Empty));
        }
        public static string SobraApenasNumeros(string numero)
        {   
            return Regex.Replace(numero, "[^0-9]", string.Empty);
        }

        public static string RemoveSimbolos(string texto)
        {
            return Regex.Replace(texto, "[^a-z,A-Z]", string.Empty);
        }
    }
}
