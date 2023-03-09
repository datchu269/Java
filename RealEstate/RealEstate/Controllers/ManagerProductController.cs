using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.RealEstateViewModel;
using RealEstate.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System;

namespace RealEstate.Controllers
{
	public class ManagerProductController : Controller
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly RealEstateContext _context;

		public ManagerProductController(IWebHostEnvironment webHostEnvironment, RealEstateContext context)
		{
			this.webHostEnvironment = webHostEnvironment;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var accId = User.FindFirstValue("AccountId");
			//var user = _context.User.Where(a => a.Email == email).AsNoTracking().FirstOrDefault();


			var viewModel = new ProductAllView();
			viewModel.User = await _context.User
				  .Include(m => m.TransactionExcepts)
					.ThenInclude(m => m.Product)
						.ThenInclude(m => m.ProductImages)
				  .Where(m => m.AccountId == int.Parse(accId))
				  .AsNoTracking()
				  .FirstOrDefaultAsync();

			User user = viewModel.User;
			viewModel.Products = user.TransactionExcepts.Select(a => a.Product).ToList();

			//viewModel.Products = await _context.Product
			//      .Include(m => m.ProductImages)
			//      .Include(m => m.TransactionExcepts)
			//        .ThenInclude(m => m.User)
			//      .AsNoTracking()
			//      .OrderBy(m => m.NameProduct)
			//      .ToListAsync();

			//Product product = viewModel.Products.Single();
			//viewModel.ProductImages = product.ProductImages;


			//viewModel.Categories = await _context.Category.ToListAsync();

			return View(viewModel);
		}

		//public async Task<IActionResult> Create([Bind("GuardId,FirstName,LastName,ServiceId,Phone,Height,Weight,Status,Avatar, ImageFile, CardId")] Guard guard)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", guard.ServiceId);
		//        //Save image to wwwroot/image
		//        string wwwRootPath = _hostEnvironment.WebRootPath;
		//        string fileName = Path.GetFileNameWithoutExtension(guard.ImageFile.FileName);
		//        string extension = Path.GetExtension(guard.ImageFile.FileName);
		//        guard.Avatar = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
		//        string path = Path.Combine(wwwRootPath + "/adminassests/Image/", fileName);
		//        using (var fileStream = new FileStream(path, FileMode.Create))
		//        {
		//            await guard.ImageFile.CopyToAsync(fileStream);
		//        }
		//        _context.Add(guard);
		//        await _context.SaveChangesAsync();
		//    }
		//    /*ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", guard.ServiceId);*/
		//    return View(guard);
		//}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var accId = User.FindFirstValue("AccountId");
			ViewBag.user = _context.User.Where(a => a.AccountId == int.Parse(accId)).AsNoTracking().FirstOrDefault();

			var viewModel = new ProductDetailView();

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductDetailView productDetailView)
		{
			var accId = User.FindFirstValue("AccountId");
			ViewBag.user = _context.User.Where(a => a.AccountId == int.Parse(accId)).AsNoTracking().FirstOrDefault();
			var userId = ViewBag.user.Id;
			var user = ViewBag.user;

			if (user.Balance <= user)
			{

			}

			var productReq = productDetailView.Product;
			var transactionExceptReq = productDetailView.TransactionExcept;

			var product = new Product
			{
				NameProduct = productReq.NameProduct,
				Price = productReq.Price,
				AddressProduct = productReq.AddressProduct,
				Directory = productReq.Directory,
				Area = productReq.Area,
				Bedroom = productReq.Bedroom,
				Restrooms = productReq.Restrooms,
				HomeOrientation = productReq.HomeOrientation,
				Description = productReq.Description,
				CategoryId = 1,
			};
			//_context.Add(product);
			//await _context.SaveChangesAsync();

			//Save image to wwwroot/image
			foreach (IFormFile img in productDetailView.files)
			{

				//Save image to wwwroot/image
				string fileName = Path.GetFileNameWithoutExtension(img.FileName);
				string extension = Path.GetExtension(img.FileName);
				string imgURL = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				string path = Path.Combine("wwwroot/img", fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await img.CopyToAsync(fileStream);
				}

				var i = 0;

				var productImage = new ProductImage
				{
					NameImage = imgURL,
					ProductId = product.Id,
				};
				_context.Add(productImage);
				await _context.SaveChangesAsync();

			}


			var transaction = new Transaction
			{
				Status = '1',
				Amount = 70,
				UserId = userId,
			};
			_context.Add(transaction);
			await _context.SaveChangesAsync();

			var transactionExcept = new TransactionExcept
			{
				Time = DateTime.Now,
				Day = transactionExceptReq.Day,
				ServicePackage = transactionExceptReq.ServicePackage,
				TransactionId = transaction.Id,
				UserId = userId,
				ProductId = product.Id,
			};
			_context.Add(transactionExcept);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}


