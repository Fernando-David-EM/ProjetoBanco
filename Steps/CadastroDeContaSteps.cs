using Banco.DAL;
using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using Banco.Util;
using Banco.View;
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
        private TelaContas _telaContas;
        private TelaLogin _telaLogin;
        private Conta _contaAtual;
        private string _cpfValido;

        [Given(@"que estou logado como um administrador")]
        public void DadoQueEstouLogadoComoUmAdministrador()
        {
            _telaLogin = new TelaLogin();
            _telaLogin.Show();

            _cpfValido = ValidaCPF.GerarCpf();

            _telaLogin.TestCampos("admin", "admin");

            Assert.DoesNotThrow(() => _telaLogin.TestLogin());
        }

        [Given(@"clico no botao de cadastro de conta")]
        public void DadoClicoNoBotaoDeCadastroDeConta()
        {
            _daoConta = new DaoConta();

            _telaContas = new TelaContas();
            _telaContas.Show();
        }

        [Given(@"que preencho todos os campos corretamente")]
        public void DadoQuePreenchoTodosOsCamposCorretamente()
        {
            _contaAtual = new Conta
            {
                Nome = "Fernando",
                Telefone = "(62) 99854-0164",
                Cpf = _cpfValido,
                Saldo = 0,
                Limite = 0
            };

            _telaContas.TesteCampos(_contaAtual.RecebePropriedades());
        }

        [Then(@"uma nova conta deve ser cadastrada ao cadastrar")]
        public void EntaoUmaNovaContaDeveSerCadastrada()
        {
            Assert.DoesNotThrow(() => _telaContas.TesteCadastrar());

            var pesquisaConta = _daoConta.PesquisaPorCpf(_cpfValido);

            _contaAtual.Id = pesquisaConta.Id;

            Assert.AreEqual(_contaAtual, pesquisaConta);
        }

        [Given(@"que deixo de preencher algum campo")]
        public void DadoQueDeixoDePreencherAlgumCampo()
        {
            _contaAtual = new Conta
            {
                Nome = "Fernando",
                Telefone = "(62) 99854-0164",
                Cpf = _cpfValido,
                Saldo = 0,
                Limite = 0
            };

            var campos = _contaAtual.RecebePropriedades();
            campos[0] = "";

            _telaContas.TesteCampos(campos);
        }

        [Then(@"devo ver um erro de preenchimento de campos ao cadastrar")]
        public void EntaoDevoVerUmErroDePreenchimentoDeCampos()
        {
            Assert.Throws<CampoNaoPreenchidoException>(() => _telaContas.TesteCadastrar());
        }

        [Given(@"que já existe uma conta com o cpf ""(.*)""")]
        public void DadoQueJaExisteUmaContaComOCpf(string p0)
        {
            var conta = new Conta
            {
                Nome = "Fernando",
                Telefone = "(62) 99854-0164",
                Cpf = p0,
                Saldo = 0,
                Limite = 0
            };

            _telaContas.TesteCampos(conta.RecebePropriedades());

            try
            {
                _daoConta.PesquisaPorCpf(p0);
            }
            catch (PesquisaSemSucessoException)
            {

                _daoConta.Insere(conta);
            }

            Assert.Throws<CpfExistenteException>(() => _telaContas.TesteCadastrar());
        }

        [Given(@"preenchi o resto dos campos corretamente com o cpf ""(.*)""")]
        public void DadoPreenchiORestoDosCamposCorretamente(string p0)
        {
            _contaAtual = new Conta
            {
                Nome = "Fernando",
                Telefone = "(62) 99854-0164",
                Cpf = p0,
                Saldo = 0,
                Limite = 0
            };

            _telaContas.TesteCampos(_contaAtual.RecebePropriedades());
        }

        [Then(@"devo ver um erro de cpf existente ""(.*)"" ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfExistente(string p0)
        {
            Assert.Throws<CpfExistenteException>(() => _telaContas.TesteCadastrar());
        }

        [Given(@"que preencho os campos corretamente e o cpf com ""(.*)""")]
        public void DadoQuePreenchoOCampoCpfCom(string p0)
        {
            _contaAtual = new Conta
            {
                Nome = "Fernando",
                Telefone = "(62) 99854-0164",
                Cpf = p0,
                Saldo = 0,
                Limite = 0
            };

            _telaContas.TesteCampos(_contaAtual.RecebePropriedades());
        }

        [Then(@"devo ver um erro de cpf invalido ao cadastrar")]
        public void EntaoDevoVerUmErroDeCpfInvalido()
        {
            Assert.Throws<CpfInvalidoException>(() => _telaContas.TesteCadastrar());
        }

    }
}