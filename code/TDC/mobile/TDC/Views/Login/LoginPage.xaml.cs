using TDC.Services;
using TDC.Models;
using TDC.IService;

namespace TDC.Views.Login;

public partial class LoginPage : ContentPage
{
    private readonly UserService _userService;
    private readonly IAccountService _accountService;

    public LoginPage(UserService userService, IAccountService accountService)
    {
        InitializeComponent();
        _userService = userService;
        _accountService = accountService;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text ?? string.Empty;
        string password = PasswordEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessageLabel.Text = "Bitte Benutzername und Passwort eingeben.";
            ErrorMessageLabel.IsVisible = true;
            return;
        }

        var loginSuccessful = await _accountService.AuthenticateUserLogin(username, password);

        if (loginSuccessful)
        {
            var account = await _accountService.GetAccountByUsername(username);
            _userService.Login(account);

            ErrorMessageLabel.IsVisible = false;
                        
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            ErrorMessageLabel.Text = "Falscher Benutzername oder Passwort.";
            ErrorMessageLabel.IsVisible = true;
        }
    }

    private void PasswordEntry_Completed(object sender, EventArgs e)
    {
        LoginButton_Clicked(sender, e);
    }
}