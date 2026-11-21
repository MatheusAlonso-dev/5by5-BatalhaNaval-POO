public class Jogador
{
    public string Nome { get; set; }
    public int Pontuacao { get; set; }

    public void RegistrarAcerto()
    {
        Pontuacao++;
    }
}
