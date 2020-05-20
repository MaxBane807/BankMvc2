namespace Bank.Web.Services.Interfaces
{
    public interface ITransferService
    {
        bool CreateTransfer(int accountid,bool credit,string operation,decimal amount,string symbol,string otheraccount);
    }
}
