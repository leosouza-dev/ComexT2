// See https://aka.ms/new-console-template for more information
using ComexT2.Modelos;
using System.Text.Json;

var listaPedidos = new List<Pedido>();
var listaProdutos = new List<Produto>
{
    new Produto("Notebook")
    {
        Descricao = "Notebook Dell Inspiron",
        PrecoUnitario = 3500.00,
        Quantidade = 10
    },
    new Produto("Smartphone")
    {
        Descricao = "Smartphone Samsung Galaxy",
        PrecoUnitario = 1200.00,
        Quantidade = 25
    },
    new Produto("Monitor")
    {
        Descricao = "Monitor LG Ultrawide",
        PrecoUnitario = 800.00,
        Quantidade = 15
    },
    new Produto("Teclado")
    {
        Descricao = "Teclado Mecânico RGB",
        PrecoUnitario = 250.00,
        Quantidade = 50
    }
};

void ExibirLogo()
{
    Console.WriteLine(@"
────────────────────────────────────────────────────────────────────────────────────────
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██████████████░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██░░██████████─██░░██████░░██─██░░░░░░░░░░░░░░░░░░██─██░░██████████─████░░██──██░░████─
─██░░██─────────██░░██──██░░██─██░░██████░░██████░░██─██░░██───────────██░░░░██░░░░██───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░░░░░░░░░██─────██░░░░░░██─────
─██░░██─────────██░░██──██░░██─██░░██──██████──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──────────██░░██─██░░██───────────██░░░░██░░░░██───
─██░░██████████─██░░██████░░██─██░░██──────────██░░██─██░░██████████─████░░██──██░░████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██──────────██░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
────────────────────────────────────────────────────────────────────────────────────────");
    Console.WriteLine("Boas vindas ao COMEX!!!");
}

async Task ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 Criar Produtx");
    Console.WriteLine("Digite 2 Listar Produtos");
    Console.WriteLine("Digite 3 Consultar API Externa");
    Console.WriteLine("Digite 4 Criar Pedido");
    Console.WriteLine("Digite 5 Listar Pedidos");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua Opção: ");
    string opcaoEscolhida = Console.ReadLine();
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica)
    {
        case 1:
            CriarProduto();
            break;
        case 2:
            ListarProdutos();
            break;
        case 3:
            await ConsultarApiExterna();
            break;
        case 4:
            await CriarPedido();
            break;
        case 5:
            await ListarPedidos();
            break;
        case -1:
            Console.WriteLine("Finalizando!!!");
            break;
        default:
            Console.WriteLine("Opção inválida!!!");
            break;
    }
}

async Task CriarPedido()
{
    Console.Clear();
    Console.WriteLine("Criando um novo pedido\n");

    Console.WriteLine("Digite o nome do Cliente: ");
    string nomeCliente = Console.ReadLine();
    var cliente = new Cliente();
    cliente.Nome = nomeCliente;

    var pedido = new Pedido(cliente);

    while (true)
    {
        Console.WriteLine("\nProdutos Disponiveis: ");
        for (int i = 0; i < listaProdutos.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {listaProdutos[i].Nome}");
        }

        Console.WriteLine("Digite o número do produto que deseja adicionar (0 para sair): ");
        int numeroProduto = int.Parse(Console.ReadLine());
        if (numeroProduto == 0) 
        { 
            break;
        }

        var produto = listaProdutos[numeroProduto - 1];

        Console.WriteLine("Digite a quntidade desejada: ");
        var quantidade = int.Parse(Console.ReadLine());

        var itemDePedido = new ItemPedido(produto, quantidade);
        pedido.AdicionarItem(itemDePedido);
        Console.WriteLine($"{itemDePedido} - adicionado com sucesso!\n");
        
    }
    listaPedidos.Add(pedido);
    Console.WriteLine($"{pedido} - criado com sucesso!\n");
    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}

async Task ListarPedidos()
{
    Console.Clear();
    Console.WriteLine("Listando todos os pedidos da nossa aplicação: ");

    var pedidosOrdanados = listaPedidos.OrderBy(p => p.Cliente.Nome).ToList();

    foreach (var pedido in pedidosOrdanados)
    {
        Console.WriteLine($"Pedido: {pedido}");
    }

    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}

async Task ConsultarApiExterna()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Exibindo Produtos da API Externa\n");

            string resposta = await client.GetStringAsync("https://fakestoreapi.com/products");
            var produtos = JsonSerializer.Deserialize<List<Produto>>(resposta);

            foreach (var produto in produtos)
            {
                Console.WriteLine($"\nNome: {produto.Nome}, " +
                    $"Descricao: {produto.Descricao}, " +
                    $"Preço: {produto.PrecoUnitario}");
            }

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
            await ExibirOpcoesDoMenu();
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Temos um problema : {ex.Message}");
        }
    }
}

void CriarProduto()
{
    Console.Clear();
    Console.WriteLine("Registro de Produto");

    Console.WriteLine("\nDigite o nome do Produto: ");
    string nomeProduto = Console.ReadLine();

    var produto = new Produto(nomeProduto);

    Console.WriteLine("\nDigite a descrição do Produto: ");
    string descricaoProduto = Console.ReadLine();
    produto.Descricao = descricaoProduto;

    Console.WriteLine("\nDigite o preço do Produto: ");
    string precoProduto = Console.ReadLine();
    produto.PrecoUnitario = double.Parse(precoProduto);

    Console.WriteLine("\nDigite a quantidade do Produto: ");
    string quantidadeProduto = Console.ReadLine();
    produto.Quantidade = int.Parse(quantidadeProduto);

    listaProdutos.Add(produto);
    Console.WriteLine($"O Produto {produto.Nome} foi registrado " +
        $"com sucesso!");
    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    ExibirOpcoesDoMenu();
}


void ListarProdutos()
{
    Console.Clear();
    Console.WriteLine("Listagem de Produtos");

    foreach (var produto in listaProdutos)
    {
        Console.WriteLine($"\nProduto: {produto.Nome}, " +
            $"Preço: {produto.PrecoUnitario:F2}, " +
            $"Quantidade: {produto.Quantidade}, " +
            $"Descrição: {produto.Descricao}");
    }

    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    ExibirOpcoesDoMenu();
}

await ExibirOpcoesDoMenu();

//Eletronico eletronicoNovo = new Eletronico("celular");
//eletronicoNovo.Descricao = "celular muito novo";
//eletronicoNovo.Potencia = 2000;

//Console.WriteLine(eletronicoNovo.Nome);
//Console.WriteLine(eletronicoNovo.Descricao);
//Console.WriteLine(eletronicoNovo.Potencia);

//Livro biblia = new Livro("Biblia");
//biblia.Isbn = "123456789";
//string identificacaoBiblia = biblia.Identificar();

//Console.WriteLine(identificacaoBiblia);

//Cliente cliente = new Cliente();
//cliente.Nome = "Lucas";
//cliente.CPF = "123456789";
//string identificacaoCliente = cliente.Identificar();

//Console.WriteLine(identificacaoCliente);

