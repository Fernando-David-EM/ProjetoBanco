using Banco.Exceptions;
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

        public Conta(List<object> propriedades) : base(propriedades)
        {
            
        }

        protected override void InserePropriedades(List<object> propriedades)
        {
            if (propriedades.Count > 5)
            {
                Id = Convert.ToInt32(propriedades.First());
                propriedades.RemoveAt(0);
            }
            Nome = Convert.ToString(propriedades[0]);
            Telefone = Convert.ToString(propriedades[1]);
            Cpf = Convert.ToString(propriedades[2]);
            Saldo = Convert.ToDouble(propriedades[3]);
            Limite = Convert.ToDouble(propriedades[4]);
        }

        public override string[] RecebePropriedades()
        {
            return new string[]
            {
                Nome,
                Telefone,
                Cpf,
                Formatacao.TransformaDinheiroEmReais(Saldo),
                Formatacao.TransformaDinheiroEmReais(Limite)
            };
        }

        public override string RecebeNomeDasColunasDaTabelaParaSql()
        {
            return "(con_nome,con_telefone,con_cpf,con_saldo,con_limite)";
        }

        public override string RecebeValorDasPropriedadesParaSql()
        {
            return $"(\'{Nome}\',\'{Telefone}\',\'{Cpf}\',{Saldo},{Limite})";
        }

        public override string RecebePropriedadeDeValidacao()
        {
            return Cpf;
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