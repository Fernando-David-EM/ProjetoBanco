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
            PopularTable();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void PopularTable()
        {
            dataGridView1.Rows.Clear();

            var contas = _daoConta.GetAll();

            if (contas.Count() > 0)
            {
                foreach (var conta in contas)
                {
                    dataGridView1.Rows.Add(conta.GetProperties());
                }
            }
        }

        private void PopularCampos(Conta conta)
        {
            textBoxNome.Text = conta.Nome;
            textBoxTelefone.Text = conta.Telefone;
            textBoxCpf.Text = conta.Cpf;
            textBoxSaldo.Text = conta.Saldo.ToString();
            textBoxLimite.Text = conta.Limite.ToString();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            var campos = new object[]
            {
                textBoxNome.Text,
                textBoxTelefone.Text,
                textBoxCpf.Text,
                textBoxSaldo.Text,
                textBoxLimite.Text
            };

            try
            {
                ValidarCampos(campos);

                _daoConta.Insert((Conta)new Conta().SetPropertiesFromObjectArray(campos));

                _clicado = false;

                PopularTable();
            }
            catch (CpfExistenteException ex)
            {
                MostrarErro("Cpf já existe!", ex.Message);
            }
            catch (CpfInvalidoException ex)
            {
                MostrarErro("Cpf inválido!", ex.Message);
            }
            catch (FalhaEmInserirException ex)
            {
                MostrarErro("", ex.Message);
            }
            catch (Exception ex)
            {
                MostrarErro(ex.Message, ex.StackTrace);
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            if (_clicado)
            {
                var campos = new object[]
                {
                _contaSelecionada.Id.ToString(),
                textBoxNome.Text,
                textBoxTelefone.Text,
                textBoxCpf.Text,
                textBoxSaldo.Text,
                textBoxLimite.Text
                };

                try
                {
                    ValidarCampos(campos);

                    _daoConta.Update((Conta)new Conta().SetPropertiesFromObjectArray(campos), textBoxCpf.Text != _contaSelecionada.Cpf);

                    _clicado = false;

                    PopularTable();
                }
                catch (CpfExistenteException ex)
                {
                    MostrarErro("Cpf já existe!", ex.Message);
                }
                catch (CpfInvalidoException ex)
                {
                    MostrarErro("Cpf inválido!", ex.Message);
                }
                catch (FalhaEmAtualizarException ex)
                {
                    MostrarErro("", ex.Message);
                }
                catch (Exception ex)
                {
                    MostrarErro(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar numa linha da tabela para executar essa ação!");
            }
        }

        private void buttonRemover_Click(object sender, EventArgs e)
        {
            if (_clicado)
            {
                var campos = new object[]
            {
                _contaSelecionada.Id.ToString(),
                textBoxNome.Text,
                textBoxTelefone.Text,
                textBoxCpf.Text,
                textBoxSaldo.Text,
                textBoxLimite.Text
            };

                try
                {
                    ValidarCampos(campos);

                    _daoConta.Delete((Conta)new Conta().SetPropertiesFromObjectArray(campos));

                    _clicado = false;

                    PopularTable();
                }
                catch (FalhaEmDeletarException ex)
                {
                    MostrarErro("", ex.Message);
                }
                catch (Exception ex)
                {
                    MostrarErro(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("É necessário clicar numa linha da tabela para executar essa ação!");
            }
        }

        private void ValidarCampos(object[] campos)
        {
            var camposSaoValidos = campos
                .All(x => !string.IsNullOrEmpty((string)x));

            if (!camposSaoValidos)
            {
                throw new CampoNaoPreenchidoException();
            }
        }

        private void MostrarErro(string msg, string ex)
        {
            MessageBox.Show($"Erro: {msg}\n{ex}");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

                    _contaSelecionada = _daoConta.GetByCpf(Formatacao.RemovePontos(cpf));

                    _clicado = true;
                }
                else
                {
                    MessageBox.Show("Selecione apenas uma linha da tabela para alterar!");
                }
            }

            PopularCampos(_contaSelecionada);
        }
    }
}
