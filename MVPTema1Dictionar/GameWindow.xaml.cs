using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Linq;
using System.IO;

namespace MVPTema1Dictionar
{
    public partial class GameWindow : Window
    {
        private List<DictionaryEntry> entries;
        private List<DictionaryEntry> gameEntries;
        private List<string> playerAnswers;
        private int currentIndex;

        public GameWindow()
        {
            InitializeComponent();

            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }

            playerAnswers = new List<string>();
            StartGame();
            ShowWord(); // Show the first word after starting the game
        }

        private void StartGame()
        {
            // reset
            currentIndex = 0;

            // clear
            clueLabel.Text = null;
            clueImage.Source = null;
            playerAnswers.Clear();

            // select 5
            Random random = new Random();
            gameEntries = entries.OrderBy(x => random.Next()).Take(5).ToList();
        }

        private Random random = new Random();

        private void ShowWord()
        {
            // get the current word
            DictionaryEntry entry = gameEntries[currentIndex];

            //description vs image
            if (string.IsNullOrEmpty(entry.ImagePath))
            {
                clueLabel.Text = entry.Description;
                clueImage.Source = null;
            }
            else
            {
                if (random.Next(2) == 0)
                {
                    clueLabel.Text = entry.Description;
                    clueImage.Source = null;
                }
                else
                {
                    clueLabel.Text = null;
                    string imagePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), entry.ImagePath));
                    try
                    {
                        clueImage.Source = new BitmapImage(new Uri(imagePath));
                    }
                    catch (UriFormatException)
                    {
                        imagePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Images", "NothingHere.png"));
                        clueImage.Source = new BitmapImage(new Uri(imagePath));
                    }
                }
            }

            // update the navigation
            previousButton.IsEnabled = currentIndex > 0;
            nextButton.Content = currentIndex < gameEntries.Count - 1 ? "Next" : "Finish";
        }

        private void CheckAnswer()
        {
            string answer = guessTextBox.Text;
            string correctAnswer = gameEntries[currentIndex].Word;

            if (answer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show("Wrong! The correct answer is: " + correctAnswer);
            }

            playerAnswers.Add(answer);
            guessTextBox.Clear();
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();

            if (currentIndex < gameEntries.Count - 1)
            {
                currentIndex++;
                ShowWord();
            }
            else
            {
                // end the game
                int correctAnswers = 0;
                for (int i = 0; i < gameEntries.Count; i++)
                {
                    if (playerAnswers[i] == gameEntries[i].Word)
                    {
                        correctAnswers++;
                    }
                }
                MessageBox.Show("Game over, your score is " + correctAnswers);
                StartGame();
                ShowWord();
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                //previous word
                currentIndex--;
                ShowWord();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow prevWindow = new MainWindow();
            prevWindow.Show();
            this.Close();
        }
    }
}
