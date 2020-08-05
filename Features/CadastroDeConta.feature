#language: pt-BR

Funcionalidade: Cadastrar uma conta
Como administrador
Posso cadastrar quantas contas eu quiser

Contexto: 
	Dado que estou logado como um administrador
	E clico no botao de cadastro de conta

Cenario: Sucesso
	Dado que preencho todos os campos corretamente
	Então uma nova conta deve ser cadastrada ao cadastrar

Cenario: Erro falta de campos
	Dado que deixo de preencher algum campo
	Então devo ver um erro de preenchimento de campos ao cadastrar

Cenario: Erro cpf já cadastrado
	Dado que já existe uma conta com o cpf "42706252014"
	E preenchi o resto dos campos corretamente com o cpf "42706252014"
	Então devo ver um erro de cpf existente "42706252014" ao cadastrar

Cenario: Erro cpf inválido
	Dado que preencho os campos corretamente e o cpf com "11111111111"
	Então devo ver um erro de cpf invalido "11111111111" ao cadastrar