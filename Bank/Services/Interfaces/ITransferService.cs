namespace Bank.Web.Services.Interfaces
{
    public interface ITransferService
    {
        void CreateTransfer(int accountid,bool credit,string operation,decimal amount,string symbol,string otheraccount);
    }
}
