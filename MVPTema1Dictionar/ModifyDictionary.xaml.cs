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

public partial class ModifyDictionary : Window
{
    public ModifyDictionary()
    {
        InitializeComponent();
    }

    private void AddWord_Click(object sender, RoutedEventArgs e)
    {
        AddWord addWordWindow = new AddWord();
        addWordWindow.Show();
        this.Close();
    }

    private void EditWord_Click(object sender, RoutedEventArgs e)
    {
        EditWord editWordWindow = new EditWord();
        editWordWindow.Show();
        this.Close();
    }

    private void DeleteWord_Click(object sender, RoutedEventArgs e)
    {
        DeleteWord deleteWordWindow = new DeleteWord();
        deleteWordWindow.Show();
        this.Close();
    }
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindowAfterLogin prevWindow = new MainWindowAfterLogin();
        prevWindow.Show();
        this.Close();
    }
}
}
