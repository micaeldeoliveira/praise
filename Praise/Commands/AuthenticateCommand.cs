using Flunt.Notifications;
using Flunt.Validations;

namespace Praise.Commands
{
    public class AuthenticateCommand : Notifiable
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            Username = Username.Trim().ToLower();
            Password = Password.Trim().ToLower();

            AddNotifications(new Contract()
                    .HasMinLen(Username, 5, "Username", "Usuario deve ter no mínimo 5 caracteres.")
                    .HasMaxLen(Username, 50, "Username", "Usuario deve ter no máximo 50 caracteres.")
                    .HasMinLen(Password, 5, "Password", "Senha deve ter no mínimo 5 caracteres."));
            return Valid;
        }
    }
}
