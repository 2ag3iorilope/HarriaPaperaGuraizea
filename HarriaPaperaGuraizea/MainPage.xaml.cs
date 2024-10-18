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

            // Aldatu ordengailuaren irudia
            ComputerChoiceImage.Source = computerChoice; 

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

            PlayerScoreLabel.Text = $"Erabiltzailearen Puntuak: {playerScore}";
            ComputerScoreLabel.Text = $"Ordenagailuaren Puntuak: {computerScore}";
        }

        private void CheckForWinner()
        {
            if (playerScore >= 10)
            {
                DisplayAlert("Irabazlea", "¡Erabiltzaileak irabazi du!", "Ados");
                ResetGame();
            }
            else if (computerScore >= 10)
            {
                DisplayAlert("Irabazlea", "¡ordenagailua irabazi du!", "Ados");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            playerScore = 0;
            computerScore = 0;
            PlayerScoreLabel.Text = "Erabiltzailearen Puntuak: 0";
            ComputerScoreLabel.Text = "Ordenagailuaren puntuak: 0";
            ComputerChoiceImage.Source = null;
        }

        private void OnFinishButtonClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new ContentPage { Content = new Label { Text = "Jokoa Amaitu Da Agur!" } };
        }
    }
}
