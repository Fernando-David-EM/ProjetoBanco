using Banco.DAL;
using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using Banco.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco.View
{
    public partial class TelaContas : Form
    {
        private DaoConta _daoConta;
        private Conta _contaSelecionadaDaTabela;
        private bool _linhaDaTabelaFoiClicada = false;
        private const bool NaoDeveGerarId = false;
        private const bool DeveGerarId = true;

        public TelaContas()
        {
            InitializeComponent();
            _daoConta = new DaoConta();
            PopulaTable();
            CriaMascaras();
        }

        private void CriaMascaras()
        {
            maskedTextBoxTelefone.Mask = "(00) 00000-0000";
            maskedTextBoxCpf.Mask = "000,000,000-00";
        }

        private void PopulaTable()
        {
            dataGridView1.Rows.Clear();
            var contas = _daoConta.PesquisaTodos();

            if (contas.Count() > 0)
            {
                foreach (var conta in contas)
                {
                    dataGridView1.Rows.Add(conta.RecebePropriedades());
                }
            }
        }

        private void PopulaCampos(Conta conta)
        {
            textBoxNome.Text = conta.Nome;
            maskedTextBoxTelefone.Text = conta.Telefone;
            maskedTextBoxCpf.Text = conta.Cpf;
            textBoxSaldo.Text = conta.Saldo.ToString();
            textBoxLimite.Text = conta.Limite.ToString();
        }

        public void TesteCampos(string[] campos)
        {
            textBoxNome.Text = campos[0];
            maskedTextBoxTelefone.Text = campos[1];
            maskedTextBoxCpf.Text = campos[2];
            textBoxSaldo.Text = campos[3];
            textBoxLimite.Text = campos[4];
        }

        public void TesteCadastrar()
        {
            ValidaCampos();

            var campos = GeraListaDeCampos(NaoDeveGerarId);

            _daoConta.Insere(new Conta(campos));

            _linhaDaTabelaFoiClicada = false;

            PopulaTable();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaCampos();

                var campos = GeraListaDeCampos(NaoDeveGerarId);

                _daoConta.Insere(new Conta(campos));

                _linhaDaTabelaFoiClicada = false;

                PopulaTable();
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message);
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            if (_linhaDaTabelaFoiClicada)
            {
                try
                {
                    ValidaCampos();

                    var campos = GeraListaDeCampos(DeveGerarId);

                    _daoConta.Atualiza(new Conta(campos));

                    _linhaDaTabelaFoiClicada = false;

                    PopulaTable();
                }
                catch (Exception ex)
                {
                    MostraErro(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar em uma linha da tabela para executar essa ação!");
            }
        }

        private void buttonRemover_Click(object sender, EventArgs e)
        {
            if (_linhaDaTabelaFoiClicada)
            {
                try
                {
                    var campos = GeraListaDeCampos(DeveGerarId);

                    _daoConta.Deleta(new Conta(campos));

                    _linhaDaTabelaFoiClicada = false;

                    PopulaTable();
                }
                catch (Exception ex)
                {
                    MostraErro(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar em uma linha da tabela para executar essa ação!");
            }
        }

        private void ValidaCampos()
        {
            VerificaCamposVazios();
            VerificaDinheiro();
            VerificaCpf();
            VerificaTelefone();
        }

        private void VerificaCamposVazios()
        {
            var campos = GeraDicionarioDeCampos();

            foreach (var campo in campos)
            {
                if (string.IsNullOrEmpty(campo.Value))
                {
                    throw new CampoNaoPreenchidoException(campo.Key);
                }
            }
        }

        private void VerificaDinheiro()
        {
            if (TemMaisDeDuasCasasDecimais(textBoxLimite.Text))
            {
                throw new CampoNaoPreenchidoException("Limite tem mais de 2 casas decimais");
            }
            if (TemMaisDeDuasCasasDecimais(textBoxSaldo.Text))
            {
                throw new CampoNaoPreenchidoException("Saldo tem mais de 2 casas decimais");
            }
        }

        private bool TemMaisDeDuasCasasDecimais(string campo)
        {
            if (campo.Contains("."))
            {
                if (campo.ElementAt(campo.Length - 3) != '.')
                {
                    return true;
                }
            }

            return false;
        }

        private void VerificaTelefone()
        {
            if (TelefoneNaoEstaPreenchido())
            {
                throw new CampoNaoPreenchidoException("Celular");
            }
            if (TelefoneNaoEhValido())
            {
                throw new TelefoneInvalidoException(maskedTextBoxTelefone.Text);
            }
        }

        private bool TelefoneNaoEstaPreenchido()
        {
            return maskedTextBoxTelefone.Text.Length < 14;
        }

        private bool TelefoneNaoEhValido()
        {
            return !Regex.IsMatch(maskedTextBoxTelefone.Text, "^\\([1-9]{2}\\) (?:[2-8]|9[1-9])[0-9]{3}\\-[0-9]{4}$");
        }

        private void VerificaCpf()
        {
            if (CpfNaoEhValido())
            {
                throw new CpfInvalidoException(maskedTextBoxCpf.Text);
            }
        }

        private bool CpfNaoEhValido()
        {
            return !Util.CPF.EhCpf(maskedTextBoxCpf.Text);
        }

        private Dictionary<string, string> GeraDicionarioDeCampos()
        {
            return new Dictionary<string, string>
            {
                { "Nome", textBoxNome.Text },
                { "Celular", Formatacao.RemoveTudoMenosNumeros(maskedTextBoxTelefone.Text) },
                { "Cpf", Formatacao.RemoveTudoMenosNumeros(maskedTextBoxCpf.Text) },
                { "Saldo", Formatacao.RemoveSimboloDeDinheiro(textBoxSaldo.Text).ToString() },
                { "Limite", Formatacao.RemoveSimboloDeDinheiro(textBoxLimite.Text).ToString() }
            };
        }

        private void MostraErro(string msg)
        {
            MessageBox.Show($"Erro: {msg}");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var cpf = PegaCpfDaTabela();

                _contaSelecionadaDaTabela = _daoConta.PesquisaPorCpf(cpf);

                _linhaDaTabelaFoiClicada = true;

                PopulaCampos(_contaSelecionadaDaTabela);
            }
        }

        private string PegaCpfDaTabela()
        {
            var row = dataGridView1.SelectedRows[0];
            var cell = row.Cells[2];
            return cell.Value.ToString();
        }

        private List<object> GeraListaDeCampos(bool deveGerarId)
        {
            List<object> campos = new List<object>();

            if (deveGerarId)
            {
                campos.Add(_contaSelecionadaDaTabela.Id);
            }

            campos.Add(textBoxNome.Text);
            campos.Add(maskedTextBoxTelefone.Text);
            campos.Add(maskedTextBoxCpf.Text);
            campos.Add(Formatacao.RemoveSimboloDeDinheiro(textBoxSaldo.Text));
            campos.Add(Formatacao.RemoveSimboloDeDinheiro(textBoxLimite.Text));

            return campos;
        }
    }
}
