
public class Jogo
{
    public Jogador Pessoa;
    public Jogador Maquina;
    public Jogador JogadorDaVez;

    public string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


    // '~' = não atacada, 'O' = água, 'X' = acertou navio
    public char[,] tirosPessoa = new char[10, 10];
    public char[,] tirosMaquina = new char[10, 10];
    public Jogo()
    {
        string nome;
        Console.WriteLine("Digite seu nome: ");
        nome = Console.ReadLine();

        Pessoa = new Jogador(nome, new Tabuleiro());
        Maquina = new Jogador("Maquina", new Tabuleiro());
        JogadorDaVez = Pessoa;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tirosPessoa[i, j] = '~';
                tirosMaquina[i, j] = '~';
            }
        }

    }

    public void ExibirTabuleiros()
    {
        Console.WriteLine("SEU TABULEIRO (~ = não atacada, O = água, X = acerto)");
        Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(letras[i] + "  ");

            for (int j = 0; j < 10; j++)
            {
                Console.Write(tirosMaquina[i, j] + " ");
            }

            Console.WriteLine();
        }
        Console.WriteLine();

        Console.WriteLine("TABULEIRO DA MÁQUINA (~ = não atacada, O = água, X = acerto)");
        Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(letras[i] + "  ");

            for (int j = 0; j < 10; j++)
            {
                Console.Write(tirosPessoa[i, j] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Acertos -> " + Pessoa.Nome + ": " + Pessoa.Acertos + "/30   Máquina: " + Maquina.Acertos + "/30");
        Console.WriteLine();
    }
    public void TurnoPessoa()
    {
        Console.Write("Digite a coordenada do ataque (ex.: B5): ");
        string coordenada = Console.ReadLine().ToUpper();

        string letra = coordenada.Substring(0, 1);
        int numero = int.Parse(coordenada.Substring(1));

        int linha = 0;
        for (int i = 0; i < 10; i++)
        {
            if (letras[i] == letra)
            {
                linha = i;
            }
        }

        int coluna = numero - 1;

        int resultado = Pessoa.Atacar(Maquina.Tabuleiro, linha, coluna);

        if (resultado == 4)
        {
            tirosPessoa[linha, coluna] = 'X';
            Console.WriteLine("Você ACERTOU um navio!");
        }
        else
        {
            tirosPessoa[linha, coluna] = 'O';
            Console.WriteLine("Você acertou a água.");
        }
    }
    public void TurnoDaMaquina()
    {
        int linha = Random.Shared.Next(0, 10);
        int coluna = Random.Shared.Next(0, 10);

        int resultado = Maquina.Atacar(Pessoa.Tabuleiro, linha, coluna);

        Console.Write("A máquina atirou em " + letras[linha] + (coluna + 1) + ": ");

        if (resultado == 4)
        {
            tirosMaquina[linha, coluna] = 'X';   
            Console.WriteLine("acertou seu navio!");
        }
        else
        {
            tirosMaquina[linha, coluna] = 'O';   
            Console.WriteLine("errou.");
        }

        Console.WriteLine();
    }
    public void Iniciar()
    {
        while (Pessoa.Acertos < 30 && Maquina.Acertos < 30)
        {
            if (JogadorDaVez == Pessoa)
            {
                ExibirTabuleiros();
                TurnoPessoa();
                JogadorDaVez = Maquina;
            }
            else
            {
                TurnoDaMaquina();
                JogadorDaVez = Pessoa;
            }
        }

        ExibirTabuleiros();

        if (Pessoa.Acertos == 30)
        {
            Console.WriteLine($"Parabéns {Pessoa.Nome}, você detonou todos os navios do adversário! ");
        }
        else
        {
            Console.WriteLine("Fim da linha meu peixe, você perdeu! A Máquina acabou com todos os seus navio.\n Não soubrou nada...");
        }
    }

}