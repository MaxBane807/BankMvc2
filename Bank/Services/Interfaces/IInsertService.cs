namespace Bank.Web.Services.Interfaces
{
    public interface IInsertService
    {
        void CreateAnInsert(int AccountID,string operation,decimal amount,string symbol,string bank,string otherAccount);
    }
}
