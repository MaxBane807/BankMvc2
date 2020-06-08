namespace Bank.Web.Services.Interfaces
{
    public interface IWithdrawService
    {
        void CreateWithdraw(int AccountID, string operation, decimal amount, string symbol, string bank, string otherAccount);
    }
}
