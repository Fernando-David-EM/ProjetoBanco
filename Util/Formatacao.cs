using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Util
{
    class Formatacao
    {
        public static string DoubleEmReais(double numero)
        {
            return numero.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")); //Type conversion
        }

        public static string RemovePontos(string texto)
        {
            return texto.Replace(".", "").Replace("-", "");
        }

        public static string CpfComPontos(string cpf)
        {
            var textos = cpf.ToCharArray();

            char[] result = new char[14];
            int j = 0;

            for (int i = 0; j < textos.Length; i++)
            {
                if (i == 3 || i == 7 || i == 11)
                {
                    i++;
                }

                result[i] = textos[j];

                j++;
            }

            result[3] = '.';
            result[7] = '.';
            result[11] = '-';

            string final = "";

            foreach (var c in result)
            {
                final += c;
            }

            return final;
        }

        public static string TelefoneComPontos(string telefone)
        {
            char[] result;

            if (telefone.Length == 8)
            {
                result = new char[9];
                var chars = telefone.ToCharArray();

                int j = 0;

                for (int i = 0; j < chars.Length; i++)
                {
                    if (i == 4)
                    {
                        result[i] = '-';
                        i++;
                    }

                    result[i] = chars[j];

                    j++;
                }
            }
            else
            {
                result = new char[10];
                var chars = telefone.ToCharArray();

                int j = 0;

                for (int i = 0; j < chars.Length; i++)
                {
                    if (i == 5)
                    {
                        result[i] = '-';
                        i++;
                    }

                    result[i] = chars[j];

                    j++;
                }
            }

            string final = "";

            foreach (var c in result)
            {
                final += c;
            }

            return final; ;
        }
    }
}
