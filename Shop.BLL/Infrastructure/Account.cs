using RecordInJsonFile;
using Shop.BLL.Interfaces;
using Shop.BLL.Models;

namespace Shop.BLL.Infrastructure
{
    public class Account : IAccount<UserDTO,RegistrationDTO, SignInDTO>
    {

        private string login;

        public UserDTO User { get; private set; }

        public bool IsLogin { get; private set; }

        public void RegistrationUser(RegistrationDTO registration)
        {
            User = new UserDTO
            {
                Login = registration.Login,
                Password = registration.Password
            };

            login = registration.Login;                 

            IsLogin = true;
        }

        public void SignInUser(SignInDTO signIn)
        {
            User = new UserDTO
            {
                Login = signIn.Login,
                Password = signIn.Password
            };

            login = signIn.Login;

            IsLogin = true;
        }

        public void CreateRecord()
        {
            var directory = new DirectoryForJson<UserDTO>(User.Login, User, false);
            directory.WriteInDerictory();
        }

        public UserDTO GetRecords()
        {
            return DirectoryForJson<UserDTO>.ReadJson(login + "\\" + login + ".json");
        }

        public void ExitFromAccount()
        {
            CreateRecord();
            IsLogin = false;
        }
    }
}
