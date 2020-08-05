using Banco.DAL;
using Banco.Data;
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
    class CadastroDeContaSteps
    {
        private DaoConta _daoConta;
        private Conta _contaAtual;

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

        [Given(@"que estou logado como um administrador")]
        public void DadoQueEstouLogadoComoUmAdministrador()
        {
            //estou!!!
        }

        [Given(@"clico no botao de cadastro de conta")]
        public void DadoClicoNoBotaoDeCadastroDeConta()
        {
            _daoConta = new DaoConta();
        }

        [Given(@"que preencho todos os campos corretamente")]
        public void DadoQuePreenchoTodosOsCamposCorretamente()
        {
            _contaAtual = 
                new Conta
                (
                    "Fernando",
                    "30874856",
                    "12312312312",
                    500.0,
                    1000.0
                );
        }

        [Then(@"uma nova conta deve ser cadastrada ao cadastrar")]
        public void EntaoUmaNovaContaDeveSerCadastrada()
        {
            _daoConta.Insert(_contaAtual);

            var pesquisaConta = _daoConta.GetByCpf("12312312312");

            _contaAtual.Id = pesquisaConta.Id;

            Assert.AreEqual(_contaAtual, pesquisaConta);
        }

        [Given(@"que deixo de preencher algum campo")]
        public void DadoQueDeixoDePreencherAlgumCampo()
        {
            //TODO TELA
        }

        [Then(@"devo ver um erro de preenchimento de campos ao cadastrar")]
        public void EntaoDevoVerUmErroDePreenchimentoDeCampos()
        {
            //TODO TELA
        }

        [Given(@"que já existe uma conta com o cpf ""(.*)""")]
        public void DadoQueJaExisteUmaContaComOCpf(string p0)
        {
            var conta =
                new Conta
                (
                    "Fernando",
                    "30874856",
                    p0,
                    500.0,
                    1000.0
                );

            _daoConta.Insert(conta);

            var pesquisaConta = _daoConta.GetByCpf(p0);

            Assert.AreEqual(p0, pesquisaConta.Cpf);
        }

        [Given(@"preenchi o resto dos campos corretamente com o cpf ""(.*)""")]
        public void DadoPreenchiORestoDosCamposCorretamente(string p0)
        {
            _contaAtual =
                new Conta
                (
                    "Fernando",
                    "30874856",
                    p0,
                    500.0,
                    1000.0
                );
        }

        [Then(@"devo ver um erro de cpf existente ""(.*)"" ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfExistente(string p0)
        {
            var ex = Assert.Throws<CpfExistenteException>(() => _daoConta.Insert(_contaAtual));
            Assert.That(ex.Message, Is.EqualTo(p0));
        }

        [Given(@"que preencho os campos corretamente e o cpf com ""(.*)""")]
        public void DadoQuePreenchoOCampoCpfCom(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"devo ver um erro de cpf invalido ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfInvalido()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
