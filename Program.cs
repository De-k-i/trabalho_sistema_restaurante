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
                Console.WriteLine("***** MENU *****");
                Console.WriteLine("1) Cadastrar Item");
                Console.WriteLine("2) Listar Cardápio");
                Console.WriteLine("3) Alterar Preço / Aplicar Desconto");
                Console.WriteLine("4) Pausar / Reativar Vendas");
                Console.WriteLine("5) Remover Itens");
                Console.WriteLine("6) Sair");
                if (!int.TryParse(Console.ReadLine(), out int opcao)) { Console.WriteLine("Entrada inválida."); continue; }
                if (opcao == 6) break;
                switch (opcao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
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
        static void CadastrarProduto()
        {
            Console.WriteLine("Informe o nome do produto: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Selecione a categoria do produto: ");
            Categoria categoria;
            while (true)
            {
                Console.WriteLine("[1 - Hambúrguer\t2 - Acompanhamento\t3 - Adicional\t4 - Sobremesa\t5 - Bebida]");
                if (int.TryParse(Console.ReadLine(), out int opcao) && Enum.IsDefined(typeof(Categoria),opcao)) {
                    categoria = (Categoria)opcao;
                    break;
                }
                Console.WriteLine("Opção inválida.");
            }
            Console.WriteLine("Informe o preço do produto: ");
            while (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Por favor informe um preço válido.");
            }

        }
    }
}
