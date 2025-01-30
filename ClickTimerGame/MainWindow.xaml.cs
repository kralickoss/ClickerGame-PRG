using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Klikacka
{
    public partial class MainWindow : Window
    {
        private GameEngine gameEngine;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Skryj úvodní menu a zobraz herní obrazovku
            MainMenu.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;

            // Nastav vlastní kurzor
            SetCustomCursor("Images/palica.cur");

            // Inicializuj hru
            StartGame();
        }

        private void StartGame()
        {
            gameEngine = new GameEngine(GameCanvas);
            gameEngine.ScoreUpdated += UpdateScore;
            gameEngine.GameOver += OnGameOver;
        }

        private void UpdateScore(int newScore)
        {
            ScoreText.Text = $"Skóre: {newScore}";
        }

        private void OnGameOver()
        {
            MessageBox.Show("Hra skončila! Promarnil jsi příliš mnoho objektů.", "Konec hry", MessageBoxButton.OK, MessageBoxImage.Information);

            GameCanvas.Background = Brushes.Gray;
            GameCanvas.Children.Clear();

            // Reset hry a návrat do menu
            MainMenu.Visibility = Visibility.Visible;
            GameScreen.Visibility = Visibility.Collapsed;

            // Reset kurzoru na výchozí
            Mouse.OverrideCursor = null;
        }

        private void SetCustomCursor(string imagePath)
        {
            try
            {
                var cursorStream = Application.GetResourceStream(new Uri(imagePath, UriKind.Relative))?.Stream;
                if (cursorStream != null)
                {
                    Mouse.OverrideCursor = new Cursor(cursorStream);
                }
                else
                {
                    Console.WriteLine("Cursor stream is null. Check the image path.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting custom cursor: {ex.Message}");
            }
        }

    }
}
