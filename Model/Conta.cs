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
                Id = (int)campos[0],
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
            return $"(\'{Nome}\',\'{Telefone}\',\'{Cpf}\',{Saldo},{Limite})";
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

        public override string GetPropriedadeDeValidacao()
        {
            return Cpf;
        }

        private List<string> SplitAndRemoveParenthesis(string text)
        {
            return 
                text
                .Split(',')
                .Select(x => x.Trim('(', ')'))
                .ToList();
        }

        public override int GetHashCode()
        {
            int hashCode = -1220008800;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Telefone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cpf);
            hashCode = hashCode * -1521134295 + Saldo.GetHashCode();
            hashCode = hashCode * -1521134295 + Limite.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Conta conta &&
                   Id == conta.Id &&
                   Nome == conta.Nome &&
                   Telefone == conta.Telefone &&
                   Cpf == conta.Cpf &&
                   Saldo == conta.Saldo &&
                   Limite == conta.Limite;
        }
    }
}