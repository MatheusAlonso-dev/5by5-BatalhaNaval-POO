public class Jogada
{
    public int Linha { get; set; }
    public int Coluna { get; set; }

    public Jogada(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }

    public int Executar(Tabuleiro tabuleiro)
    {
        if (Linha < 0 || Linha >= 10 ||
            Coluna < 0 || Coluna >= 10)
        {
            return 0;
        }

        return tabuleiro.ReceberTiro(Linha, Coluna);
    }
}
