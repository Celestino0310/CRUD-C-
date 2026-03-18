using CrudMvc.Models;
using CrudMvc.Models.ViewModels;
using CrudMvc.Services;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Edit() { 
            return View();
        }
        public IActionResult Details() {
            return View(); 
        }
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//protege essa requisição de alguns ataques
        public IActionResult Delete(Seller seller) {
            _sellerService.Delete(seller);
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]//serve para informar que essa função vai coloca algo no db
        [ValidateAntiForgeryToken]//protege essa requisição de alguns ataques
        public IActionResult Create(Seller seller) {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
