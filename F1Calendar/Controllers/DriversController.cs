using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using F1Manager.Models;
using F1Manager.Services;
using System.Collections.Generic;

namespace F1Manager.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public IActionResult AddDriversTeams()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(DriverModel driver)
        {
            List<string> errors = new List<string>();
            bool valid = _driverService.Validate(driver, out errors);

            var viewModel = new ErrorViewModelCustom
            {
                Errors = errors,
            };

            if (valid)
            {
                viewModel.MainMessage = "Driver added successfully!";
                await _driverService.Add(driver);
            }
            else
            {
                viewModel.MainMessage = "Validation errors!";
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DriversTeams(string name)
        {
            var drivers = await _driverService.GetAll(name);
            return View(drivers);
        }
    }
}