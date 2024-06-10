using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.SlideShows;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Slideshows;

namespace Shop2City.Web.Pages.Admin.SlideShows
{
    public class EditModel : PageModel
    {
        private ISlideShowService _slideShowService;
        public EditModel(ISlideShowService slideShowService)
        {
            _slideShowService = slideShowService;
        }

        [BindProperty]
        public SlideShow editSlideShow { get; set; }
        public void OnGet(int id)
        {
            editSlideShow = _slideShowService.GetSlideShowBySlideShowId(id);
        }
    }
}