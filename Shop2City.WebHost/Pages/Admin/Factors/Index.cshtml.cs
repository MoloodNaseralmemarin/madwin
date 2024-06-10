using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Web.Pages.Admin.Factors
{
    public class IndexModel : PageModel
    {
        private readonly IFactorService _factorService;
        public IndexModel(IFactorService factorService)
        {
            _factorService = factorService;
        }
        public List<Factor> GetAllFactor { get; set; }
        public void OnGet()
        {
            GetAllFactor = _factorService.GetAllOrder();

        }
    }
}
