public class Jogador
{
    public string Nome { get; set; }
    public Tabuleiro Tabuleiro { get; set; }
    public int Acertos { get; private set; }

    public Jogador(string nome, Tabuleiro tabuleiro)
    {
        Nome = nome;
        Tabuleiro = tabuleiro;
        Acertos = 0;
    }

    public int Atacar(Tabuleiro tabuleiroAdversario, int linha, int coluna)
    {
        Jogada jogada = new Jogada(linha, coluna);

        int resultado = jogada.Executar(tabuleiroAdversario);

        if (resultado == 4)
        {
            Acertos++;
        }

        return resultado;
    }
}