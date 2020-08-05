#language: pt-BR

Funcionalidade: Login
Dado que tenho uma conta de administrador
Logo no sistema e tenho acesso às funcionalidades

Cenario: Sucesso
	Dado que existe uma conta com login "admin" e senha "admin"
	E preencho os campos corretamente e clico em login
	Então devo entrar com sucesso no sistema

Cenario: Erro campo não preenchido
	Dado que não preencho algum campo
	Então devo ver um erro de campos nao preenchidos ao logar

Cenario: Erro conta inexistente
	Dado que preencho o campo do login e senha com "teste" e "teste" 
	Então devo ver um erro de usuario inexistente ao logar

Cenario: Erro senha inválida
	Dado que coloco o login como "admin"
	E coloco a senha "1234"
	Então devo ver um erro de senha invalida ao logar