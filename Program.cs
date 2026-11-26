using System;
using System.Collections.Generic;

namespace N2
{
    class Produto
    {
        public string nome;
        public int codigo;
        public double preco;
        public int estoque;
    }

    class Program
    {
        static List<Produto> produtos = new List<Produto>();

        static void Main()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n===== LOJA DE ELETRÔNICOS =====");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar");
                Console.WriteLine("3 - Buscar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Cadastrar();
                        break;
                    case 2:
                        Listar();
                        break;
                    case 3:
                        Buscar();
                        break;
                    case 4:
                        Remover();
                        break;
                    case 0:
                        Console.WriteLine("Programa encerrado.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar.");
                    Console.ReadLine();
                }
            } while (opcao != 0);
        }

        // CADASTRAR PRODUTO
        static void Cadastrar()
        {
            Console.WriteLine("\n===== CADASTRAR PRODUTO =====");
            Produto p = new Produto();

            Console.Write("Nome: ");
            p.nome = Console.ReadLine();
            Console.Write("Código: ");
            p.codigo = int.Parse(Console.ReadLine());

            foreach (Produto produto in produtos)
            {
                if (produto.codigo == p.codigo)
                {
                    Console.WriteLine("Código já cadastrado!");
                    return;
                }
            }

            Console.Write("Preço: ");
            p.preco = double.Parse(Console.ReadLine());
            Console.Write("Estoque: ");
            p.estoque = int.Parse(Console.ReadLine());
            produtos.Add(p);
            Console.WriteLine("Produto cadastrado com sucesso!");
        }

        // LISTAR PRODUTOS
        static void Listar()
        {
            Console.WriteLine("\n===== PRODUTOS CADASTRADOS =====");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            for (int i = 0; i < produtos.Count; i++)
            {
                Exibir(produtos[i]);
            }

            Console.WriteLine("\nTotal de produtos: " + produtos.Count);
        }

        // EXIBIR PRODUTO
        static void Exibir(Produto p)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Nome: " + p.nome);
            Console.WriteLine("Código: " + p.codigo);
            Console.WriteLine("Preço: R$ " + p.preco.ToString("F2"));
            Console.WriteLine("Estoque: " + p.estoque);
            Console.WriteLine("-----------------------------");
        }

        // SELECTION SORT
        static void SelectionSort(int criterio)
        {
            for (int i = 0; i < produtos.Count - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < produtos.Count; j++)
                {
                    // Ordenar por código
                    if (criterio == 1)
                    {
                        if (produtos[j].codigo < produtos[menor].codigo)
                        {
                            menor = j;
                        }
                    }
                    // Ordenar por nome
                    else if (criterio == 2)
                    {
                        if (string.Compare(produtos[j].nome,
                            produtos[menor].nome, true) < 0)
                        {
                            menor = j;
                        }
                    }
                }

                Produto temp = produtos[i];
                produtos[i] = produtos[menor];
                produtos[menor] = temp;
            }
        }

        // BUSCA BINÁRIA POR CÓDIGO
        static int BuscaBinariaCodigo(int codigo)
        {
            SelectionSort(1);
            int inicio = 0;
            int fim = produtos.Count - 1;

            while (inicio <= fim)
            {
                int meio = (inicio + fim) / 2;
                if (produtos[meio].codigo == codigo)
                {
                    return meio;
                }
                else if (produtos[meio].codigo < codigo)
                {
                    inicio = meio + 1;
                }
                else
                {
                    fim = meio - 1;
                }
            }

            return -1;
        }

        // BUSCA BINÁRIA POR NOME
        static int BuscaBinariaNome(string nome)
        {
            SelectionSort(2);
            int inicio = 0;
            int fim = produtos.Count - 1;

            while (inicio <= fim)
            {
                int meio = (inicio + fim) / 2;
                int comparacao = string.Compare(
                    produtos[meio].nome, nome, true);

                if (comparacao == 0)
                {
                    return meio;
                }
                else if (comparacao < 0)
                {
                    inicio = meio + 1;
                }
                else
                {
                    fim = meio - 1;
                }
            }

            return -1;
        }

        // BUSCAR PRODUTO
        static void Buscar()
        {
            Console.WriteLine("\n===== BUSCAR PRODUTO =====");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.WriteLine("1 - Buscar por código");
            Console.WriteLine("2 - Buscar por nome");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());
            int posicao;

            if (opcao == 1)
            {
                Console.Write("Digite o código: ");
                int codigo = int.Parse(Console.ReadLine());
                posicao = BuscaBinariaCodigo(codigo);
            }
            else if (opcao == 2)
            {
                Console.Write("Digite o nome: ");
                string nome = Console.ReadLine();
                posicao = BuscaBinariaNome(nome);
            }
            else
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            if (posicao == -1)
            {
                Console.WriteLine("Produto não encontrado.");
            }
            else
            {
                Console.WriteLine("\nProduto encontrado!");
                Exibir(produtos[posicao]);
            }
        }

        // REMOVER PRODUTO
        static void Remover()
        {
            Console.WriteLine("\n===== REMOVER PRODUTO =====");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());
            int posicao = BuscaBinariaCodigo(codigo);

            if (posicao != -1)
            {
                produtos.RemoveAt(posicao);
                Console.WriteLine("Produto removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }
    }
}
