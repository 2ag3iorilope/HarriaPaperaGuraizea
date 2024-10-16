using System;
using Microsoft.Maui.Controls;

namespace HarriaPaperaGuraizea
{
    public partial class MainPage : ContentPage
    {
        private int playerScore = 0;
        private int computerScore = 0;
        private readonly Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
          
            
        }

        private void OnPlayerChoiceTapped(object sender, EventArgs e)
        {
            string playerChoice = (sender as Image).Source.ToString();
            string computerChoice = GetComputerChoice();

            // Cambia el texto por la imagen del ordenador
            ComputerChoiceImage.Source = computerChoice; // Actualiza aquí para usar la imagen

            UpdateScores(playerChoice, computerChoice);
            CheckForWinner();
        }

        private string GetComputerChoice()
        {
            int choice = random.Next(3); // 0 = piedra, 1 = papel, 2 = tijera
            return choice switch
            {
                0 => "piedra.png",
                1 => "eskua.png",
                2 => "tijera.png",
                _ => throw new InvalidOperationException()
            };
        }

        private void UpdateScores(string playerChoice, string computerChoice)
        {
            if (playerChoice.Contains("piedra") && computerChoice.Contains("tijera") ||
                playerChoice.Contains("papel") && computerChoice.Contains("piedra") ||
                playerChoice.Contains("tijera") && computerChoice.Contains("papel"))
            {
                playerScore++;
            }
            else if (computerChoice.Contains("piedra") && playerChoice.Contains("tijera") ||
                     computerChoice.Contains("papel") && playerChoice.Contains("piedra") ||
                     computerChoice.Contains("tijera") && playerChoice.Contains("papel"))
            {
                computerScore++;
            }

            PlayerScoreLabel.Text = $"Puntos Jugador: {playerScore}";
            ComputerScoreLabel.Text = $"Puntos Ordenador: {computerScore}";
        }

        private void CheckForWinner()
        {
            if (playerScore >= 10)
            {
                DisplayAlert("Ganador", "¡El jugador ha ganado!", "Aceptar");
                ResetGame();
            }
            else if (computerScore >= 10)
            {
                DisplayAlert("Ganador", "¡El ordenador ha ganado!", "Aceptar");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            playerScore = 0;
            computerScore = 0;
            PlayerScoreLabel.Text = "Puntos Jugador: 0";
            ComputerScoreLabel.Text = "Puntos Ordenador: 0";
            ComputerChoiceImage.Source = null;
        }

        private void OnFinishButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ContentPage { Content = new Label { Text = "Juego Finalizado" } };
        }
    }
}
