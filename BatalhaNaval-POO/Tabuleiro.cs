public class Tabuleiro
{
    Random random = new Random();
    private int[,] Matriz = new int[10, 10];

    public Tabuleiro()
    {
        PopularMatrizComAgua();
        InserirNaviosNoTabuleiro();
    }
    private void PopularMatrizComAgua()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Matriz[i, j] = 1; //1 = agua
            }
        }
    }
    private void InserirNaviosNoTabuleiro()
    {
        int qtdNaviosInseridos = 0;
        int tamanhoNavio = 5; // tamanho do primeiro navio a ser inserido, ao longo da função é alterado de acordo com a quantidade necessaria de cada.
        int[] navio = new int[2];
        int linhaNavio;
        int colunaNavio;
        int direcao;

        while(qtdNaviosInseridos < 10)
        {
            bool inseriuNavio = false;

            if (qtdNaviosInseridos < 1)
                tamanhoNavio = 5;
            else if (qtdNaviosInseridos < 3)
                tamanhoNavio = 4;
            else if (qtdNaviosInseridos < 6)
                tamanhoNavio = 3;
            else
                tamanhoNavio = 2;

            navio[1] = tamanhoNavio;

            while (!inseriuNavio)
            {
                int tamanhoTabuleiro = 10;
                //linha e coluna
                linhaNavio = random.Next(0, 10);
                colunaNavio = random.Next(0, 10);

                direcao = random.Next(0, 2);
                navio[0] = direcao;
                int contaCasaLivre = 0;

                if (direcao == 0)
                {
                    if (colunaNavio + tamanhoNavio <= tamanhoTabuleiro)
                    {
                        for (int i = colunaNavio; i < colunaNavio + tamanhoNavio; i++)
                        {
                            if (Matriz[linhaNavio, i] == 1)
                                contaCasaLivre++;
                        }

                        if (contaCasaLivre == tamanhoNavio)
                        {
                            for (int i = colunaNavio; i < colunaNavio + tamanhoNavio; i++)
                            {
                                Matriz[linhaNavio, i] = 4;
                            }
                            inseriuNavio = true;
                            qtdNaviosInseridos++;
                        }
                    }

                }
                else
                {
                    if (linhaNavio + tamanhoNavio <= tamanhoTabuleiro)
                    {
                        for (int i = linhaNavio; i < linhaNavio + tamanhoNavio; i++)
                        {
                            if (Matriz[i, colunaNavio] == 1)
                                contaCasaLivre++;
                        }

                        if (contaCasaLivre == tamanhoNavio)
                        {
                            for (int i = linhaNavio; i < linhaNavio + tamanhoNavio; i++)
                            {
                                Matriz[i, colunaNavio] = 4;
                            }
                            inseriuNavio = true;
                            qtdNaviosInseridos++;
                        }
                    }
                }

            }
        }
    }
    public void ImprimirTabuleiroDebug()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Console.Write($"{Matriz[i, j]} ");
            }
            Console.WriteLine();
        }
    }


}

