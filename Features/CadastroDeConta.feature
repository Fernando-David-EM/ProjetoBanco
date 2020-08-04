#language: pt-BR

Funcionalidade: Cadastrar uma conta
Como administrador
Posso cadastrar quantas contas eu quiser

Contexto: 
	Dado que estou logado como um administrador
	E clico no botao de cadastro de conta

Cenario: Sucesso
	Dado que preencho todos os campos corretamente
	E clico no botao cadastrar
	Então uma nova conta deve ser cadastrada

Cenario: Erro falta de campos
	Dado que deixo de preencher algum campo
	E clico no botao cadastrar
	Então devo ver um erro de preenchimento de campos "Obrigatório o preenchimento dos campos!"

Cenario: Erro cpf já cadastrado
	Dado que já existe uma conta com o cpf 42706252014
	E preenchi o resto dos campos corretamente
	E clico no botao cadastrar
	Então devo ver um erro de cpf existente "CPF já existe no sistema!"

Cenario: Erro cpf inválido
	Dado que preencho o campo cpf com 11111111111
	E preenchi o resto dos campos corretamente
	E clico no botao cadastrar
	Então devo ver um erro de cpf invalido "CPF inválido!"