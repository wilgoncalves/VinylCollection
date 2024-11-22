using Microsoft.AspNetCore.Mvc;
using VinylCollection.Interfaces;
using VinylCollection.Models;
using VinylCollection.Repositories;

namespace VinylCollection.Controllers
{
    public class TitlesController : Controller
    {
        private readonly ITitleRepository _titleRepository;

        public TitlesController(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public IActionResult Index()
        {
            var titles = _titleRepository.GetAll();
            return View(titles);
        }

        public IActionResult Details(string title)
        {
            var titulo = _titleRepository.Get(title);
            if (titulo == null)
            {
                return NotFound();
            }

            return View(titulo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Title title)
        {
            if (ModelState.IsValid)
            {
                _titleRepository.Add(title);
                return RedirectToAction("Index");
            }
            return View(title);
        }

        [HttpPost]
        public IActionResult Edit(string name, int recordedYear)
        {
            if (ModelState.IsValid)
            {
                _titleRepository.Update(name, recordedYear);
                return RedirectToAction("Index");
            }
            return View(name, recordedYear);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var titulo = _titleRepository.GetById(id);
            if (titulo == null)
            {
                return NotFound();
            }

            _titleRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
