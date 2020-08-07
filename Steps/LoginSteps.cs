using Banco.DAL;
using Banco.Exceptions;
using Banco.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Banco.Steps
{
    [Binding]
    class LoginSteps
    {
        private DaoLogin _daoLogin = new DaoLogin();
        private TelaLogin _telaLogin;
        private string _loginUsado;
        private string _senhaUsada;

        [Given(@"que estou com a tela aberta")]
        public void DadoQueEstouComATelaAberta()
        {
            _telaLogin = new TelaLogin();
            _telaLogin.Show();
        }

        [Given(@"que existe uma conta com login ""(.*)"" e senha ""(.*)""")]
        public void DadoQueExisteUmaContaComLoginESenha(string login, string senha)
        {
            _loginUsado = login;
            _senhaUsada = senha;

            Assert.DoesNotThrow(() => _daoLogin.Logar(login, senha));
        }

        [Given(@"preencho os campos corretamente e clico em login")]
        public void DadoPreenchoOsCamposCorretamenteEClicoEmLogin()
        {
            Assert.DoesNotThrow(() => _telaLogin.TestCampos(_loginUsado, _senhaUsada));
        }

        [Then(@"devo entrar com sucesso no sistema")]
        public void EntaoDevoEntrarComSucessoNoSistema()
        {
            Assert.DoesNotThrow(() => _telaLogin.TestLogin());
        }

        [Given(@"que não preencho algum campo")]
        public void DadoQueNaoPreenchoAlgumCampo()
        {
            _senhaUsada = "";
        }

        [Then(@"devo ver um erro de campos nao preenchidos ao logar")]
        public void EntaoDevoVerUmErroDeCampos()
        {
            _telaLogin.TestCampos(_loginUsado, _senhaUsada);

            Assert.Throws<CampoNaoPreenchidoException>(() => _telaLogin.TestLogin());
        }

        [Given(@"que preencho o campo do login e senha com ""(.*)"" e ""(.*)""")]
        public void DadoQuePreenchoOCampoDoLoginESenhaComE(string login, string senha)
        {
            _loginUsado = login;
            _senhaUsada = senha;

            _telaLogin.TestCampos(_loginUsado, _senhaUsada);
        }

        [Then(@"devo ver um erro de usuario inexistente ao logar")]
        public void EntaoDevoVerUmErroDeConta()
        {
            Assert.Throws<UsuarioInexistenteException>(() => _telaLogin.TestLogin());
        }

        [Given(@"que coloco o login como ""(.*)""")]
        public void DadoQueColocoOLoginComo(string login)
        {
            _loginUsado = login;
        }

        [Given(@"coloco a senha ""(.*)""")]
        public void DadoColocoASenha(string senha)
        {
            _senhaUsada = senha;

            _telaLogin.TestCampos(_loginUsado, _senhaUsada);
        }

        [Then(@"devo ver um erro de senha invalida ao logar")]
        public void EntaoDevoVerUmErroDeSenha()
        {
            Assert.Throws<SenhaIncorretaException>(() => _telaLogin.TestLogin());
        }

    }
}
