using Exceptions;

namespace Shop.PL.Models
{
    public class RegistrationModel
    {
        public RegistrationModel(string login, string password, string confirmPassword)
        {
            ValidationUser validation = new ValidationUser(login, password, confirmPassword);
            validation.CheckUserLogin();
            validation.CheckUserPassword();

            Login = login;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }       
    }
}
