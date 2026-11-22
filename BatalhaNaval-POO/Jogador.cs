public class Jogador
{
    // Armazena o nome do jogador.
    public string Nome { get; set; }

    // Guarda a referência ao tabuleiro pertencente ao jogador.
    public Tabuleiro Tabuleiro { get; set; }

    // Armazena a quantidade de navios atingidos pelo jogador.
    // O private set impede que outras classes alterem diretamente esse valor.
    public int Acertos { get; private set; }

    // Construtor responsável por inicializar os dados do jogador.
    public Jogador(string nome, Tabuleiro tabuleiro)
    {
        Nome = nome;
        Tabuleiro = tabuleiro;
        Acertos = 0;
    }

    // Realiza um ataque no tabuleiro do adversário utilizando as coordenadas recebidas.
    public int Atacar(Tabuleiro tabuleiroAdversario, int linha, int coluna)
    {
        // Cria um objeto Jogada com as coordenadas informadas.
        Jogada jogada = new Jogada(linha, coluna);

        // Executa a jogada no tabuleiro adversário e armazena o resultado.
        int resultado = jogada.Executar(tabuleiroAdversario);

        // Verifica se o resultado foi 4, indicando que um navio foi atingido.
        if (resultado == 4)
        {
            // Incrementa a quantidade de acertos do jogador.
            Acertos++;
        }

        // Retorna o resultado do ataque para quem chamou o método.
        return resultado;
    }
}