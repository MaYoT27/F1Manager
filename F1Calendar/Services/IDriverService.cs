using System.Collections.Generic;
using System.Threading.Tasks;
using F1Manager.Entities;
using F1Manager.Models;

namespace F1Manager.Services
{
    public interface IDriverService
    {
        bool Validate(DriverModel driver, out List<string> errors);

        Task Add(DriverModel driver);

        Task<IEnumerable<DriverEntity>> GetAll(string name);
    }
}