﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Products;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Web
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }
        public Product detailProduct { get; set; }
        public void OnGet(int id)
        {
            detailProduct = _productService.GetDetailsProductByProductId(id);
        }
    }
}