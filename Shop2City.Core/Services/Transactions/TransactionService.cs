using Shop2City.DataLayer.Context;
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
    public class TransactionService : ITransactionService
    {
        private readonly Shop2CityContext _context;
        public TransactionService(Shop2CityContext context)
        {
            _context = context;   
        }

        public async Task AddAdvice(AdviceModel advice)
        {
            _context.Advices.Add(advice);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransaction(TransactionModel transaction)
        {
            _context.Transactions.Add(transaction);
            await  _context.SaveChangesAsync();
        }
    }
}
