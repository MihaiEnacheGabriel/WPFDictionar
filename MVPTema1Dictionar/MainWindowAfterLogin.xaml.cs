using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVPTema1Dictionar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowAfterLogin : Window
    {
        public MainWindowAfterLogin()
        {
            InitializeComponent();
        }

        private void ModifyDictionary_Click(object sender, RoutedEventArgs e)
        {
            ModifyDictionary modifyDictionary= new ModifyDictionary();
            modifyDictionary.Show();
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWord searchWord = new SearchWord();
            searchWord.Show();
            this.Close();
        }

        private void FunButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }
    }
}