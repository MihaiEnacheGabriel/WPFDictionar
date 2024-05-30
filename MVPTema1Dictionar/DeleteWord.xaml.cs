using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MVPTema1Dictionar
{
    public partial class DeleteWord : Window
    {
        private DictionaryEntry currentEntry;

        public DeleteWord()
        {
            InitializeComponent();
            Loaded += DeleteWord_Loaded;
        }
        private void DeleteWord_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                List<DictionaryEntry> entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
                wordComboBox.ItemsSource = entries;
            }
        }

        private void WordComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentEntry = wordComboBox.SelectedItem as DictionaryEntry;
        }
        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            if (currentEntry == null)
            {
                MessageBox.Show("Please select a word.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string word = currentEntry.Word;

            if (string.IsNullOrEmpty(word))
            {
                MessageBox.Show("Please enter a word.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<DictionaryEntry> entries = new List<DictionaryEntry>();
            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }

            DictionaryEntry entry = entries.FirstOrDefault(x => x.Word == word);
            if (entry == null)
            {
                MessageBox.Show("This word does not exist in the dictionary.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the word '{word}'?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entries.Remove(entry);

                string newJson = JsonConvert.SerializeObject(entries, Formatting.Indented);
                File.WriteAllText("dictionary.json", newJson);

                MessageBox.Show("Word deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyDictionary prevWindow = new ModifyDictionary();
            prevWindow.Show();
            this.Close();
        }
    }
}
