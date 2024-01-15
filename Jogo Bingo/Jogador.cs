using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Jogador
    {
        public string nome;
        public int idade;
        public char sexo;
        private Cartela[] cartelas;
        private Cartela[] copiaCartela;

        public bool desclassificado = false;
        public bool ganhador = false;
        public bool estaNoRanking = false;
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
}
