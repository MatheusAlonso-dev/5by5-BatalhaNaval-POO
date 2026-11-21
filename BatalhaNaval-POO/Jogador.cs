public class Jogador
{
    public string Nome { get; set; }

    public Tabuleiro Tabuleiro { get; set; }

    public Jogador(string nome, Tabuleiro tabuleiro)
    {
        Nome = nome;
        Tabuleiro = tabuleiro;
    }
}
