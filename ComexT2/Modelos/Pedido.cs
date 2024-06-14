namespace ComexT2.Modelos;

public class Pedido
{
    public Pedido(Cliente cliente)
    {
        Cliente = cliente;
        Data = DateTime.Now;
        Itens = new List<ItemPedido>();
    }

    public Cliente Cliente { get; private set; }
    public DateTime Data { get; private set; }
    public List<ItemPedido> Itens { get; private set; }
    public double Total { get; private set; }

    public void AdicionarItem(ItemPedido item)
    {
        Itens.Add(item);
        Total += item.Subtotal;
    }

    public override string ToString()
    {
        return $"Cliente: {Cliente.Nome}, Data: {Data}, Total: {Total:F2}";
    }
}
