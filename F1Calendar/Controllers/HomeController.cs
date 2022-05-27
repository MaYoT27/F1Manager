using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using F1Manager.Models;
using F1Manager.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace F1Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRaceService _raceService;

        public HomeController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            var drivers = await _raceService.GetAll(name);
            return View(drivers);
        }

        public IActionResult AddRace()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RaceModel race)
        {
            List<string> errors = new List<string>();
            bool valid = _raceService.Validate(race, out errors);

            var viewModel = new ErrorViewModelCustom
            {
                Errors = errors,
            };

            if (valid)
            {
                viewModel.MainMessage = "Race added successfully!";
                await _raceService.Add(race);
            }
            else
            {
                viewModel.MainMessage = "Validation errors!";
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}