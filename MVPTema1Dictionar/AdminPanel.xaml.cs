using System.Windows;

namespace MVPTema1Dictionar
{
    public partial class AdminPanel : Window
    {
        private LoginManager loginManager; // Create an instance of LoginManager

        public AdminPanel()
        {
            InitializeComponent();
            loginManager = new LoginManager(); // Initialize the LoginManager instance
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Call the VerifyLogin method of the LoginManager instance
            loginManager.VerifyLogin(username, password);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow prevWindow = new MainWindow();
            prevWindow.Show();
            this.Close();
        }
    }
}
