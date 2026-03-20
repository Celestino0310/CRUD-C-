using CrudMvc.Models;
using CrudMvc.Models.ViewModels;
using CrudMvc.Services;
using CrudMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics;
using ZstdSharp.Unsafe;

namespace CrudMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;

        public SellersController(SellerService sellerService,DepartamentService departamentService)
        {

            _sellerService = sellerService;
            _departamentService = departamentService;
        }
        

        public IActionResult Index()
        {
            var list = _sellerService.findAll();
            return View(list);
        }
        public IActionResult Create()
        {   
            var departaments= _departamentService.findAll();
            var viewModel = new SellerFormViewModel { Departaments=departaments};
            return View(viewModel);
        }
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });
            }

            var seller =  _sellerService.findByID(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" }); ;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.Update(seller);
                    
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                catch (NotFoundException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }
        //============================DETAILS =======================================
        public IActionResult Details (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message="ID NOT EXIST"});

            }
            var obj = _sellerService.findByID(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            }

            return View(obj);
        }


        public IActionResult Delete(int id) {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            }
            var obj = _sellerService.findByID(id);
            if (obj==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller NOT FOUND" });

            }
            
            return View(obj); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//protege essa requisição de alguns ataques
        public IActionResult Delete(Seller seller) {
            _sellerService.Remove(seller.Id);
            return RedirectToAction(nameof(Index)); 
        }

        //POST CREATE 

        [HttpPost]//serve para informar que essa função vai coloca algo no db
        [ValidateAntiForgeryToken]//protege essa requisição de alguns ataques
        public IActionResult Create(Seller seller) {

            if (!ModelState.IsValid)
            { return View(); }

              _sellerService.Insert(seller);
              return RedirectToAction(nameof(Index)); 
        }
        public IActionResult Error(string message)
        {
            var erro = new ErrorViewModel
            {
                Mensagem = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

         
            };return View(erro);
        }
    }
}
