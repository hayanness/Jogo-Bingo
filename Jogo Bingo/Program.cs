using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    class Jogador
    {
        public string nome;
        public int idade;
        public char sexo;
        private Cartela[] cartelas;
        private Cartela[] copiaCartela;

        public bool desclassificado = false;
        public bool ganhador = false;
        Random r = new Random();

        public Jogador(string nome, int idade, char sexo, int numeroCartelas)
        {
            this.nome = nome;
            this.idade = idade;
            this.sexo = sexo;
            cartelas = new Cartela[numeroCartelas];
            copiaCartela = new Cartela[numeroCartelas];

            for (int i = 0; i < numeroCartelas; i++)
            {
                cartelas[i] = new Cartela(r);
            }

            for (int i = 0; i < numeroCartelas; i++)
            {
                copiaCartela[i] = cartelas[i];
                Console.WriteLine("Imprimindo cartela " + i);
                copiaCartela[i].MostrarCartela();
            }
        }
        public Cartela[] ObterCopia()
        {
            return copiaCartela;
        }
        public Cartela[] ObterCartelas()
        {
            return cartelas;
        }
        public int NumeroCartelasClassificadas()
        {
            int numCartelasClassificadas = 0;
            for (int i = 0; i < cartelas.Length; i++)
            {
                if (cartelas[i].desclassificada == false) numCartelasClassificadas++;
            }
            return numCartelasClassificadas;
        }
        public void Imprimir()
        {
            string status = ganhador ? "ganhador" : "desclassificado";
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Sexo: {sexo}");
            Console.WriteLine($"Status: {status}");
        }
    }
    class Cartela
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
    class Ranking
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
    internal class Program
    {
        static int[] numerosSorteados = new int[75];
        static int numerosSorteadosContador = 0;
        static int ObterNumeroDeCartelas()
        {
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero) || numero < 1 || numero > 4)//converte a entrada do usuário para um número inteiro
            {
                Console.Write("Por favor, digite um número válido de cartelas (entre 1 e 4): ");
            }
            return numero;
        }
        static int SortearNumero()
        {
            Random random = new Random();

            while (true)
            {
                int numeroSorteado = random.Next(1, 76);

                //verificar se o número já foi sorteado
                bool numeroRepetido = false;
                for (int i = 0; i < numerosSorteadosContador; i++)
                {
                    if (numerosSorteados[i] == numeroSorteado)
                    {
                        numeroRepetido = true;
                        break;
                    }
                }

                //se o número não foi repetido, vai retorna-lo
                if (!numeroRepetido)
                {
                    numerosSorteados[numerosSorteadosContador] = numeroSorteado;
                    numerosSorteadosContador++;

                    return numeroSorteado;
                }
            }
        }

        //verificar se há uma sequência na cartela
        static bool VerificarSequencia(int[,] matriz, int[] numerosSorteados, int quantSorteado)
        {
            for (int k = 0; k < matriz.GetLength(0); k++)
            {
                int contadorCerto = 0;
                for (int m = 0; m < matriz.GetLength(1); m++)
                {
                    if (matriz[k, m] == 0)
                    {
                        for (int l = 0; l < quantSorteado; l++)
                        {
                            if (numerosSorteados[l] == matriz[k, m])
                            {
                                contadorCerto++;
                            }
                        }
                    }

                    if (contadorCerto == 5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //transpor a cartela para verificar as colunas 
        static int[,] TransporMatriz(int[,] matriz)
        {
            int linhas = matriz.GetLength(0);
            int colunas = matriz.GetLength(1);

            int[,] transposta = new int[colunas, linhas];

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    transposta[j, i] = matriz[i, j];
                }
            }

            return transposta;
        }

        static void Main(string[] args)
        {
            Console.Write("Informe quantas pessoas irão jogar:");
            int quantidadeJogadores = int.Parse(Console.ReadLine());

            // validar a quantidade de jogadores
            if (quantidadeJogadores < 2 || quantidadeJogadores > 5)
            {
                Console.WriteLine("Número inválido de jogadores. O jogo requer entre 2 e 5 jogadores.");
                return;
            }

            //criando vetor da classe Jogador
            Jogador[] jogadores = new Jogador[quantidadeJogadores];
            int jogadoresAtivos = 0;

            for (int i = 0; i < quantidadeJogadores; i++)
            {
                Console.WriteLine($"\nDiga o nome do {i + 1}° jogador, sua idade e o seu sexo (F or M):");
                string nome = Console.ReadLine();
                int idade = int.Parse(Console.ReadLine());
                char sexo = char.Parse(Console.ReadLine());

                //permitir que cada jogador escolha o número de cartelas (1 a 4).
                Console.Write($"Quantas cartelas {nome} deseja jogar? (entre 1 e 4): ");
                int numeroDeCartelas = ObterNumeroDeCartelas();

                jogadores[i] = new Jogador(nome, idade, sexo, numeroDeCartelas);
                jogadoresAtivos++;
            }


            Console.WriteLine("\n\nO JOGO VAI COMEÇAR!\n\n");

            //inicializa o Ranking
            Ranking ranking = new Ranking(quantidadeJogadores);

            //sortear numeros
            bool fimDeJogo = false;
            int contaSorteado= 0;
            while (!fimDeJogo)
            {
                int numSorteado = SortearNumero();
                contaSorteado++;
                numerosSorteados[contaSorteado - 1] = numSorteado;
                Console.WriteLine("\nNúmero sorteado:" + numSorteado);
                for (int i = 0; i < quantidadeJogadores; i++)
                {
                    if (jogadores[i] != null)
                    {
                        Cartela[] cartelas = jogadores[i].ObterCopia();

                        for (int j = 0; j < cartelas.Length; j++)
                        {
                            if (cartelas[j].desclassificada == false)
                            {
                                Console.Write($"\nJogador {jogadores[i].nome}, possui {numSorteado} em sua cartela? [1-Sim || 2-Não]");
                                Console.WriteLine($"\nCartela {j + 1}:");
                                cartelas[j].MostrarCartela();

                                char respostaJogador = char.Parse(Console.ReadLine());

                                if (respostaJogador == '1')
                                {
                                    Console.Write("Qual a linha se encontra o número? (0 a 4)");
                                    int linhaSorteada = int.Parse(Console.ReadLine());
                                    Console.Write("Qual a coluna se encontra o número? (0 a 4)");
                                    int colunaSorteada = int.Parse(Console.ReadLine());

                                    cartelas[j].AlterarCartela(linhaSorteada, colunaSorteada);
                                }
                            }
                        }
                    }

                    if (contaSorteado >= 4 && jogadoresAtivos>1)
                    {
                        Console.Write("Gritar bingo [1-Sim || 2-Não] ");
                        char respostaBingo = char.Parse(Console.ReadLine());

                        if (respostaBingo == '1')
                        {
                            Console.WriteLine("Informe o número da cartela que deseja verificar:");

                            int cartelaParaVerificacao = int.Parse(Console.ReadLine());

                            Cartela cartela = jogadores[i].ObterCartelas()[cartelaParaVerificacao - 1];

                            Cartela copia = jogadores[i].ObterCopia()[cartelaParaVerificacao - 1];

                            //verifica linha da cartela
                            bool linhaCompleta = VerificarSequencia(copia.cartela, numerosSorteados, contaSorteado);

                            //verifica coluna da cartela
                            bool colunaCompleta = VerificarSequencia(TransporMatriz(copia.cartela), numerosSorteados, contaSorteado);

                            if (linhaCompleta || colunaCompleta)
                            {
                                Console.WriteLine("Deu bingo");
                                ranking.AdicionarGanhador(jogadores[i]);
                            }

                            else
                            {
                                cartela.desclassificada = true;
                                if (jogadores[i].NumeroCartelasClassificadas() == 0)
                                {
                                    ranking.AdicionarDesclassificado(jogadores[i]);
                                    Console.WriteLine("Você perdeu o jogo, {0} !", jogadores[i].nome);
                                    jogadores[i] = null;
                                    jogadoresAtivos--;
                                    Jogador vencedor = null;
                                    if (jogadoresAtivos == 1)
                                    {
                                        fimDeJogo = true;
                                        for (int n = 0; n < quantidadeJogadores; n++)
                                        {
                                            if (jogadores[n] != null)
                                            {
                                                vencedor = jogadores[n];
                                            }
                                        }
                                    }

                                    vencedor.ganhador = true;
                                    if (vencedor != null) ranking.AdicionarGanhador(vencedor);
                                }
                                else
                                {
                                    Console.WriteLine("Não foi bingo, sua cartela foi desclassificada !");
                                }
                            }
                        }
                    }
                }
            }
            ranking.Imprimir();
        }
    }
}
