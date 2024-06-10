using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.SlideShows;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Slideshows;

namespace Shop2City.Web.Pages.Admin.SlideShows
{
    public class CreateModel : PageModel
    {
        private ISlideShowService _slideShowService;
        public CreateModel(ISlideShowService slideShowService)
        {
            _slideShowService = slideShowService;
        }
        [BindProperty]
        public SlideShow createSlideShow { get; set; }
        public IActionResult OnPost(IFormFile imgProduct)
        {
            if (!ModelState.IsValid)
                return Page();


            _slideShowService.AddSlideShow(createSlideShow, imgProduct);
            return RedirectToPage("Index");
        }
    }
}