using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MVPTema1Dictionar
{
    public class LoginManager
    {
        public void VerifyLogin(string username, string password)
        {
            List<User> users = ReadLoginInfo();

            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    MainWindowAfterLogin mainWindow = new MainWindowAfterLogin();
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AdminPanel))
                        {
                            window.Close();
                            break;
                        }
                    }

                    return;
                }
            }

            MessageBox.Show("Invalid username or password.");
        }

        private List<User> ReadLoginInfo()
        {
            try
            {
                string json = File.ReadAllText("..\\..\\adminlogin.json");
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading login information: " + ex.Message);
                return new List<User>();
            }
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
