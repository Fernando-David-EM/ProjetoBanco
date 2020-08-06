using Banco.Util;
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
            

            if (campos.Length == 6)
            {
                return new Conta
                {
                    Id = Convert.ToInt32(campos[0]),
                    Nome = Convert.ToString(campos[1]),
                    Telefone = Convert.ToString(campos[2]),
                    Cpf = Convert.ToString(campos[3]),
                    Saldo = Convert.ToDouble(campos[4]),
                    Limite = Convert.ToDouble(campos[5])
                };
            }
            else
            {
                return new Conta
                {
                    Nome = Convert.ToString(campos[0]),
                    Telefone = Convert.ToString(campos[1]),
                    Cpf = Convert.ToString(campos[2]),
                    Saldo = Convert.ToDouble(campos[3]),
                    Limite = Convert.ToDouble(campos[4])
                };
            }
        }

        public override string[] GetProperties()
        {
            return new string[]
            {
                Nome,
                Telefone,
                Cpf,
                Formatacao.DoubleEmReais(Saldo),
                Formatacao.DoubleEmReais(Limite)
            };
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

            string final = "";

            for (int i = 0; i < names.Count; i++)
            {
                final += $"{names[i]}={values[i]},";
            }

            final = final.Trim(',');

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