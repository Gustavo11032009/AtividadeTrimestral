using System;
using System.Diagnostics.Eventing.Reader;

class Program
{
    const int MAX = 15;
    static Paciente[] fila = new Paciente[MAX];
    static int quantidade = 0;

    class Paciente
    {
        public string Nome;
        public int Idade;
        public bool Preferencial;

        public override string ToString()
        {
            return $"{Nome} - {Idade} anos - {(Preferencial ? "Preferencial" : "Normal")}";
        }
    }

    static void Main()
    {
        string opcao;
        do
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1 - Cadastrar paciente");
            Console.WriteLine("2 - Listar pacientes");
            Console.WriteLine("3 - Atender paciente");
            Console.WriteLine("4 - Alterar paciente");
            Console.WriteLine("q - Sair");
            Console.Write("Escolha: ");
            opcao = Console.ReadLine();

            if (opcao == "1")
            {
                CadastrarPaciente();
            }
            else if (opcao == "2")
            {
                ListarPacientes();
            }
            else if (opcao == "3")
            {
                AtenderPaciente();
            }
            else if (opcao == "4")
            {
                AlterarPaciente();
            }
            else if (opcao == "q")
            {
                Console.WriteLine("Saindo...");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }


        } while (opcao != "q");
    }

    static void CadastrarPaciente()
    {
        if (quantidade >= MAX)
        {
            Console.WriteLine("Fila cheia!");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Idade: ");
        int idade = int.Parse(Console.ReadLine());
        Console.Write("É preferencial? (s/n): ");
        bool pref = Console.ReadLine().ToLower() == "s";

        Paciente novo = new Paciente { Nome = nome, Idade = idade, Preferencial = pref };

        if (pref) // Coloca na frente dos não preferenciais
        {
            for (int i = quantidade; i > 0; i--)
            {
                fila[i] = fila[i - 1];
            }
            fila[0] = novo;
        }
        else
        {
            fila[quantidade] = novo;
        }
        quantidade++;
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    static void ListarPacientes()
    {
        if (quantidade == 0)
        {
            Console.WriteLine("Fila vazia.");
            return;
        }
        Console.WriteLine("\n--- Fila de Pacientes ---");
        for (int i = 0; i < quantidade; i++)
        {
            Console.WriteLine($"{i + 1}. {fila[i]}");
        }
    }

    static void AtenderPaciente()
    {
        if (quantidade == 0)
        {
            Console.WriteLine("Nenhum paciente na fila.");
            return;
        }
        Console.WriteLine($"Atendendo: {fila[0]}");

        for (int i = 0; i < quantidade - 1; i++)
        {
            fila[i] = fila[i + 1];
        }
        fila[quantidade - 1] = null;
        quantidade--;
    }

    static void AlterarPaciente()
    {
        ListarPacientes();
        if (quantidade == 0) return;

        Console.Write("Digite o número do paciente para alterar: ");
        int indice = int.Parse(Console.ReadLine()) - 1;

        if (indice < 0 || indice >= quantidade)
        {
            Console.WriteLine("Paciente inválido.");
            return;
        }

        Console.Write("Novo nome: ");
        fila[indice].Nome = Console.ReadLine();
        Console.Write("Nova idade: ");
        fila[indice].Idade = int.Parse(Console.ReadLine());
        Console.Write("É preferencial? (s/n): ");
        fila[indice].Preferencial = Console.ReadLine().ToLower() == "s";

        Console.WriteLine("Dados atualizados!");
    }
}
