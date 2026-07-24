using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabaio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ItemCardapio> listaCardapio = new List<ItemCardapio>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***** MENU *****");
                Console.WriteLine("1) Cadastrar Item");
                Console.WriteLine("2) Listar Cardápio");
                Console.WriteLine("3) Alterar Preço / Aplicar Desconto");
                Console.WriteLine("4) Pausar / Reativar Vendas");
                Console.WriteLine("5) Remover Itens");
                Console.WriteLine("6) Sair");
                Console.Write("Opção: ");
                if (!int.TryParse(Console.ReadLine(), out int opcao)) { Console.WriteLine("Entrada inválida."); continue; }
                if (opcao == 6) break;
                switch (opcao)
                {
                    case 1:
                        CadastrarProduto(listaCardapio);
                        break;
                    case 2:
                        ListarProdutos(listaCardapio);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
        }
        static void CadastrarProduto(List<ItemCardapio> lista)
        {
            Console.Clear();
            Console.WriteLine("Informe o nome do produto: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Selecione a categoria do produto: ");
            Categoria categoria;
            while (true)
            {
                Console.WriteLine("1 - Hambúrguer\n2 - Acompanhamento\n3 - Adicional\n4 - Sobremesa\n5 - Bebida");
                Console.Write("Opção: ");
                if (int.TryParse(Console.ReadLine(), out int opcao) && Enum.IsDefined(typeof(Categoria), opcao))
                {
                    categoria = (Categoria)opcao;
                    break;
                }
                Console.WriteLine("Opção inválida.");
            }
            decimal preco;
            while (true)
            {
                Console.WriteLine("Informe o preço do produto: ");
                if (decimal.TryParse(Console.ReadLine(), out preco)) break;
                Console.WriteLine("Por favor informe um preço válido.");
            }
            lista.Add(new ItemCardapio(lista.Count+1,nome, categoria, preco));
            Console.WriteLine("***** PRODUTO CADASTRADO *****");
            Console.ReadKey();
        }

        static void ListarProdutos(List<ItemCardapio> lista)
        {
            Console.Clear();
            Console.WriteLine("***** CARDÁPIO *****");
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
