using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabaio
{
    internal class Program
    {
        static List<ItemCardapio> listaCardapio = new List<ItemCardapio>();
        static int idx = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***** MENU *****");
                Console.WriteLine("1) Cadastrar Item");
                Console.WriteLine("2) Listar Cardápio");
                Console.WriteLine("3) Alterar Preço / Aplicar Desconto");
                Console.WriteLine("4) Pausar / Reativar Vendas");
                Console.WriteLine("5) Remover Itens");
                Console.WriteLine("0) Sair");
                Console.Write("\nOpção: ");

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    ExibirMensagemErro("Entrada inválida.");
                    continue;
                }

                if (opcao == 0) break;

                switch (opcao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
                        ListarProdutos();
                        break;
                    case 3:
                        MenuAlterarPreco();
                        break;
                    case 4:
                        AlternarDisponibilidade();
                        break;
                    case 5:
                        RemoverProduto();
                        break;
                    default:
                        ExibirMensagemErro("Opção inválida.");
                        break;
                }
            }
        }

        static void CadastrarProduto()
        {
            Console.Clear();
            Console.WriteLine("***** CADASTRAR PRODUTO *****\n");
            Console.Write("Informe o nome do produto: ");
            string nome = Console.ReadLine();

            Categoria categoria;
            while (true)
            {
                Console.WriteLine("\nSelecione a categoria do produto:");
                Console.WriteLine("1 - Hambúrguer\n2 - Acompanhamento\n3 - Adicional\n4 - Sobremesa\n5 - Bebida");
                Console.Write("\nOpção: ");
                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    categoria = (Categoria)opcao;
                    break;
                }

                ExibirMensagemErro("Opção inválida.");
            }

            decimal preco;
            while (true)
            {
                Console.Write("\nInforme o preço do produto (R$): ");
                if (decimal.TryParse(Console.ReadLine(), out preco) && preco > 0) break;
                ExibirMensagemErro("Por favor, informe um preço válido maior que zero.");
            }

            listaCardapio.Add(new ItemCardapio(idx++, nome, categoria, preco));
            ExibirMensagemSucesso("Produto cadastrado com sucesso!");
        }

        static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("***** CARDÁPIO *****");
            
            if (listaCardapio.Count == 0)
            {
                Console.WriteLine("\nNenhum produto cadastrado no momento.");
            }
            else
            {
                ExibirResumoCardapio();
            }

            PressionarParaContinuar();
        }

        static void MenuAlterarPreco()
        {
            Console.Clear();
            Console.WriteLine("***** ALTERAR PREÇO / APLICAR DESCONTO *****");
            ExibirResumoCardapio();
            
            ItemCardapio item = BuscarPorId();
            if (item != null)
            {
                AlterarPreco(item);
            }
        }

        static void AlternarDisponibilidade()
        {
            Console.Clear();
            Console.WriteLine("***** PAUSAR / REATIVAR VENDAS *****");
            ExibirResumoCardapio();
            
            ItemCardapio item = BuscarPorId();
            if (item == null) return;
            
            Console.WriteLine($"\nProduto Selecionado: {item.Nome}");
            Console.WriteLine($"Status Atual: {(item.EstaDisponivel ? "Disponível" : "Pausado")}\n");
            Console.WriteLine("1) Pausar Vendas");
            Console.WriteLine("2) Reativar Vendas");
            Console.Write("\nEscolha uma ação: ");

            if (int.TryParse(Console.ReadLine(), out int acao))
            {
                if (acao == 1)
                {
                    item.PausarVendas();
                    ExibirMensagemSucesso($"Vendas de '{item.Nome}' foram PAUSADAS.");
                }
                else if (acao == 2)
                {
                    item.ReativarVendas();
                    ExibirMensagemSucesso($"Vendas de '{item.Nome}' foram REATIVADAS.");
                }
                else
                {
                    ExibirMensagemErro("Ação inválida.");
                }
            }
            else
            {
                ExibirMensagemErro("Entrada inválida.");
            }
        }

        static void RemoverProduto()
        {
            Console.Clear();
            Console.WriteLine("***** REMOVER PRODUTO *****");
            ExibirResumoCardapio();
            
            ItemCardapio item = BuscarPorId();
            if (item == null) return;

            Console.Write($"\nTem certeza que deseja remover '{item.Nome}' (ID: {item.Id})? (S/N): ");
            string confirmacao = Console.ReadLine()?.Trim().ToUpper();

            if (confirmacao == "S")
            {
                listaCardapio.Remove(item);
                ExibirMensagemSucesso($"Produto '{item.Nome}' removido com sucesso!");
            }
            else
            {
                ExibirMensagemErro("Operação de remoção cancelada.");
            }
        }

        static void ExibirResumoCardapio()
        {
            Console.WriteLine("\n***** ITENS REGISTRADOS *****");
            
            if (listaCardapio.Count == 0)
            {
                Console.WriteLine("(Nenhum produto cadastrado no momento)");
            }
            else
            {
                foreach (var item in listaCardapio)
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("---------------------------\n");
        }

        static ItemCardapio BuscarPorId()
        {
            if (listaCardapio.Count == 0)
            {
                ExibirMensagemErro("Não há produtos cadastrados.");
                return null;
            }

            Console.Write("Informe o ID do produto: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var item = listaCardapio.FirstOrDefault(x => x.Id == id);
                if (item == null) ExibirMensagemErro($"Produto com ID {id} não encontrado.");
                return item;
            }

            ExibirMensagemErro("ID inválido.");
            return null;
        }

        static void AlterarPreco(ItemCardapio item)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"***** ALTERAR PREÇO: {item.Nome.ToUpper()} (ATUAL: {item.PrecoBase:C2}) *****\n");
                Console.WriteLine("1) Alterar o preço base");
                Console.WriteLine("2) Reduzir o preço base por porcentagem");
                Console.WriteLine("3) Incrementar o preço base por porcentagem");
                Console.WriteLine("0) Voltar");
                Console.Write("\nOpção: ");

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    ExibirMensagemErro("Opção inválida. Digite um número.");
                    continue;
                }

                if (opcao == 0) break;

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            if (LerDecimal("Informe o novo preço (R$): ", out decimal novoPreco))
                            {
                                item.AlterarPrecoBase(novoPreco);
                                ExibirMensagemSucesso($"Preço alterado com sucesso para {item.PrecoBase:C2}!");
                            }
                            break;

                        case 2:
                            if (LerDecimal("Informe a porcentagem de desconto (%): ", out decimal percentualDesconto))
                            {
                                item.AplicarDesconto(percentualDesconto);
                                ExibirMensagemSucesso($"Desconto aplicado! Novo preço: {item.PrecoBase:C2}");
                            }
                            break;

                        case 3:
                            if (LerDecimal("Informe a porcentagem de aumento (%): ", out decimal percentualAumento))
                            {
                                item.AplicarAcrescimo(percentualAumento);
                                ExibirMensagemSucesso($"Aumento aplicado! Novo preço: {item.PrecoBase:C2}");
                            }
                            break;

                        default:
                            ExibirMensagemErro("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExibirMensagemErro($"Erro de Validação: {ex.Message}");
                }
            }
        }

        static bool LerDecimal(string mensagemPrompt, out decimal valor)
        {
            Console.Write($"\n{mensagemPrompt}");
            if (decimal.TryParse(Console.ReadLine(), out valor))
            {
                return true;
            }

            ExibirMensagemErro("Valor numérico inválido.");
            valor = 0;
            return false;
        }

        static void ExibirMensagemSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[SUCESSO] {mensagem}");
            Console.ResetColor();
            PressionarParaContinuar();
        }

        static void ExibirMensagemErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERRO] {mensagem}");
            Console.ResetColor();
            PressionarParaContinuar();
        }

        static void PressionarParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}