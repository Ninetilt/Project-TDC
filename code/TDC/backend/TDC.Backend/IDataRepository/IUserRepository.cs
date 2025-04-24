namespace TDC.Backend.IDataRepository
{
    public interface IUserRepository
    {
        public void AddAccount();
        public void DeleteAccount();
        public void UpdateAccount();
        public void GetAccountByUsername();
        public void GetAccountByEmail();
    }
}
