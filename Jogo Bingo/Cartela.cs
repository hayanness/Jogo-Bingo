using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Cartela
    {
        public int[,] cartela = new int[5, 5];

        public bool desclassificada = false;
        public bool premiada = false;

        public Cartela(Random random)
        {
            GerarCartela(random);
        }
        private void GerarCartela(Random random)
        {
            PreencherColuna(0, 1, 15, random);
            PreencherColuna(1, 16, 30, random);
            PreencherColuna(2, 31, 45, random);
            PreencherColuna(3, 46, 60, random);
            PreencherColuna(4, 61, 75, random);
        }
        private void PreencherColuna(int coluna, int inicio, int final, Random random)
        {
            for (int i = 0; i < 5; i++)
            {
                int numero;
                bool ehrepetido;
                do
                {
                    numero = random.Next(inicio, final + 1);
                    ehrepetido = VerificarNumeroRepetido(coluna, i, numero);

                } while (ehrepetido == true);

                if (coluna == 2 && i == 2)
                {
                    cartela[i, coluna] = 00;
                }
                else
                {
                    cartela[i, coluna] = numero;
                }
            }
        }
        private bool VerificarNumeroRepetido(int coluna, int linha, int numero)
        {
            for (int i = 0; i < linha; i++)
            {
                if (cartela[i, coluna] == numero)
                {
                    return true;
                }
            }

            return false;
        }
        public void MostrarCartela()
        {
            Console.WriteLine($"B I N G O:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{cartela[i, j]:D2} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void AlterarCartela(int linha, int coluna)
        {
            cartela[linha, coluna] = 00;
        }

    }
}
