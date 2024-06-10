using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Calculations;
using Shop2City.DataLayer.Entities.Calculations;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.WebHost.Pages.Admin.Calculations
{
    // [PermissionChecker(10)]
    public class IndexModel : PageModel
    {
        private readonly ICalculationService _calculationRepository;
        public IndexModel(ICalculationService calculationRepository)
        {
            _calculationRepository = calculationRepository;

        }
        public List<CalculationModel> calculationModel { get; set; }

        public  void OnGet()
        {
            calculationModel =  _calculationRepository.GetCalculation();
        }
    }
}