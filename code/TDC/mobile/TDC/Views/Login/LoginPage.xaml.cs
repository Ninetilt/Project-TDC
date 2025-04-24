using TDC.Services;
using TDC.Models;
using TDC.IRepository; // Wichtig für IAccountRepository
using TDC.Views; // für die MainPage Navigation (oder deine Zielseite)

namespace TDC.Views.Login;

public partial class LoginPage : ContentPage
{
    private readonly UserService _userService;
    private readonly IAccountRepository _accountRepository;

    public LoginPage(UserService userService, IAccountRepository accountRepository)
    {
        InitializeComponent();
        _userService = userService;
        _accountRepository = accountRepository;
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

        Account? account = _accountRepository.AuthenticateUser(username, password);

        if (account != null)
        {
            _userService.Login(account);

            ErrorMessageLabel.IsVisible = false;

            // Erfolgreich eingeloggt -> zur Zielseite navigieren
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            ErrorMessageLabel.Text = "Falscher Benutzername oder Passwort.";
            ErrorMessageLabel.IsVisible = true;
        }
    }
}