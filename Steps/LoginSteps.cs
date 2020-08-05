using Banco.DAL;
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

        [Then(@"devo ver um erro de campos ""(.*)""")]
        public void EntaoDevoVerUmErroDeCampos(string p0)
        {
            //TODO tela
        }

        [Given(@"que preencho o campo do login e senha com ""(.*)"" e ""(.*)""")]
        public void DadoQuePreenchoOCampoDoLoginESenhaComE(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de conta ""(.*)""")]
        public void EntaoDevoVerUmErroDeConta(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que coloco o login como ""(.*)""")]
        public void DadoQueColocoOLoginComo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"coloco a senha ""(.*)""")]
        public void DadoColocoASenha(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de senha ""(.*)""")]
        public void EntaoDevoVerUmErroDeSenha(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
