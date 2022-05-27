using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1Manager.Database;
using F1Manager.Entities;
using F1Manager.Models;

namespace F1Manager.Services
{
    public class DriverService : IDriverService
    {
        private readonly AppDbContext _dbContext;

        public DriverService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Validate(DriverModel driver, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(driver.FullName))
                errors.Add("Full name is empty");
            if (driver.Number < 1 || driver.Number > 99)
                errors.Add("Number must be between 1 and 99");
            if (string.IsNullOrEmpty(driver.Team))
                errors.Add("Team name is empty");

            var drivers = GetAll(null);

            var driverFound = drivers.Result.Where(t => t.Number == driver.Number);
            if (driverFound.Count() != 0)
                errors.Add($"Driver with number {driver.Number} already exist");

            driverFound = drivers.Result.Where(t => t.Team == driver.Team);
            if (driverFound.Count() >= 2 && !string.IsNullOrEmpty(driver.Team))
                errors.Add($"Team '{driver.Team}' already has 2 drivers");

            if (errors.Count == 0)
                return true;
            else
                return false;
        }

        public async Task Add(DriverModel driver)
        {
            var entity = new DriverEntity
            {
                FullName = driver.FullName,
                Number = driver.Number,
                Team = driver.Team,
            };

            await _dbContext.Drivers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DriverEntity>> GetAll(string name)
        {
            IQueryable<DriverEntity> driversQuery = _dbContext.Drivers;

            if (!string.IsNullOrEmpty(name))
            {
                driversQuery = driversQuery.Where(x => x.FullName.Contains(name));
            }

            var drivers = await driversQuery.ToListAsync();
            return drivers;
        }
    }
}