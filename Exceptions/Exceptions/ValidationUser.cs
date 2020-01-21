using System.IO;
using ShopBLL.Models;

namespace Exceptions
{
    public class ValidationUser : IValidationUser
    {
        private readonly string password;
        private readonly string confirmPassword;
        private readonly string login;
        private readonly UserSignInDTO user;

        public ValidationUser( string password, string confirmPassword)
        {
            this.password = password;
            this.confirmPassword = confirmPassword;
        }

        public ValidationUser(string login, string password, UserSignInDTO user)
        {
            this.user = user;
            this.login = login;
            this.password = password;                
        }

        public ValidationUser(string login, string password, string confirmPassword)
        {
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.login = login;
        }

        public void CheckUserPassword()
        {
            if(password.Length < 6)
            {
                throw new ValidationException("Слишком простой пароль", "");
            }
            else if(password != confirmPassword)
            {
                throw new ValidationException("Пароль повторён неверно", "");
            }       
        }

        public void CheckUserLogin()
        {
            if(login.Length < 2)
            {
                throw new ValidationException("Слишком короткое имя", "");
            }
            else if(Directory.Exists(login) || (login == "admin"))
            {
                throw new ValidationException("Пользователь с таким именем уже существует.", "");
            }
        }

        public void CheckExistUser()
        {
            if(user.Login == login)
            {
                if (user.Password == password)
                {
                    return;
                }
            }

            throw new ValidationException("Проверьте логин и/или пароль.", "");          
        }

    }
}
