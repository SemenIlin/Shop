using RecordInJsonFile;
using Shop.PL.Models;
using ShopBLL.Models;

namespace Shop.PL
{
    public class Account
    {
        private string login;

        public UserSignInDTO UserSignInDTO { get; private set; }

        public bool IsLogin { get; private set; }

        public void RegistrationUser(RegistrationModel registration)
        {
            UserSignInDTO = new UserSignInDTO
            {
                Login = registration.Login,
                Password = registration.Password
            };

            login = registration.Login;                 

            IsLogin = true;
        }

        public void SignInUser(SignInModel signIn)
        {
            UserSignInDTO = new UserSignInDTO
            {
                Login = signIn.Login,
                Password = signIn.Password
            };

            login = signIn.Login;

            IsLogin = true;
        }

        public void CreateRecord()
        {
            var directory = new DirectoryForJson<UserSignInDTO>(UserSignInDTO.Login, UserSignInDTO, false);
            directory.WriteInDerictory();
        }

        public UserSignInDTO GetRecords()
        {
            return DirectoryForJson<UserSignInDTO>.ReadJson(login + "\\" + login + ".json");
        }

        public void ExitFromAccount()
        {
            CreateRecord();
            IsLogin = false;
        }
    }
}
