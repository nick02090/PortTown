using System;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class TownService : ITownService
    {
        private readonly ITownRepository townRepository;
        private readonly IBuildingRepository buildingRepository;
        private readonly IItemRepository itemRepository;

        public TownService(ITownRepository _townRepository,
            IBuildingRepository _buildingRepository,
            IItemRepository _itemRepository)
        {
            townRepository = _townRepository;
            buildingRepository = _buildingRepository;
            itemRepository = _itemRepository;
        }

        public async Task ResetAsync(Guid id)
        {
            var town = await townRepository.GetAsync(id);
            town.Level = 1;

            foreach(var building in town.Buildings)
            {
                await buildingRepository.DeleteAsync(building.Id);
            }

            foreach (var item in town.Items)
            {
                await itemRepository.DeleteAsync(item.Id);
            }
        }
    }
}