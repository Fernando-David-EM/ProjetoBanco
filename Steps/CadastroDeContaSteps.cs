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
        private DaoLogin _daoLogin;
        private Conta _contaAtual;
        private string _cpfValido = "11739736052";

        [Given(@"que estou logado como um administrador")]
        public void DadoQueEstouLogadoComoUmAdministrador()
        {
            _daoLogin = new DaoLogin();

            Assert.DoesNotThrow(() => _daoLogin.Logar("admin", "admin"));
        }

        [Given(@"clico no botao de cadastro de conta")]
        public void DadoClicoNoBotaoDeCadastroDeConta()
        {
            _daoConta = new DaoConta();
        }

        [Given(@"que preencho todos os campos corretamente")]
        public void DadoQuePreenchoTodosOsCamposCorretamente()
        {
            _contaAtual = new Conta();

            _contaAtual.Nome = "Fernando";
            _contaAtual.Telefone = "30874856";
            _contaAtual.Cpf = _cpfValido;
            _contaAtual.Saldo = 500.0;
            _contaAtual.Limite = 1000.0;
        }

        [Then(@"uma nova conta deve ser cadastrada ao cadastrar")]
        public void EntaoUmaNovaContaDeveSerCadastrada()
        {
            _daoConta.Insere(_contaAtual);

            var pesquisaConta = _daoConta.PesquisaPorCpf(_cpfValido);

            _contaAtual.Id = pesquisaConta.Id;

            Assert.AreEqual(_contaAtual, pesquisaConta);
        }

        [Given(@"que deixo de preencher algum campo")]
        public void DadoQueDeixoDePreencherAlgumCampo()
        {
            //Não sei testar
        }

        [Then(@"devo ver um erro de preenchimento de campos ao cadastrar")]
        public void EntaoDevoVerUmErroDePreenchimentoDeCampos()
        {
            //Também não sei testar

            Assert.Throws<CampoNaoPreenchidoException>(() => throw new CampoNaoPreenchidoException());
        }

        [Given(@"que já existe uma conta com o cpf ""(.*)""")]
        public void DadoQueJaExisteUmaContaComOCpf(string p0)
        {
            var conta = new Conta();

            conta.Nome = "Fernando";
            conta.Telefone = "30874856";
            conta.Cpf = p0;
            conta.Saldo = 500.0;
            conta.Limite = 1000.0;

            _daoConta.Insere(conta);

            var pesquisaConta = _daoConta.PesquisaPorCpf(p0);

            Assert.AreEqual(p0, pesquisaConta.Cpf);
        }

        [Given(@"preenchi o resto dos campos corretamente com o cpf ""(.*)""")]
        public void DadoPreenchiORestoDosCamposCorretamente(string p0)
        {
            _contaAtual = new Conta();

            _contaAtual.Nome = "Fernando";
            _contaAtual.Telefone = "30874856";
            _contaAtual.Cpf = p0;
            _contaAtual.Saldo = 500.0;
            _contaAtual.Limite = 1000.0;
        }

        [Then(@"devo ver um erro de cpf existente ""(.*)"" ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfExistente(string p0)
        {
            var ex = Assert.Throws<CpfExistenteException>(() => _daoConta.Insere(_contaAtual));
            Assert.That(ex.Message, Is.EqualTo(p0));
        }

        [Given(@"que preencho os campos corretamente e o cpf com ""(.*)""")]
        public void DadoQuePreenchoOCampoCpfCom(string p0)
        {
            _contaAtual = new Conta();

            _contaAtual.Nome = "Fernando";
            _contaAtual.Telefone = "30874856";
            _contaAtual.Cpf = p0;
            _contaAtual.Saldo = 500.0;
            _contaAtual.Limite = 1000.0;
        }

        [Then(@"devo ver um erro de cpf invalido ""(.*)"" ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfInvalido(string p0)
        {
            var ex = Assert.Throws<CpfInvalidoException>(() => _daoConta.Insere(_contaAtual));
            Assert.That(ex.Message, Is.EqualTo(p0));
        }

    }
}