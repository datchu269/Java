using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.RealEstateViewModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;



namespace RealEstate.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RealEstateContext _context;

        public ProductController(IWebHostEnvironment webHostEnvironment, RealEstateContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        private string UploadedFile(ProductImage productImage)
        {
            string uniqueFileName = null;

            if (productImage.FrontImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + productImage.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productImage.FrontImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
