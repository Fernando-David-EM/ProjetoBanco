#language: pt-BR

Funcionalidade: Login
Dado que tenho uma conta de administrador
Logo no sistema e tenho acesso às funcionalidades

Cenario: Sucesso
	Dado que existe uma conta com login "admin" e senha "admin"
	E preencho os campos corretamente
	E clico no botao de login
	Então devo entrar com sucesso no sistema

Cenario: Erro campo não preenchido
	Dado que não preencho algum campo
	E clico no botao de login
	Então devo ver um erro de campos "Obrigatório o preenchimento dos campos!"

Cenario: Erro conta inexistente
	Dado que preencho os campos do login
	Mas não tenho conta no sistema
	E clico no botao de login
	Então devo ver um erro de conta "Usuário inexistente"

Cenario: Erro senha inválida
	Dado que preencho os campos do login com um usuário existente
	Mas erro a senha
	E clico no botao de login
	Então devo ver um erro de senha "Senha inválida"