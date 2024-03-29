﻿
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	 class Game
	{
        public static int WIDTH = 12;
        public static int UPPER = 35;
        public static int LOWER = 25;

        public static Consulta consulta = new Consulta();

        private Jugador player1 = new ComputerPlayer(consulta);
        private Jugador player2 = new HumanPlayer(consulta);
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool juegaHumano = false;


		public Game()
		{
			var rnd = new Random();
			limite = rnd.Next(LOWER, UPPER);
			
			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
			
			for (int i = 1; i <= WIDTH; i++) 
            {
				if (!naipesHuman.Contains(i)) 
                {
					naipesComputer.Add(i);
				}
			}
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
		}
		
		private void printScreen()
		{
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("LIMITE: ");
            Console.ResetColor();
            Console.Write("el límite actual es de: " + limite.ToString() + "\n\n");
		}
		
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}		
		
		private void printWinner()
		{
			if (!juegaHumano) 
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\nJUEGO TERMINADO: ");
				Console.WriteLine("\nHa ganado el Humano!");
                Console.ResetColor();
			} 
            else 
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.Write("\nJUEGO TERMINADO: ");
                Console.Write("Ha ganado la Inteligencia Artificial!");
                Console.ResetColor();
			}
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void play()
		{
			while (!this.fin()) 
            {
				this.printScreen();
				this.turn(player2, player1, naipesHuman); // Juega el usuario
				if (!this.fin()) 
                {
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			this.printWinner();
		}
	}
}
