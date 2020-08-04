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
        [Given(@"que existe uma conta com login ""(.*)"" e senha ""(.*)""")]
        public void DadoQueExisteUmaContaComLoginESenha(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"preencho os campos corretamente")]
        public void DadoPreenchoOsCamposCorretamente()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"clico no botao de login")]
        public void DadoClicoNoBotaoDeLogin()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo entrar com sucesso no sistema")]
        public void EntaoDevoEntrarComSucessoNoSistema()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que não preencho algum campo")]
        public void DadoQueNaoPreenchoAlgumCampo()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de campos ""(.*)""")]
        public void EntaoDevoVerUmErroDeCampos(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que preencho os campos do login")]
        public void DadoQuePreenchoOsCamposDoLogin()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"não tenho conta no sistema")]
        public void DadoNaoTenhoContaNoSistema()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de conta ""(.*)""")]
        public void EntaoDevoVerUmErroDeConta(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que preencho os campos do login com um usuário existente")]
        public void DadoQuePreenchoOsCamposDoLoginComUmUsuarioExistente()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"erro a senha")]
        public void DadoErroASenha()
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
