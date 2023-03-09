﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Extension;
using RealEstate.Helpper;
using RealEstate.Models;
using RealEstate.Models.RealEstateViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstate.Controllers
{
    public class AccountsController : Controller
    {
        private readonly RealEstateContext _context;
        public INotyfService _notyfService { get; }

        public AccountsController(RealEstateContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return _context.Account != null ?
                        View(await _context.Account.ToListAsync()) :
                        Problem("Entity set 'RealEstateContext.accRegister'  is null.");
        }

        // GET: Accounts/Login
        public IActionResult Login()
        {
            //var accRegisterID = HttpContext.Session.GetString("AccountId");
            //if (accRegisterID != null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var user = _context.User.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (user != null)
                    return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var user = _context.User.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (user != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterView accRegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Account> lsAccount = await _context.Account.ToListAsync();
                    foreach (var item in lsAccount)
                    {
                        if (item.Email.Trim() == accRegister.Email.Trim())
                        {
                            _notyfService.Error("Email đã tồn tại");
                            return View(accRegister);
                        }
                    }


                    string salt = Utilities.GetRandomKey();
                    Account account = new Account
                    {
                        FullName = accRegister.FullName,
                        Email = accRegister.Email.Trim().ToLower(),
                        Password = BCrypt.Net.BCrypt.HashPassword(accRegister.Password),
                        //Password = (accRegister.Password + salt.Trim()).ToMD5(),
                        Status = '1',
                    };


                    try
                    {
                        _context.Add(account);
                        await _context.SaveChangesAsync();

                        User user = new User
                        {
                            FullName = accRegister.FullName,
                            Phone = accRegister.Phone.Trim().ToLower(),
                            Email = accRegister.Email.Trim().ToLower(),
                            AccountId = account.Id,
                        };
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        //Lưu Session MaKh
                        HttpContext.Session.SetString("AccountId", account.Id.ToString());
                        var accID = HttpContext.Session.GetString("AccountId");

                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.FullName),
                            new Claim("Email", account.Email),
                            new Claim("Image", user.Image + ""),
                            new Claim("AccountId", account.Id.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đăng ký thành công");
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        return RedirectToAction("Register", "Accounts");
                    }
                }

                else
                {
                    return View(accRegister);
                }
            }
            catch
            {
                return View(accRegister);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView accLogin, string? returnUrl)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(accLogin.UserName);
                    if (!isEmail) return View(accLogin);

                    var account = _context.Account.AsNoTracking().FirstOrDefault(x => x.Email.Trim() == accLogin.UserName.Trim());
                    var user = _context.User.AsNoTracking().FirstOrDefault(x => x.Email.Trim() == accLogin.UserName.Trim());

                    if (account == null)
                    {
                        _notyfService.Warning("tkThông tin đăng nhập chưa chính xác");
                        return View(accLogin);
                    }

                    bool checkPass = BCrypt.Net.BCrypt.Verify(accLogin.Password, account.Password);
                    if (!checkPass)
                    {
                        _notyfService.Warning("Thông tin đăng nhập chưa chính xác");
                        return View(accLogin);
                    }
                    //kiem tra xem account co bi disable hay khong

                    //if (account.Active == false)
                    //{
                    //    return RedirectToAction("ThongBao", "Accounts");
                    //}

                    //Luu Session MaKh
                    HttpContext.Session.SetString("AccountId", account.Id.ToString());
                    var accID = HttpContext.Session.GetString("AccountId");
                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.FullName),
                        new Claim("Email", account.Email),
                        new Claim("Image", user.Image + ""),
                        new Claim("AccountId", account.Id.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");

                    if (account.Status == '0')
                    {
                        return RedirectToAction("Index", "Admin", "AdminProducts");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }

                }
            }
            catch
            {
                return RedirectToAction("Register", "Accounts");
            }
            return View(accLogin);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("AccountId");
            _notyfService.Success("Đăng xuất thành công");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //public IActionResult ChangePassword(ChangePasswordViewModel model)
        //{
        //    try
        //    {
        //        var accID = HttpContext.Session.GetString("AccountId");
        //        if (accID == null)
        //        {
        //            return RedirectToAction("Login", "Accounts");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            var taikhoan = _context.Customers.Find(Convert.ToInt32(accID));
        //            if (taikhoan == null) return RedirectToAction("Login", "Accounts");
        //            var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
        //            {
        //                string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
        //                taikhoan.Password = passnew;
        //                _context.Update(taikhoan);
        //                _context.SaveChanges();
        //                _notyfService.Success("Đổi mật khẩu thành công");
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        _notyfService.Success("Thay đổi mật khẩu không thành công");
        //        return RedirectToAction("Index", "Home");
        //    }
        //    _notyfService.Success("Thay đổi mật khẩu không thành công");
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Profile()
        {
            var accId = User.FindFirstValue("AccountId");
            var acc = _context.Account.Where(a => a.Id == int.Parse(accId)).AsNoTracking().Include(a => a.Users).FirstOrDefault();
            return View(acc);
        }

        public IActionResult EditProfile()
        {
            var accId = User.FindFirstValue("AccountId");
            var acc = _context.Account.Where(a => a.Id == int.Parse(accId)).AsNoTracking().Include(a => a.Users).FirstOrDefault();
            return View(acc);
        }

        private bool accRegisterExists(int id)
        {
            return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
