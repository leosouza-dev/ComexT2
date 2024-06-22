namespace ComexT2.Modelos;

/// <summary>
/// Representa um pedido realizado por um cliente
/// </summary>
public class Pedido
{
    /// <summary>
    /// Inicializa uma nova instancia da classe Pedido.
    /// </summary>
    /// <param name="cliente">O cliente que realizou o pedido</param>
    public Pedido(Cliente cliente)
    {
        Cliente = cliente;
        Data = DateTime.Now;
        Itens = new List<ItemPedido>();
    }

    /// <summary>
    /// Obtem o cliente que realizou o pedido.
    /// </summary>
    public Cliente Cliente { get; private set; }
    /// <summary>
    /// Obtem a data em que o pedido foi realizado.
    /// </summary>
    public DateTime Data { get; private set; }
    /// <summary>
    /// Obtem a lista de itens do pedido.
    /// </summary>
    public List<ItemPedido> Itens { get; private set; }
    /// <summary>
    /// Obtem o valor total do pedido.
    /// </summary>
    public double Total { get; private set; }

    /// <summary>
    /// Adiciona um item ao pedido e atualiza o valor total.
    /// </summary>
    /// <param name="item"></param>
    public void AdicionarItem(ItemPedido item)
    {
        Itens.Add(item);
        Total += item.Subtotal;
    }

    /// <summary>
    /// Retorna uma string que representa o pedido atual.
    /// </summary>
    /// <returns>Uma string que representa o pedido atual.</returns>
    public override string ToString()
    {
        return $"Clientes: {Cliente.Nome}, Data: {Data}, Total: {Total:F2}";
    }
}
