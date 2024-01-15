using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Ranking
    {
        private Jogador[] jogadores;
        bool[] posicoesOcupadas;

        public Ranking(int numJogadores)
        {
            jogadores = new Jogador[numJogadores];
            posicoesOcupadas = new bool[numJogadores];

        }

        public void AdicionarGanhador(Jogador jogador)
        {
            for (int i = 0; i < posicoesOcupadas.Length; i++)
            {
                bool posicaoOcupada = posicoesOcupadas[i];
                if (!posicaoOcupada)
                {
                    jogadores[i] = jogador;
                    posicoesOcupadas[i] = true;
                    break;
                }
            }
        }

        public void AdicionarDesclassificado(Jogador jogador)
        {
            for (int i = posicoesOcupadas.Length - 1; i >= 0; i--)
            {
                bool posicaoOcupada = posicoesOcupadas[i];
                if (!posicaoOcupada)
                {
                    jogadores[i] = jogador;
                    posicoesOcupadas[i] = true;
                    break;
                }
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("\n===== RANKING =====");
            for (int i = 0; i < jogadores.Length; i++)
            {
                Jogador jogador = jogadores[i];
                if (jogador != null)
                {
                    Console.WriteLine($"{i + 1}˚ Colocado:");
                    jogador.Imprimir();
                    Console.WriteLine();
                }
            }
        }

    }
}
