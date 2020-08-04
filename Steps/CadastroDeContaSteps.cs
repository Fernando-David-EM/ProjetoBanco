using Banco.DAL;
using Banco.Data;
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
    class CadastroDeContaSteps
    {

        DaoConta _daoConta;
        Conta _contaAtual;

        [Given(@"que estou logado como um administrador")]
        public void DadoQueEstouLogadoComoUmAdministrador()
        {
            //TODO
        }

        [Given(@"clico no botao de cadastro de conta")]
        public void DadoClicoNoBotaoDeCadastroDeConta()
        {
            _daoConta = new DaoConta(DataBase.GetConexao());
        }

        [Given(@"que preencho todos os campos corretamente")]
        public void DadoQuePreenchoTodosOsCamposCorretamente()
        {
            _contaAtual = 
                new Conta
                (
                    "Fernando",
                    "30874856",
                    "42706252014",
                    500,
                    1000
                );
        }

        [Given(@"clico no botao cadastrar")]
        public void DadoClicoNoBotaoCadastrar()
        {
            _daoConta.Insert(_contaAtual);
        }

        [Then(@"uma nova conta deve ser cadastrada")]
        public void EntaoUmaNovaContaDeveSerCadastrada()
        {
            
        }

        [Given(@"que deixo de preencher algum campo")]
        public void DadoQueDeixoDePreencherAlgumCampo()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de preenchimento de campos ""(.*)""")]
        public void EntaoDevoVerUmErroDePreenchimentoDeCampos(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que já existe uma conta com o cpf (.*)")]
        public void DadoQueJaExisteUmaContaComOCpf(Decimal p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"preenchi o resto dos campos corretamente")]
        public void DadoPreenchiORestoDosCamposCorretamente()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de cpf existente ""(.*)""")]
        public void EntaoDevoVerUmErroDeCpfExistente(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que preencho o campo cpf com (.*)")]
        public void DadoQuePreenchoOCampoCpfCom(Decimal p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de cpf invalido ""(.*)""")]
        public void EntaoDevoVerUmErroDeCpfInvalido(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
