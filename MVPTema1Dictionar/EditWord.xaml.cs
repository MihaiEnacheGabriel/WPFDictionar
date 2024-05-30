using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MVPTema1Dictionar
{
    public partial class EditWord : Window
    {
        private DictionaryEntry currentEntry;
        private List<DictionaryEntry> entries;

        public EditWord()
        {
            InitializeComponent();

            // Load the list of words from the file
            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }
            wordComboBox.ItemsSource = entries;
        }

        private void WordComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentEntry = wordComboBox.SelectedItem as DictionaryEntry;

            if (currentEntry != null)
            {
                // Load into the textboxes
                categoryTextBox.Text = currentEntry.Category;
                descriptionTextBox.Text = currentEntry.Description;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string word = wordComboBox.Text;
            string category = categoryTextBox.Text;
            string description = descriptionTextBox.Text;

            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Find and update
            DictionaryEntry entry = entries.FirstOrDefault(x => x.Word == currentEntry.Word);
            if (entry != null)
            {
                entry.Word = word;
                entry.Category = category;
                entry.Description = description;

                string newJson = JsonConvert.SerializeObject(entries, Formatting.Indented);
                File.WriteAllText("dictionary.json", newJson);

                MessageBox.Show("Word edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
