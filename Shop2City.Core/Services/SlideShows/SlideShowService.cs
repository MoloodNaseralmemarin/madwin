﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.Convertors;
using Shop2City.Core.DTOs;
using Shop2City.Core.Generator;
using Shop2City.Core.Security;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Slideshows;

namespace Shop2City.Core.Services.SlideShows
{
    public class SlideShowService : ISlideShowService
    {
        private readonly Shop2CityContext _context;
        public SlideShowService(Shop2CityContext context)
        {
            _context = context;
        }

        public void AddSlideShow(SlideShow slideShow, IFormFile imgProduct)
        {
            slideShow.IsDelete = false;
            if (imgProduct != null)
            {
                slideShow.FileName = NameGenerator.GenerateUniqCode() +
                                           Path.GetExtension(Path.GetExtension(imgProduct.FileName));
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshows/images",
                    slideShow.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProduct.CopyTo(stream);
                }

                //TODO : Check Image

                #region Image Resize

                //ImageConvertor imgResizer = new ImageConvertor();
                //string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshows/thumbs",
                //           slideShow.slideShowImageName);
                //imgResizer.Image_resize(imagePath, thumbPath, 200);

                #endregion
            }
            _context.SlideShows.Add(slideShow);
            _context.SaveChanges();
        }

        public SlideShow GetSlideShowBySlideShowId(int slideShowId)
        {
            
           // return qImg ?? null; //میدونم مشکلم کجاس
            return _context.SlideShows.Find(slideShowId);
        }

        public List<SlideShowViewModel> GetSlideShows()
        {
            var getSlideShows = _context.SlideShows
               .Where(s=>s.IsActive)
                .OrderBy(s => s.Sort)
                .Select(s => new SlideShowViewModel
                {
                    Alt = s.Alt,
                    Id = s.Id,
                    FileName = s.FileName,
                    Link =s.Link+s.Path+s.FileName,
                    Sort = s.Sort,
                    Title = s.Title,
                    endShowDateTime=s.EndShowDateTime,
                    startShowDateTime=s.StartShowDateTime
                })
                .ToList();
            return getSlideShows;
        }


        public void UpdateSlideShow(SlideShow slideShow, IFormFile imgProduct)
        {
            slideShow.IsDelete = false;
            if (imgProduct != null)
            {
                slideShow.FileName = NameGenerator.GenerateUniqCode() +
                                           Path.GetExtension(Path.GetExtension(imgProduct.FileName));
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshows/images",
                    slideShow.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProduct.CopyTo(stream);
                }

                //TODO : Check Image

                #region Image Resize

                //ImageConvertor imgResizer = new ImageConvertor();
                //string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshows/thumbs",
                //           slideShow.slideShowImageName);
                //imgResizer.Image_resize(imagePath, thumbPath, 200);

                #endregion
            }
            _context.SlideShows.Update(slideShow);
            _context.SaveChanges();
        }
    }
}
