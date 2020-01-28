
namespace Shop.BLL.Interfaces
{
    public interface IAccount<TAccount, TRegistrationModel, TSignInModel>: IRegistration<TRegistrationModel>, ISignIn<TSignInModel>
        where TAccount : class
        where TRegistrationModel : class
        where TSignInModel : class
    {
        bool IsLogin { get;  }
        TAccount User { get; }
        void CreateRecord();
        TAccount GetRecords();
        void ExitFromAccount();
    }
}
