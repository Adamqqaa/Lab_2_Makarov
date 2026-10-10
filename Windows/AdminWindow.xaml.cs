using System.Windows;
using Lab_2.Models;

namespace Lab_2.Windows;

/// <summary>Форма, открываемая для пользователя с ролью «Администратор».</summary>
public partial class AdminWindow : Window
{
    public AdminWindow(User user)
    {
        InitializeComponent();
        TxtFullName.Text = user.FullName;
        TxtLogin.Text = "Логин: " + user.Login;
        TxtRole.Text = "Текущая роль: " + user.Role.Name;
    }

    private void BtnLogout_Click(object sender, RoutedEventArgs e)
    {
        var login = new MainWindow();
        login.Show();
        Application.Current.MainWindow = login;
        Close();
    }
}