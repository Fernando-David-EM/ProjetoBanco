using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Model
{
    class Conta : BaseModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public double Saldo { get; set; }
        public double Limite { get; set; }

        public Conta()
        {

        }

        public Conta(string nome, string telefone, string cpf, double saldo, double limite)
        {
            Nome = nome;
            Telefone = telefone;
            Cpf = cpf;
            Saldo = saldo;
            Limite = limite;
        }

        public override BaseModel SetPropertiesFromObjectArray(object[] campos)
        {
            Conta conta = new Conta
            {
                Nome = (string)campos[1],
                Telefone = (string)campos[2],
                Cpf = (string)campos[3],
                Saldo = (double)campos[4],
                Limite = (double)campos[5]
            };

            return conta;
        }

        public override string GetNameOfTableColumns()
        {
            return "(con_nome,con_telefone,con_cpf,con_saldo,con_limite)";
        }

        public override string GetValueOfTableProperties()
        {
            return $"(\"{Nome}\",\"{Telefone}\",\"{Cpf}\",{Saldo},{Limite})";
        }

        public override string GetColumnEqualsValue()
        {
            var names = SplitAndRemoveParenthesis(GetNameOfTableColumns());

            var values = SplitAndRemoveParenthesis(GetValueOfTableProperties());

            string final = "(";

            for (int i = 0; i < names.Count; i++)
            {
                final += $"{names[i]}={values[i]}";
            }

            final += ")";

            return final;
        }

        private List<string> SplitAndRemoveParenthesis(string text)
        {
            return 
                text
                .Split(',')
                .Select(x => x.Trim('(', ')'))
                .ToList();
        }

        public override bool Equals(object obj)
        {
            var comparado = obj as Conta;

            if (comparado.Nome == Nome &&
                comparado.Telefone == Telefone &&
                comparado.Cpf == Cpf &&
                comparado.Id == Id &&
                comparado.Saldo == Saldo &&
                comparado.Limite == Limite)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}