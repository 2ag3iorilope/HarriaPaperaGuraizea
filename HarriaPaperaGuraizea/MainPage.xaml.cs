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

        /// <summary>
        /// Erabiltzaileak irudia hautatzen duenean metodo hau exekutatzen da.
        /// Irudia erabiltzailearen aukeraren arabera detektatzen du, ordenagailuaren aukera sortzen du,
        /// eta puntuazioak eguneratzen ditu.
        /// </summary>
        private void OnPlayerChoiceTapped(object sender, EventArgs e)
        {
         
            string playerChoice = (sender as Image).Source.ToString();

            string computerChoice = GetComputerChoice();

            ComputerChoiceImage.Source = computerChoice;

           
            UpdateScores(playerChoice, computerChoice);

          
            CheckForWinner();
        }

        /// <summary>
        /// Ordenagailuaren aukera ausaz sortzen du. Aukera hauek dira:
        /// 0 = "piedra.png", 1 = "eskua.png" (papel), 2 = "tijera.png".
        /// </summary>
        /// <returns>Ordenagailuaren aukeraren irudiaren izena.</returns>
        private string GetComputerChoice()
        {
           
            int choice = random.Next(3);

         
            return choice switch
            {
                0 => "piedra.png",
                1 => "eskua.png",
                2 => "tijera.png",
                _ => throw new InvalidOperationException() // Aukera baliogabea gertatuz gero errorea jaurti
            };
        }

        /// <summary>
        /// Erabiltzailearen eta ordenagailuaren aukeren arabera puntuazioak eguneratzen ditu.
        /// Jokalari bakoitzak puntu bat irabazten du arau klasikoen arabera.
        /// </summary>
        /// <param name="playerChoice">Erabiltzailearen aukera.</param>
        /// <param name="computerChoice">Ordenagailuaren aukera.</param>
        private void UpdateScores(string playerChoice, string computerChoice)
        {
            // Irabazteko baldintzak erabiltzailearentzat
            if (playerChoice.Contains("piedra") && computerChoice.Contains("tijera") ||
                playerChoice.Contains("eskua") && computerChoice.Contains("piedra") ||
                playerChoice.Contains("tijera") && computerChoice.Contains("eskua"))
            {
                playerScore++; // Erabiltzailearen puntuazioa igo
            }
          
            else if (computerChoice.Contains("piedra") && playerChoice.Contains("tijera") ||
                     computerChoice.Contains("eskua") && playerChoice.Contains("piedra") ||
                     computerChoice.Contains("tijera") && playerChoice.Contains("eskua"))
            {
                computerScore++; // Ordenagailuaren puntuazioa igo
            }

           
            PlayerScoreLabel.Text = $"Erabiltzailearen Puntuak: {playerScore}";
            ComputerScoreLabel.Text = $"Ordenagailuaren Puntuak: {computerScore}";
        }

        /// <summary>
        /// Jokoaren amaiera egiaztatzen du. Norbait 10 puntura iristen bada,
        /// irabazlearen mezua bistaratzen du eta jokoa berrabiarazten du.
        /// </summary>
        private void CheckForWinner()
        {
            if (playerScore >= 10) 
            {
                DisplayAlert("Irabazlea", "¡Erabiltzaileak irabazi du!", "Ados");
                ResetGame(); 
            }
            else if (computerScore >= 10) 
            {
                DisplayAlert("Irabazlea", "¡Ordenagailuak irabazi du!", "Ados");
                ResetGame(); 
            }
        }

        /// <summary>
        /// Jokoa berrabiarazten du puntuazioak 0ra jarriz eta irudia ezabatuz.
        /// </summary>
        private void ResetGame()
        {
            playerScore = 0; 
            computerScore = 0; 

          
            PlayerScoreLabel.Text = "Erabiltzailearen Puntuak: 0";
            ComputerScoreLabel.Text = "Ordenagailuaren Puntuak: 0";

           
            ComputerChoiceImage.Source = null;
        }

        /// <summary>
        /// "Amaitu" botoia sakatzen denean exekutatzen da. Jokoa amaitzen du
        /// eta mezua bistaratzen du.
        /// </summary>
        private void OnFinishButtonClicked(object sender, EventArgs e)
        {
           
            Application.Current!.MainPage = new ContentPage
            {
                Content = new Label { Text = "Jokoa Amaitu Da Agur!" }
            };
        }
    }
}