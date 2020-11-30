using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Interfaces
{
    public interface IInsertTransaction
    {
        void SaveTransaction(string PaymentID, int EmployeeID, string CustomerEmail, List<int> ItemIDs, double AmtDiscounted, DateTime TransactionDate);
    }
}
