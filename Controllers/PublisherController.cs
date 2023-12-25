using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> PublisherAdminPanel()
        {
            var publishers = await _publisherRepository.GetAll();
            return View(publishers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) return View(null);

            var publisherVM = new EditPublisherViewModel()
            {
                FirstName = publisher.FirstName,
                LastName = publisher.LastName,
                Biography = publisher.Biography,
                DateOfBrith = publisher.DateOfBrith
            };

            return View(publisherVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPublisherViewModel editPublisherVM)
        {
            var publisher = await _publisherRepository.GetByIdAsyncNoTracking(id);
            if (publisher == null) return View();

            var publisherNew = new Publisher()
            {
                Id = id,
                FirstName = editPublisherVM.FirstName,
                LastName = editPublisherVM.LastName,
                Biography = editPublisherVM.Biography,
                DateOfBrith = editPublisherVM.DateOfBrith
            };

            _publisherRepository.Update(publisherNew);
            return RedirectToAction("PublisherAdminPaneL");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) return View();

            return View(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePublisher(int id) 
        {
            var publiser = await _publisherRepository.GetByIdAsync(id);
            if(publiser == null) return View();

            _publisherRepository.Delete(publiser);
            return RedirectToAction("PublisherAdminPaneL");
        }
    }
}
