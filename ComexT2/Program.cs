// See https://aka.ms/new-console-template for more information
using ComexT2.Modelos;
using System.Text.Json;

var listaProdutos = new List<Produto>();

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
        case -1:
            Console.WriteLine("Finalizando!!!");
            break;
        default:
            Console.WriteLine("Opção inválida!!!");
            break;
    }
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

