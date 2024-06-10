using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.Core.Services.DisCounts
{
    public interface IDisCountService
    {
        List<DisCount> GetAllDisCounts();

        void AddDisCount(DisCount discount);
    }
}
