using Dynamitey;
using System;
using System.Text.RegularExpressions;

namespace Banco.Util
{
    /// <summary>
    /// Realiza a validação do CPF
    /// </summary>
    public static class CPF
    {
        public static string GeraCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente += resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente += resto;

            var chars = semente.ToCharArray();
            char[] cpf = new char[14];

            int k = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 3 || i == 6)
                {
                    cpf[k] = '.';

                    k++;
                }
                if (i == 9)
                {
                    cpf[k] = '-';

                    k++;
                }

                cpf[k] = chars[i];

                k++;
            }

            return string.Concat(cpf);
        }

        public static bool EhCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;
            else
            {
                int[] d = new int[11];
                int[] v = new int[2];
                int j, i, soma;
                string SoNumero;

                SoNumero = Regex.Replace(cpf, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }
                // CPF inválido se
                // a quantidade de dígitos numérios for diferente de 11
                else return false;
            }
        }
    }
}