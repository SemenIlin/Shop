namespace Shop.BLL.Interfaces
{
    public interface ISignIn<T> where T : class
    {
        void SignInUser(T signIn);
    }
}
