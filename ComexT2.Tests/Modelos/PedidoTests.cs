using ComexT2.Modelos;

namespace ComexT2.Tests.Modelos
{
    public class PedidoTests
    {
        [Fact]
        public void PedidoDeveInicializarComClienteEDataCorreta()
        {
            // Arrange
            var cliente = new Cliente { Nome = " Leo " };

            //Act
            var pedido = new Pedido(cliente);

            //Assert
            Assert.Equal(cliente, pedido.Cliente);
            Assert.Empty(pedido.Itens);
            Assert.Equal(0, pedido.Total);
        }

        [Theory]
        [InlineData("Produto A", 100.0, 2)]
        [InlineData("Produto B", 200.0, 1)]
        [InlineData("Produto C", 300.0, 3)]
        public void AdicionarItemDeveAdicionarItemEAtualizarTotal(string nomeProduto, 
            double precoUnitario, int quantidade)
        {
            // Arrange
            var cliente = new Cliente { Nome = "Leo" };
            var pedido = new Pedido(cliente);
            var produto = new Produto(nomeProduto) { PrecoUnitario = precoUnitario };
            var item = new ItemPedido(produto, quantidade);

            var totalEsperado = precoUnitario * quantidade;

            // Act
            pedido.AdicionarItem(item);

            // Assert
            Assert.Contains(item, pedido.Itens);
            Assert.Equal(totalEsperado, pedido.Total);
        }

        [Fact]
        public void ToStringDeveRetornarStringCorretra()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Leo" };
            var pedido = new Pedido(cliente);
            var produto = new Produto("Produto A") { PrecoUnitario = 100.0 };
            var item = new ItemPedido(produto, 2);
            pedido.AdicionarItem(item);

            var stringEsperada = $"Clientes: {cliente.Nome}, Data: {pedido.Data}, Total: {pedido.Total:F2}";

            // Act
            var result = pedido.ToString();

            // Assert
            Assert.Equal(stringEsperada, result);
        }
    }
}