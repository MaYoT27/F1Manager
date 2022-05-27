using System.Collections.Generic;
using System.Threading.Tasks;
using F1Manager.Entities;
using F1Manager.Models;

namespace F1Manager.Services
{
    public interface IRaceService
    {
        bool Validate(RaceModel race, out List<string> errors);

        Task Add(RaceModel race);

        Task<IEnumerable<RaceEntity>> GetAll(string name);
    }
}