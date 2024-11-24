Feature: CheckoutPedido
	Para controlar os pedidos da lanchonete
	Eu quero adicionar um pedido

Scenario: Controlar pedidos
	Given Recebendo um pedido com um hamburguer
	And e uma coca-cola
	And e uma batata frita
	When quando eu fizer o checkout
	Then o resultado esperado é o cadastro do pedido com sucesso