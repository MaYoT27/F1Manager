using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1Manager.Database;
using F1Manager.Entities;
using F1Manager.Models;

namespace F1Manager.Services
{
    public class RaceService : IRaceService
    {
        private readonly AppDbContext _dbContext;

        public RaceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Validate(RaceModel race, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(race.Name))
                errors.Add("Name is empty");
            if (string.IsNullOrEmpty(race.Track))
                errors.Add("Track name is empty");
            if (race.Date.Year < 1950 || race.Date.Year > 2100)
                errors.Add("Year must be between 1950 and 2100");
            if (race.Length < 2000)
                errors.Add("Length must be greater than 2000");

            var races = GetAll(null);

            var raceFound = races.Result.Where(t => t.Name == race.Name);
            if (raceFound.Count() != 0)
                errors.Add($"Race with name '{race.Name}' already exist");

            raceFound = races.Result.Where(t => t.Date.Date == race.Date.Date);
            if (raceFound.Count() != 0)
                errors.Add($"Race with date {race.Date.ToShortDateString()} already exist");

            if (errors.Count == 0)
                return true;
            else
                return false;
        }

        public async Task Add(RaceModel race)
        {
            var entity = new RaceEntity
            {
                Name = race.Name,
                Track = race.Track,
                Date = race.Date,
                Length = race.Length,
            };

            await _dbContext.Races.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RaceEntity>> GetAll(string name)
        {
            IQueryable<RaceEntity> racesQuery = _dbContext.Races;

            if (!string.IsNullOrEmpty(name))
            {
                racesQuery = racesQuery.Where(x => x.Name.Contains(name));
            }

            var products = await racesQuery.ToListAsync();
            return products;
        }
    }
}