using DataAccess;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBIntegrationWithDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository _repository;
        public ProductController(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var item =await _repository.GetAllProductsAsync();
            return View(item);
        }
        public async Task<IActionResult> Create()
        {
            await Task.Delay(0);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _repository.AddProductAsync(item);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product item = await _repository.GetProductByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product item)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateProductAsync(item.Id, item);
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public async Task<ActionResult> Details(string id)
        {
            return View(await _repository.GetProductByIdAsync(id));
        }
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product item = await _repository.GetProductByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            await _repository.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
