using Shop2City.DataLayer.Entities.Advices;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Shop2City.Core.Services.Transactions
{
    public interface ITransactionService
    {
        Task AddTransaction(TransactionModel transaction);

        Task AddAdvice(AdviceModel transaction);
    }
}
