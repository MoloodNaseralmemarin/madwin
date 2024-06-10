using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Calculations;
using Shop2City.DataLayer.Entities.Calculations;


namespace Shop2City.Web.Pages.Admin.Calculations
{
    public class EditModel : PageModel
    {
        private readonly ICalculationService _calculationsService;

        public EditModel(ICalculationService calculationsService)
        {
            _calculationsService = calculationsService;
        }

        [BindProperty]
        public CalculationModel editCalculation { get; set; }
        public void OnGet(int id)
        {
          editCalculation = _calculationsService.GetCalculationtById(id);
        }

        public async Task<IActionResult> OnPost()
        {
    
            await _calculationsService.UpdateCalculation(editCalculation);
            return RedirectToPage("Index");
        }
    }
}
