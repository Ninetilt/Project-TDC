namespace TDC.Services
{
    using TDC.Models;
    public class UserService
    {
        public Account? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(Account user)
        {
            // Setzt den aktuellen Benutzer
            this.CurrentUser = user;

            // Hier könntest du später weitere Logik hinzufügen,
            // z.B. Laden von Benutzereinstellungen, Auslösen eines Events, etc.
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}