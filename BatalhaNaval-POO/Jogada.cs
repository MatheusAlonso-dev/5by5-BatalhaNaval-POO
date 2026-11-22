public class Jogada
{
    // Armazena a linha escolhida para o ataque.
    public int Linha { get; set; }

    // Armazena a coluna escolhida para o ataque.
    public int Coluna { get; set; }

    // Construtor que recebe as coordenadas da jogada.
    public Jogada(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }

    // Executa a jogada no tabuleiro recebido como parâmetro.
    public int Executar(Tabuleiro tabuleiro)
    {
        // Verifica se a linha ou a coluna estão fora dos limites do tabuleiro.
        if (Linha < 0 || Linha >= 10 ||
            Coluna < 0 || Coluna >= 10)
        {
            // Retorna 0 caso as coordenadas sejam inválidas.
            return 0;
        }

        // Envia as coordenadas para o tabuleiro verificar o resultado do tiro.
        return tabuleiro.ReceberTiro(Linha, Coluna);
    }
}
