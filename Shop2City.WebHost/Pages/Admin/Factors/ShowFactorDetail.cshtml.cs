using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Factors;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Web.Pages.Admin.Factors
{
    public class ShowFactorDetailModel : PageModel
    {
        private readonly IFactorService _orderService;
        public ShowFactorDetailModel(IFactorService orderService)
        {
            _orderService = orderService;
        }
        public List<FactorDetail> GetAllOrderDetails { get; set; }

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            GetAllOrderDetails = _orderService.GetAllFactorDetailByFactorId(id);
        }
    }
}
