namespace ComexT2.Modelos;

public class ItemPedido
{
    public ItemPedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = produto.PrecoUnitario;
        Subtotal = quantidade * produto.PrecoUnitario;
    }

    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public double PrecoUnitario { get; private set; }
    public double Subtotal { get; private set; }

    public override string ToString()
    {
        return $"Produto: {Produto.Nome}, Quantidade: {Quantidade}, " +
            $"Preço Unitário: {PrecoUnitario:F2}, SubTotal: {Subtotal:F2}";
    }
}
