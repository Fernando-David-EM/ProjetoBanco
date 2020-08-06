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

        public bool UsuarioAutenticado { get; private set; }
        private DaoLogin _daoLogin;

        public TelaLogin()
        {
            InitializeComponent();
            _daoLogin = new DaoLogin();

            maskedTextBoxSenha.PasswordChar = '*';
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = textBoxUsuario.Text;
                string senha = maskedTextBoxSenha.Text;

                var usuarioLogado = _daoLogin.Logar(usuario, senha);

                if (usuarioLogado != null)
                {
                    UsuarioAutenticado = true;
                    Close();
                }

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
