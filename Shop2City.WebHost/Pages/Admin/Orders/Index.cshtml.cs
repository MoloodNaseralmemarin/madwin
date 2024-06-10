using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Web.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public List<OrderModel> GetAllOrder { get; set; }
        public void OnGet()
        {
            GetAllOrder = _orderService.GetAllOrder();

        }
    }
}
