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
        private string _loginUsado;
        private string _senhaUsada;

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
            //TODO tela
        }

        [Then(@"devo entrar com sucesso no sistema")]
        public void EntaoDevoEntrarComSucessoNoSistema()
        {
            Assert.DoesNotThrow(() => _daoLogin.Logar(_loginUsado, _senhaUsada)); //redundante?
        }

        [Given(@"que não preencho algum campo")]
        public void DadoQueNaoPreenchoAlgumCampo()
        {
            //TODO tela
        }

        [Then(@"devo ver um erro de campos nao preenchidos ao logar")]
        public void EntaoDevoVerUmErroDeCampos()
        {
            //TODO tela
        }

        [Given(@"que preencho o campo do login e senha com ""(.*)"" e ""(.*)""")]
        public void DadoQuePreenchoOCampoDoLoginESenhaComE(string login, string senha)
        {
            _loginUsado = login;
            _senhaUsada = senha;
        }

        [Then(@"devo ver um erro de usuario inexistente ao logar")]
        public void EntaoDevoVerUmErroDeConta()
        {
            Assert.Throws<UsuarioInexistenteException>(() => _daoLogin.Logar(_loginUsado, _senhaUsada));
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
        }

        [Then(@"devo ver um erro de senha invalida ao logar")]
        public void EntaoDevoVerUmErroDeSenha()
        {
            Assert.Throws<SenhaIncorretaException>(() => _daoLogin.Logar(_loginUsado, _senhaUsada));
        }

    }
}
