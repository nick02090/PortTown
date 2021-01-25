using Domain;
using Domain.Template;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly ICraftableRepository CraftableRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IItemRepository ItemRepository;

        public ItemService(ICraftableRepository craftableRepository, IResourceBatchRepository resourceBatchRepository,
            IItemRepository itemRepository)
        {
            CraftableRepository = craftableRepository;
            ResourceBatchRepository = resourceBatchRepository;
            ItemRepository = itemRepository;
        }

        #region Template
        public async Task AddDataToTemplate(Item item)
        {
            // Get the craftable parent
            var craftable = item.ParentCraftable;
            // Save the cost for the craftable for later usage
            var craftableCosts = craftable.RequiredResources;
            craftable.RequiredResources = null;
            // Create the craftable entity and update the item
            craftable = await CraftableRepository.CreateAsync(craftable);
            item.ParentCraftable = craftable;
            // Create the craftable costs
            foreach (var craftableCost in craftableCosts)
            {
                craftableCost.Craftable = craftable;
                await ResourceBatchRepository.CreateAsync(craftableCost);
            }
            // Create the item entity
            await ItemRepository.CreateAsync(item);

        }

        public async Task AddInitialTemplateData()
        {
            foreach (var item in ItemTemplate.Template())
            {
                await AddDataToTemplate(item);
            }
        }

        public async Task<JSONFormatter> CheckInitialTemplateData()
        {
            var hasData = new JSONFormatter();
            var items = await ItemRepository.GetTemplateAsync();
            if (items.Any())
            {
                hasData.AddField("HasData", true);
            }
            else
            {
                hasData.AddField("HasData", false);
            }
            return hasData;
        }
        #endregion
    }
}