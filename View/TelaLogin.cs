using Banco.DAL;
using Banco.Exceptions;
using Banco.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    public partial class TelaLogin : Form
    {
        private DaoLogin _daoLogin;

        public TelaLogin()
        {
            InitializeComponent();
            _daoLogin = new DaoLogin();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = textBoxUsuario.Text;
                string senha = textBoxSenha.Text;

                _daoLogin.Logar(usuario, senha);

                new TelaContas().Show();
            }
            catch (PesquisaSemSucessoException ex)
            {
                MessageBox.Show("Erro: Ocorreu algum problema em uma pesquisa no banco!\n" + ex.Message);
            }
            catch (SenhaIncorretaException)
            {
                MessageBox.Show("Erro: Senha incorreta!");
            }
            catch (UsuarioInexistenteException)
            {
                MessageBox.Show("Erro: Usuário incorreto!");
            }
        }
    }
}
