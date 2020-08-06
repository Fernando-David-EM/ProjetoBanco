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
            PopularTable();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            maskedTextBoxTelefone.Mask = "(00) 00000-0000";
            maskedTextBoxCpf.Mask = "000,000,000-00";
            maskedTextBoxSaldo.Mask = "$ 999999";
            maskedTextBoxLimite.Mask = "$ 999999";
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
            maskedTextBoxTelefone.Text = conta.Telefone;
            maskedTextBoxCpf.Text = conta.Cpf;
            maskedTextBoxSaldo.Text = conta.Saldo.ToString();
            maskedTextBoxLimite.Text = conta.Limite.ToString();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {

            try
            {
                ValidarCampos();

                var campos = GeraCamposArrayObject(false);

                _daoConta.Insert((Conta)new Conta().SetPropertiesFromObjectArray(campos));

                _clicado = false;

                PopularTable();
            }
            catch (CampoNaoPreenchidoException ex)
            {
                MostrarErro("Todos os campos devem ser preenchidos corretamente", ex.Message);
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
                try
                {
                    ValidarCampos();

                    var campos = GeraCamposArrayObject(true);

                    _daoConta.Update((Conta)new Conta().SetPropertiesFromObjectArray(campos), maskedTextBoxCpf.Text != _contaSelecionada.Cpf);

                    _clicado = false;

                    PopularTable();
                }
                catch (CampoNaoPreenchidoException ex)
                {
                    MostrarErro("Todos os campos devem ser preenchidos corretamente", ex.Message);
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
                try
                {
                    ValidarCampos();

                    var campos = GeraCamposArrayObject(true);

                    _daoConta.Delete((Conta)new Conta().SetPropertiesFromObjectArray(campos));

                    _clicado = false;

                    PopularTable();
                }
                catch (CampoNaoPreenchidoException ex)
                {
                    MostrarErro("Todos os campos devem ser preenchidos corretamente", ex.Message);
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

        private void ValidarCampos()
        {
            var campos = new string[]
            {
                textBoxNome.Text,
                Formatacao.SobraApenasNumeros(maskedTextBoxTelefone.Text),
                Formatacao.SobraApenasNumeros(maskedTextBoxCpf.Text),
                Formatacao.SobraApenasNumeros(maskedTextBoxSaldo.Text),
                Formatacao.SobraApenasNumeros(maskedTextBoxLimite.Text)
            };

            var camposForamPreenchidos = campos
                .All(x => !string.IsNullOrEmpty(x));

            if (!camposForamPreenchidos)
            {
                throw new CampoNaoPreenchidoException();
            }
            if (campos[1].Length < 11 || !Regex.IsMatch(maskedTextBoxTelefone.Text, "^\\([1-9]{2}\\) (?:[2-8]|9[1-9])[0-9]{3}\\-[0-9]{4}$"))
            {
                throw new CampoNaoPreenchidoException("Campo Celular");
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

                    _contaSelecionada = _daoConta.GetByCpf(cpf);

                    _clicado = true;
                }
                else
                {
                    MessageBox.Show("Selecione apenas uma linha da tabela para alterar!");
                }
            }

            PopularCampos(_contaSelecionada);
        }

        private object[] GeraCamposArrayObject(bool deveGerarId)
        {

            if (deveGerarId)
            {
                return new object[]
            {
                _contaSelecionada.Id,
                textBoxNome.Text,
                maskedTextBoxTelefone.Text,
                maskedTextBoxCpf.Text,
                Formatacao.RemoveReais(maskedTextBoxSaldo.Text),
                Formatacao.RemoveReais(maskedTextBoxLimite.Text)
            };
            }
            else
            {
                return new object[]
            {
                textBoxNome.Text,
                maskedTextBoxTelefone.Text,
                maskedTextBoxCpf.Text,
                Formatacao.RemoveReais(maskedTextBoxSaldo.Text),
                Formatacao.RemoveReais(maskedTextBoxLimite.Text)
            };
            }

        }
    }
}
