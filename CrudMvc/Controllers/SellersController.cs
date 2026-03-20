using CrudMvc.Models;
using CrudMvc.Models.ViewModels;
using CrudMvc.Services;
using CrudMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
                return NotFound();
            }

            var seller =  _sellerService.findByID(id.Value);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.Update(seller);
                    
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new DbException("erro com o banco de dados");
                }
                catch (NotFoundException e)
                {
                    throw new NotFoundException("ID NÃO ENCONTRADO");
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //============================DETAILS =======================================
        public IActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var obj = _sellerService.findByID(id.Value);
            if (obj == null)
            {
                return NotFound();

            }

            return View(obj);
        }


        public IActionResult Delete(int id) {
            if (id == null)
            {
                return NotFound();

            }
            var obj = _sellerService.findByID(id);
            if (obj==null)
            {
                return NotFound();

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
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
