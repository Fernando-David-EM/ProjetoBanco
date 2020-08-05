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
        public static string NumericoAleatorio(int tamanho)
        {
            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        DaoConta _daoConta;
        Conta _contaAtual;
        string _cpfUsado;

        [Given(@"que estou logado como um administrador")]
        public void DadoQueEstouLogadoComoUmAdministrador()
        {
            //TODO
        }

        [Given(@"clico no botao de cadastro de conta")]
        public void DadoClicoNoBotaoDeCadastroDeConta()
        {
            _daoConta = new DaoConta();
        }

        [Given(@"que preencho todos os campos corretamente")]
        public void DadoQuePreenchoTodosOsCamposCorretamente()
        {
            _cpfUsado = NumericoAleatorio(11);

            _contaAtual = 
                new Conta
                (
                    "Fernando",
                    "30874856",
                    _cpfUsado,
                    500.0,
                    1000.0
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
            var pesquisaConta = _daoConta.GetByCpf(_cpfUsado);

            _contaAtual.Id = pesquisaConta.Id;

            Assert.AreEqual(_contaAtual, pesquisaConta);
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
