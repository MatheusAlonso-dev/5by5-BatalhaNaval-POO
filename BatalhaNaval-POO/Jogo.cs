public class Jogo
{
    public Jogador Pessoa;
    public Jogador Maquina;
    public Jogador JogadorDaVez;

    // Converte o índice numérico de uma linha (0 a 9) para a letra
    public string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


    // '~' = não atacada, 'O' = água, 'X' = acertou navio
    // Essas matrizes registram somente os disparos já realizados.
    public char[,] tirosPessoa = new char[10, 10];
    public char[,] tirosMaquina = new char[10, 10];

    // Inicializa os participantes da partida e prepara as matrizes de tiros.
    public Jogo()
    {
        // Solicita o nome antes de criar o jogador humano.
        string nome;
        Console.WriteLine("Digite seu nome: ");
        nome = Console.ReadLine();

        // Cada jogador recebe um tabuleiro próprio, que também posiciona
        // seus navios durante a construção do objeto Tabuleiro.
        Pessoa = new Jogador(nome, new Tabuleiro());
        Maquina = new Jogador("Maquina", new Tabuleiro());

        // A partida começa dando o primeiro turno à pessoa.
        JogadorDaVez = Pessoa;

        // Preenche as duas matrizes com '~', indicando que nenhuma posição
        // foi alvo de um disparo ainda.
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tirosPessoa[i, j] = '~';
                tirosMaquina[i, j] = '~';
            }
        }

    }

    // Mostra na tela o estado dos disparos feitos por cada participante e
    // a quantidade de navios atingidos por cada um.
    public void ExibirTabuleiros()
    {
        // A matriz tirosMaquina representa os disparos da máquina contra
        // o tabuleiro da pessoa, portanto mostra o que a máquina descobriu.
        Console.WriteLine("SEU TABULEIRO (~ = não atacada, O = água, X = acerto)");
        Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");

        // Percorre as dez linhas do tabuleiro.
        for (int i = 0; i < 10; i++)
        {
            Console.Write(letras[i] + "  ");

            // Percorre as dez colunas e imprime o resultado de cada tiro.
            for (int j = 0; j < 10; j++)
            {
                Console.Write(tirosMaquina[i, j] + " ");
            }

            Console.WriteLine();
        }
        Console.WriteLine();

        // A matriz tirosPessoa representa os disparos feitos pela pessoa
        // contra o tabuleiro da máquina.
        Console.WriteLine("TABULEIRO DA MÁQUINA (~ = não atacada, O = água, X = acerto)");
        Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");

        // Repete a mesma forma de impressão para os tiros da pessoa.
        for (int i = 0; i < 10; i++)
        {
            Console.Write(letras[i] + "  ");

            for (int j = 0; j < 10; j++)
            {
                Console.Write(tirosPessoa[i, j] + " ");
            }

            Console.WriteLine();
        }

        // Acertos é atualizado pela classe Jogador quando um tiro retorna 4,
        // que é o código utilizado para representar um navio atingido.
        Console.WriteLine("Acertos -> " + Pessoa.Nome + ": " + Pessoa.Acertos + "/30   Máquina: " + Maquina.Acertos + "/30");
        Console.WriteLine();
    }

    // Lê uma coordenada digitada pela pessoa, transforma sua representação
    // textual em índices da matriz e executa o ataque contra a máquina.
    public void TurnoPessoa()
    {
        // Pede a linha (letra) e a coluna (número) separadamente.
        Console.Write("Digite a letra da linha (A a J): ");
        string letra = Console.ReadLine().ToUpper();

        Console.Write("Digite o número da coluna (1 a 10): ");
        int numero = int.Parse(Console.ReadLine());

        // Procura qual índice numérico corresponde à letra informada.
        int linha = 0;
        for (int i = 0; i < 10; i++)
        {
            if (letras[i] == letra)
            {
                linha = i;
            }
        }

        // As colunas exibidas ao usuário começam em 1, mas os arrays começam
        // em 0; por isso, subtrai-se 1 do número digitado.
        int coluna = numero - 1;

        // O Jogador encaminha o disparo ao tabuleiro adversário e retorna
        // um código que informa se houve acerto ou erro.
        int resultado = Pessoa.Atacar(Maquina.Tabuleiro, linha, coluna);

        // O código 4 significa que a posição continha um navio.
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

    // Realiza automaticamente o turno da máquina escolhendo uma posição
    // aleatória e atacando o tabuleiro da pessoa.
    public void TurnoDaMaquina()
    {
        // Random.Shared gera uma linha e uma coluna entre 0 e 9.
        int linha = Random.Shared.Next(0, 10);
        int coluna = Random.Shared.Next(0, 10);

        // A máquina ataca o tabuleiro da pessoa e recebe o resultado do tiro.
        int resultado = Maquina.Atacar(Pessoa.Tabuleiro, linha, coluna);

        // Converte os índices internos para o formato amigável mostrado
        // ao usuário, como A1, B5 ou J10.
        Console.Write("A máquina atirou em " + letras[linha] + (coluna + 1) + ": ");

        // Registra visualmente o resultado do disparo da máquina.
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

    // Controla o ciclo principal da partida até que um dos jogadores atinja
    // todos os 30 espaços ocupados por navios do adversário.
    public void Iniciar()
    {
        // Enquanto os dois jogadores ainda não chegaram a 30 acertos,
        // a partida continua alternando entre os turnos.
        while (Pessoa.Acertos < 30 && Maquina.Acertos < 30)
        {
            // A comparação identifica de quem é a vez sem criar outra
            // variável de controle para o turno.
            if (JogadorDaVez == Pessoa)
            {
                // A pessoa vê o estado atual, informa seu tiro e, em seguida,
                // o controle passa para a máquina.
                ExibirTabuleiros();
                TurnoPessoa();
                JogadorDaVez = Maquina;
            }
            else
            {
                // A máquina realiza seu tiro e o próximo turno volta para a
                // pessoa.
                TurnoDaMaquina();
                JogadorDaVez = Pessoa;
            }
        }

        // Exibe o estado final antes de informar o vencedor.
        ExibirTabuleiros();

        // Se a pessoa chegou a 30 acertos, todos os navios da máquina foram
        // atingidos; caso contrário, foi a máquina que venceu.
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