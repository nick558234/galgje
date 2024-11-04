using System.Windows;

namespace galgje
{
    public partial class MainWindow : Window
    {
        private string wordToGuess;
        private char[] guessedWord;
        private int incorrectGuesses;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeGame()
        {
            // Maak een array van underscores die de nog niet geraden letters vertegenwoordigen
            guessedWord = new string('_', wordToGuess.Length).ToCharArray();
            incorrectGuesses = 0;
            UpdateWordDisplay();
            ResetHangmanFigure();
        }

        private void submitWord_Click(object sender, RoutedEventArgs e)
        {
            wordToGuess = guessWord.Text;
            guessWord.Visibility = Visibility.Hidden;
            submitWord.Visibility = Visibility.Hidden;
            InitializeGame();
        }

        private void UpdateWordDisplay()
        {
            WordDisplay.Text = string.Join(" ", guessedWord);
        }

        private void SubmitGuessButton_Click(object sender, RoutedEventArgs e)
        {
            string guess = GuessInput.Text.ToLower();
            if (guess.Length == 1 && char.IsLetter(guess[0]))
            {
                ProcessGuess(guess[0]);
                GuessInput.Clear();
            }
            else
            {
                MessageDisplay.Text = "Please enter a single letter.";
            }
        }

        private void ProcessGuess(char guessedChar)
        {
            bool correctGuess = false;

            // Loop door elk karakter van het te raden woord
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                // Controleer of de geraden letter overeenkomt met een letter in het te raden woord
                if (wordToGuess[i] == guessedChar)
                {
                    // Vervang de underscore door de geraden letter
                    guessedWord[i] = guessedChar;
                    correctGuess = true;
                }
            }

            if (correctGuess)
            {
                UpdateWordDisplay();
                MessageDisplay.Text = "Correct guess!";
                // Controleer of het hele woord is geraden
                if (new string(guessedWord) == wordToGuess)
                {
                    MessageBox.Show("You win! The word was: " + wordToGuess);
                    InitializeGame();
                }
            }
            else
            {
                incorrectGuesses++;
                UpdateHangmanFigure();
                MessageDisplay.Text = "Incorrect guess. Try again.";
            }
        }

        private void UpdateHangmanFigure()
        {
            // Toon delen van de galgfiguur op basis van het aantal foute gokken
            switch (incorrectGuesses)
            {
                case 1: Head.Visibility = Visibility.Visible; break;
                case 2: Body.Visibility = Visibility.Visible; break;
                case 3: LeftArm.Visibility = Visibility.Visible; break;
                case 4: RightArm.Visibility = Visibility.Visible; break;
                case 5: LeftLeg.Visibility = Visibility.Visible; break;
                case 6:
                    RightLeg.Visibility = Visibility.Visible;
                    MessageBox.Show("You lost! The word was: " + wordToGuess);
                    InitializeGame();
                    break;
            }
        }

        private void ResetHangmanFigure()
        {
            Head.Visibility = Visibility.Hidden;
            Body.Visibility = Visibility.Hidden;
            LeftArm.Visibility = Visibility.Hidden;
            RightArm.Visibility = Visibility.Hidden;
            LeftLeg.Visibility = Visibility.Hidden;
            RightLeg.Visibility = Visibility.Hidden;
        }
    }
}
