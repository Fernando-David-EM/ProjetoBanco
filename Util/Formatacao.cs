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
        public static string TransformaDinheiroEmReais(double numero)
        {
            return numero.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static double RemoveSimboloDeDinheiro(string numero)
        {
            return Convert.ToDouble(numero.Replace("R$ ", "").Replace(".", ","));
        }
        public static string RemoveTudoMenosNumeros(string numero)
        {   
            return Regex.Replace(numero, "[^0-9]", string.Empty);
        }

        public static string RemoveTudoMenosLetras(string texto)
        {
            return Regex.Replace(texto, "[^a-z,A-Z]", string.Empty);
        }
    }
}
