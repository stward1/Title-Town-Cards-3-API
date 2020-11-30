using System.Collections.Generic;

namespace API.Models
{
    public interface IGetAllTransactions
    {
        List<Transaction> GetAllTransactions();
    }
}