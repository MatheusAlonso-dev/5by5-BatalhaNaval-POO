public class Jogador
{
    public string Nome { get; set; }
    public int Pontuacao { get; set; }

    public void RegistrarAcerto()
    {
        Pontuacao++;
    }

    public int Atacar(Tabuleiro tabuleiro, int linha, int coluna)
    {
        int resultado = tabuleiro.AtirarNoTabuleiro(linha, coluna);

        if (resultado == 1)
        {
            RegistrarAcerto();
        }

        return resultado;
    }
}