using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Shop2City.Core.DTOs;
using Shop2City.DataLayer.Entities.Slideshows;

namespace Shop2City.Core.Services.SlideShows
{
   public  interface ISlideShowService
    {
        List<SlideShowViewModel> GetSlideShows();

        void AddSlideShow(SlideShow slideShow, IFormFile imgProduct);

        void UpdateSlideShow(SlideShow slideShow, IFormFile imgProduct);

        SlideShow GetSlideShowBySlideShowId(int slideShowId);
    }
}
