using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace MVPTema1Dictionar
{

    public partial class AddWord : Window
    {
        private string selectedImagePath;
        private List<string> categories;

        public AddWord()
        {
            InitializeComponent();

            List<DictionaryEntry> entries = new List<DictionaryEntry>();
            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }

            categories = new List<string>();
            foreach (var entry in entries)
            {
                if (!categories.Contains(entry.Category))
                {
                    categories.Add(entry.Category);
                }
            }

            foreach (var category in categories)
            {
                existingCategoryComboBox.Items.Add(category);
            }
        }

        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            string word = wordTextBox.Text.ToLower();
            string category;
            if (newCategoryTextBox.Visibility == Visibility.Visible)
            {
                category = newCategoryTextBox.Text.ToLower();
                if (categories.Contains(category))
                {
                    MessageBox.Show("This category already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                category = existingCategoryComboBox.Text.ToLower();
            }
            string description = descriptionTextBox.Text;

            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Read from file
            List<DictionaryEntry> entries = new List<DictionaryEntry>();
            if (File.Exists("dictionary.json"))
            {
                string json = File.ReadAllText("dictionary.json");
                entries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(json);
            }

            // Check if the word already exists
            foreach (var entry in entries)
            {
                if (entry.Word == word)
                {
                    MessageBox.Show("This word already exists in the dictionary.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            DictionaryEntry newEntry = new DictionaryEntry
            {
                Word = word,
                Category = category,
                Description = description,
                ImagePath = selectedImagePath
            };

            entries.Add(newEntry);

            // Serialize
            string newJson = JsonConvert.SerializeObject(entries, Formatting.Indented);

            // Save
            File.WriteAllText("dictionary.json", newJson);

            MessageBox.Show("Word added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


       
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg,*.jpg,*.webp)|*.png;*.jpeg;*.jpg;*.webp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string originalPath = openFileDialog.FileName;
                string targetPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Images", Path.GetFileName(originalPath));

                // Create the Images directory
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

                // Read the file into a MemoryStream
                using (Stream input = File.OpenRead(originalPath))
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    input.CopyTo(memoryStream);

                    // Write the MemoryStream to the target location
                    using (Stream output = File.Create(targetPath))
                    {
                        memoryStream.WriteTo(output);
                    }
                }

                // Store the relative path of the copied image
                selectedImagePath = Path.Combine("..", "..", "Images", Path.GetFileName(originalPath));
                selectedImage.Source = new BitmapImage(new Uri(targetPath));
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyDictionary prevWindow = new ModifyDictionary();
            prevWindow.Show();
            this.Close();
        }
        private void CategoryOptionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedOption = (categoryOptionComboBox.SelectedItem as ComboBoxItem).Content as string;
            if (selectedOption == "New Category")
            {
                newCategoryTextBox.Visibility = Visibility.Visible;
                existingCategoryComboBox.Visibility = Visibility.Collapsed;
            }
            else if (selectedOption == "Select Category")
            {
                newCategoryTextBox.Visibility = Visibility.Collapsed;
                existingCategoryComboBox.Visibility = Visibility.Visible;
            }
        }

    }

    public class DictionaryEntry
    {
        public string Word { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}