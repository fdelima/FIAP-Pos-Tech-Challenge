Feature: ControlarPedidos
	Para controlar os pedidos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um pedido
	Alterar um pedido
	Consultar um pedido
	Listar os pedidos
	Deletar um pedido

Scenario: Controlar pedidos
	Given Recebendo um pedido na lanchonete vamos preparar o pedido
	And Adicionar o pedido
	And Encontrar o pedido
	And Alterar o pedido
	And Consultar o pedido
	When Listar o pedido
	Then posso deletar o pedido