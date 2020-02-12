using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebCoreProject.Models;
using WebCoreProject.ViewModels;
using X.PagedList;

namespace WebCoreProject.Controllers
{
    [AllowAnonymous]
    public class PhonesController : Controller
    {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IHostingEnvironment hostingEnvironment;
            //private object page;

        public PhonesController(IEmployeeRepository employeeRepository,
                                        IHostingEnvironment hostingEnvironment)
            {
                _employeeRepository = employeeRepository;
                this.hostingEnvironment = hostingEnvironment;
            }
            public IActionResult ListPhones(int? page)
            {
                int pagesize = 6, pageindex = 1;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
                var list = _employeeRepository.GetAllPhones();  //Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();
                IPagedList<Phone> listOfPhones = list.ToPagedList(pageindex, pagesize);

            return View(listOfPhones);
            }
            public IActionResult ViewPhone(int id)
            {
            var phone = _employeeRepository.GetPhone(id);
            return View(phone);
            }
            public IActionResult CreatePhone()
            {
                return View();
            }
            [HttpPost]
            public IActionResult CreatePhone(CreatePhoneViewModel phone)
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (phone.Photo != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + phone.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        phone.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    Phone newPhone = new Phone()
                    {
                        DisplayName = phone.DisplayName,
                        Model = phone.Model,
                        Price = (decimal)phone.Price,
                        OldPrice = (decimal)phone.OldPrice,
                        Color = phone.Color,
                        Storage = phone.Storage,
                        Quantity = phone.Quantity,
                        Description = phone.Description,
                        Manufacturer = phone.Manufacturer,
                        PhotoPath = uniqueFileName
                    };
                    _employeeRepository.AddPhone(newPhone);
                    RedirectToAction("ListPhones");
                }
                return View(phone);
            }
            [HttpGet]
            public IActionResult EditPhone(int id)
            {
                Phone phone = _employeeRepository.GetPhone(id);
                EditPhoneViewModel editPhoneViewModel = new EditPhoneViewModel
                {
                    Id = phone.Id,
                    DisplayName = phone.DisplayName,
                    Model = phone.Model,
                    Price = phone.Price,
                    OldPrice = phone.OldPrice,
                    Quantity = phone.Quantity,
                    Color = phone.Color,
                    Storage = phone.Storage,
                    Description = phone.Description,
                    Manufacturer = phone.Manufacturer,
                    ExistingPhotoPath = phone.PhotoPath
                };
                return View(editPhoneViewModel);
            }
            [HttpPost]
            public IActionResult EditPhone(EditPhoneViewModel editedPhone)
            {
                if (ModelState.IsValid)
                {
                    Phone phone = _employeeRepository.GetPhone(editedPhone.Id);
                    phone.DisplayName = editedPhone.DisplayName;
                    phone.Model = editedPhone.Model;
                    phone.Price = (decimal)editedPhone.Price;
                    phone.OldPrice = editedPhone.OldPrice;
                    phone.Quantity = editedPhone.Quantity;
                    phone.Color = editedPhone.Color;
                    phone.Storage = editedPhone.Storage;
                    phone.Description = editedPhone.Description;
                    phone.Manufacturer = editedPhone.Manufacturer;

                    if (editedPhone.Photo != null)
                    {
                        if (editedPhone.ExistingPhotoPath != null)
                        {
                            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", editedPhone.ExistingPhotoPath);
                            System.IO.File.Delete(filePath);
                        }
                        phone.PhotoPath = ProcessUploadedFile(editedPhone);
                    }
                    Phone updatedPhone = _employeeRepository.UpdatePhone(phone);
                    return RedirectToAction("index", "home");
                }
                return View(editedPhone);
            }
            [HttpGet]
            public IActionResult DeletePhone(int id)
            {
                _employeeRepository.DeletePhone(id);
                return RedirectToAction("ListPhones");
            }

        private string ProcessUploadedFile(CreatePhoneViewModel model)
            {
                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                }
                return uniqueFileName;
            }
    }
}