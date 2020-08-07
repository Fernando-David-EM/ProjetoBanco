using Banco.DAL;
using Banco.Exceptions;
using Banco.Util;
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

        private void ValidaCampos()
        {
            if (string.IsNullOrEmpty(textBoxUsuario.Text))
            {
                throw new CampoNaoPreenchidoException("Usuário");
            }
            if (string.IsNullOrEmpty(maskedTextBoxSenha.Text))
            {
                throw new CampoNaoPreenchidoException("Senha");
            }
        }

        public void TestCampos(string usuario, string senha)
        {
            textBoxUsuario.Text = usuario;
            maskedTextBoxSenha.Text = senha;
        }

        public void TestLogin()
        {
            ValidaCampos();

            string usuario = textBoxUsuario.Text;
            string senha = maskedTextBoxSenha.Text;

            var usuarioLogado = _daoLogin.Logar(usuario, senha);

            if (usuarioLogado != null)
            {
                UsuarioAutenticado = true;
                Close();
            }
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaCampos();

                string usuario = textBoxUsuario.Text;
                string senha = maskedTextBoxSenha.Text;

                var usuarioLogado = _daoLogin.Logar(usuario, senha);

                if (usuarioLogado != null)
                {
                    UsuarioAutenticado = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message);
            }
        }
        private void MostraErro(string msg)
        {
            MessageBox.Show($"Erro: {msg}");
        }
    }
}
