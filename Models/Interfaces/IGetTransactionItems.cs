using System.Collections.Generic;

namespace API.Models
{
    public interface IGetTransactionItems
    {
        List<Item> GetTransactionItems(int transactionID);
    }
}