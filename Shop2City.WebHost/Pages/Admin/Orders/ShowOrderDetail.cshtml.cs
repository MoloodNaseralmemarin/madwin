using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Web.Pages.Admin.Orders
{
    public class ShowOrderDetailModel : PageModel
    {
        private readonly IOrderService _orderService;
        public ShowOrderDetailModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public List<CalculationDetailModel> GetAllOrderDetails { get; set; }

        public void OnGet(int id)
        {
            GetAllOrderDetails = _orderService.GetAllCalculationDetailModelByOrderId(id);
        }
    }
}
