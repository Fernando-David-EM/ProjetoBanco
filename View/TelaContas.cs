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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco.View
{
    public partial class TelaContas : Form
    {
        private DaoConta _daoConta;
        private Conta _contaSelecionada;
        private bool _clicado = false;

        public TelaContas()
        {
            InitializeComponent();
            _daoConta = new DaoConta();
            PopulaTable();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            maskedTextBoxTelefone.Mask = "(00) 00000-0000";
            maskedTextBoxCpf.Mask = "000,000,000-00";
            maskedTextBoxSaldo.Mask = "$ 999999";
            maskedTextBoxLimite.Mask = "$ 999999";
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
            maskedTextBoxSaldo.Text = conta.Saldo.ToString();
            maskedTextBoxLimite.Text = conta.Limite.ToString();
        }

        public void TesteCampos(string[] campos)
        {
            textBoxNome.Text = campos[0];
            maskedTextBoxTelefone.Text = campos[1];
            maskedTextBoxCpf.Text = campos[2];
            maskedTextBoxSaldo.Text = campos[3];
            maskedTextBoxLimite.Text = campos[4];
        }

        public void TesteCadastrar()
        {
            ValidaCampos();

            var campos = GeraListaDeCampos(false);

            _daoConta.Insere(new Conta(campos));

            _clicado = false;

            PopulaTable();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaCampos();

                var campos = GeraListaDeCampos(false);

                _daoConta.Insere(new Conta(campos));

                _clicado = false;

                PopulaTable();
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message, ex.StackTrace);
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            if (_clicado)
            {
                try
                {
                    ValidaCampos();

                    var campos = GeraListaDeCampos(true);

                    _daoConta.Atualiza(new Conta(campos), maskedTextBoxCpf.Text != _contaSelecionada.Cpf);

                    _clicado = false;

                    PopulaTable();
                }
                catch (Exception ex)
                {
                    MostraErro(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar em uma linha da tabela para executar essa ação!");
            }
        }

        private void buttonRemover_Click(object sender, EventArgs e)
        {
            if (_clicado)
            {
                try
                {
                    ValidaCampos();

                    var campos = GeraListaDeCampos(true);

                    _daoConta.Deleta(new Conta(campos));

                    _clicado = false;

                    PopulaTable();
                }
                catch (Exception ex)
                {
                    MostraErro(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar em uma linha da tabela para executar essa ação!");
            }
        }

        private void ValidaCampos()
        {
            var campos = new string[]
            {
                textBoxNome.Text,
                Formatacao.RemoveTudoMenosNumeros(maskedTextBoxTelefone.Text),
                Formatacao.RemoveTudoMenosNumeros(maskedTextBoxCpf.Text),
                Formatacao.RemoveTudoMenosNumeros(maskedTextBoxSaldo.Text),
                Formatacao.RemoveTudoMenosNumeros(maskedTextBoxLimite.Text)
            };

            var camposForamPreenchidos = campos
                .All(x => !string.IsNullOrEmpty(x));

            if (!camposForamPreenchidos)
            {
                throw new CampoNaoPreenchidoException();
            }
            if (campos[1].Length < 11 || !Regex.IsMatch(maskedTextBoxTelefone.Text, "^\\([1-9]{2}\\) (?:[2-8]|9[1-9])[0-9]{3}\\-[0-9]{4}$"))
            {
                throw new CampoNaoPreenchidoException("Celular");
            }
        }

        private void MostraErro(string msg, string ex)
        {
            MessageBox.Show($"Erro: {msg}\n{ex}");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowCount =
               dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                if (selectedRowCount == 1)
                {
                    var row = dataGridView1.SelectedRows[0];
                    var cell = row.Cells[2];
                    var cpf = cell.Value.ToString();

                    _contaSelecionada = _daoConta.PesquisaPorCpf(cpf);

                    _clicado = true;
                }
                else
                {
                    MessageBox.Show("Selecione apenas uma linha da tabela para alterar!");
                }
            }

            PopulaCampos(_contaSelecionada);
        }

        private List<object> GeraListaDeCampos(bool deveGerarId)
        {
            List<object> campos = new List<object>();

            if (deveGerarId)
            {
                campos.Add(_contaSelecionada.Id);
            }

            campos.Add(textBoxNome.Text);
            campos.Add(maskedTextBoxTelefone.Text);
            campos.Add(maskedTextBoxCpf.Text);
            campos.Add(Formatacao.RemoveSimboloDeDinheiro(maskedTextBoxSaldo.Text));
            campos.Add(Formatacao.RemoveSimboloDeDinheiro(maskedTextBoxLimite.Text));

            return campos;
        }
    }
}
