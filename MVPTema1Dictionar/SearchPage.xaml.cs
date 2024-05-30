using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MVPTema1Dictionar
{
    public partial class SearchWord : Window
    {
        private List<DictionaryEntry> entries;
        private List<DictionaryEntry> filteredEntries;

        public SearchWord()
        {
            InitializeComponent();

            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }
            filteredEntries = new List<DictionaryEntry>(entries);

            categoryComboBox.ItemsSource = entries.Select(x => x.Category).Distinct().ToList();
        }


        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = categoryComboBox.SelectedItem as string;
            filteredEntries = entries.Where(x => x.Category == selectedCategory).ToList();
        }

        private void WordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string enteredText = wordTextBox.Text;
            suggestionListBox.ItemsSource = filteredEntries.Where(x => x.Word.StartsWith(enteredText)).ToList();

            if (!string.IsNullOrEmpty(enteredText))
            {
                suggestionPopup.IsOpen = true;
            }
            else
            {
                suggestionPopup.IsOpen = false;
            }
        }

        private void SuggestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DictionaryEntry selectedEntry = suggestionListBox.SelectedItem as DictionaryEntry;
            if (selectedEntry != null)
            {
                wordTextBlock.Text = "Word: " + selectedEntry.Word;
                categoryTextBlock.Text = "Category: " + selectedEntry.Category;
                descriptionTextBlock.Text = "Description: " + selectedEntry.Description;

                string imagePath;
                if (string.IsNullOrEmpty(selectedEntry.ImagePath))
                {
                    imagePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Images", "NothingHere.png"));
                }
                else
                {
                    imagePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), selectedEntry.ImagePath));
                }

                try
                {
                    wordImage.Source = new BitmapImage(new Uri(imagePath));
                }
                catch (UriFormatException)
                {
                    imagePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Images", "NothingHere.png"));
                    wordImage.Source = new BitmapImage(new Uri(imagePath));
                }

                suggestionPopup.IsOpen = false;
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
