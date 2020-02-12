using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using WebCoreProject.Models;
using WebCoreProject.Security;
using WebCoreProject.Utilities;
using X.PagedList;

namespace WebCoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDataProtector protector;

        public HomeController(IEmployeeRepository employeeRepository, 
                                IDataProtectionProvider dataProtectionProvider,
                                DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _employeeRepository = employeeRepository;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }
        [AllowAnonymous]
        public IActionResult Index(int? page)
        {
            //var model = _employeeRepository.GetAllEmployees()
            //    .Select(e => {
            //        e.EncrypredId = protector.Protect(e.Id.ToString());
            //        return e;
            //});
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = _employeeRepository.GetAllPhones();//Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();
            IPagedList<Phone> listOfPhones = list.ToPagedList(pageindex, pagesize);

            ViewBag.MySession = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");
            return View(listOfPhones);
        }
        [AllowAnonymous]
        public IActionResult ViewPhone(int id)
        {
            var phone = _employeeRepository.GetPhone(id);
            return View(phone);
        }
        [AllowAnonymous]
        public IActionResult Checkout()
        {
            ViewBag.MySession = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");
            return View();
        }
        [AllowAnonymous]
        public IActionResult AddToCart(int id, int quantity = 1) {
            Phone phone = _employeeRepository.GetPhone(id);
            var cart = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");

            if (cart == null)
            {
                cart = new List<Phone>();

                cart.Add(new Phone()
                {
                    Id = phone.Id,
                    Model = phone.Model,
                    DisplayName = phone.DisplayName,
                    Price = phone.Price,
                    PhotoPath = phone.PhotoPath,
                    Quantity = quantity
                });
                HttpContext.Session.SetComplexData("ComplexObject", cart);
            }
            else if (IncreaseQty(id)) { }
            else
            {
                cart.Add(new Phone()
                {
                    Id = phone.Id,
                    Model = phone.Model,
                    DisplayName = phone.DisplayName,
                    Price = phone.Price,
                    PhotoPath = phone.PhotoPath,
                    Quantity = quantity
                });
                HttpContext.Session.SetComplexData("ComplexObject", cart);
            }
            ViewBag.MySession = cart;
            return RedirectToAction("Checkout");
        }
        private bool IncreaseQty(int id)
        {
            List<Phone> cart = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");
            foreach (var item in cart)
            {
                if (item.Id == id)
                {
                    item.Quantity++;
                    HttpContext.Session.SetComplexData("ComplexObject", cart);
                    return true;
                }
            }
            return false;
        }
        public ActionResult DecreaseQty(int id)
        {
            List<Phone> cart = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");
            foreach (var item in cart)
            {
                if (item.Id == id && !(item.Quantity <= 1))
                {
                    item.Quantity--;
                    HttpContext.Session.SetComplexData("ComplexObject", cart);
                }
                else if (item.Id == id && item.Quantity <= 1) {
                    RemoveFromCart(id);
                }
            }
            return RedirectToAction("Checkout");
        }
        public ActionResult RemoveFromCart(int id)
        {
            List<Phone> cart = HttpContext.Session.GetComplexData<List<Phone>>("ComplexObject");
            foreach (var item in cart)
            {
                if (item.Id == id)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetComplexData("ComplexObject", cart);
                    break;
                }
            }
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
                return RedirectToAction("Checkout");
        }
    }
}