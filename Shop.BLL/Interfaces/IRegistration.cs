namespace Shop.BLL.Interfaces
{
    public interface IRegistration<T> where T : class
    {
        void RegistrationUser(T registration);
    }
}
