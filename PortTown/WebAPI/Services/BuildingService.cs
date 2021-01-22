using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ITownRepository townRepository;
        private readonly IBuildingRepository buildingRepository;

        public BuildingService(ITownRepository _townRepository,
            IBuildingRepository _buildingRepository)
        {
            townRepository = _townRepository;
            buildingRepository = _buildingRepository;
        }

        public async Task<bool> CanUpgradeAsync(Guid id)
        {
            var building = await buildingRepository.GetAsync(id);

            var town = await townRepository.GetAsync(building.Town.Id);

            if (building.Level + 1 > town.Level)
                return false;
            else
                return true;
        }
    }
}