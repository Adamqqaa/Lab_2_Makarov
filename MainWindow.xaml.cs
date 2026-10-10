using System.Windows;
using System.Windows.Input;
using Lab_2.Data;
using Lab_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        TxtLogin.Focus();
    }

    private void PwdPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            BtnLogin_Click(sender, e);
    }

    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        TxtError.Visibility = Visibility.Collapsed;

        string login = TxtLogin.Text.Trim();
        string password = PwdPassword.Password;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            ShowError("Введите логин и пароль.");
            return;
        }

        try
        {
            using var db = TradeContextFactory.Create();

            User? user = db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user is null)
            {
                ShowError("Неверный логин или пароль.");
                return;
            }

            // В зависимости от роли открываем разные формы
            Window roleWindow = user.Role.Name.Trim() switch
            {
                "Администратор" => new Windows.AdminWindow(user),
                "Менеджер" => new Windows.ManagerWindow(user),
                _ => new Windows.ClientWindow(user)
            };

            roleWindow.Show();
            Application.Current.MainWindow = roleWindow;
            Close();
        }
        catch (Exception ex)
        {
            ShowError("Ошибка подключения к базе данных: " + ex.Message);
        }
    }


    private void ShowError(string message)
    {
        TxtError.Text = message;
        TxtError.Visibility = Visibility.Visible;
    }
}