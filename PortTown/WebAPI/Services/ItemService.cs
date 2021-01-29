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
        private readonly ISellableRepository SellableRepository;

        public ItemService(ICraftableRepository craftableRepository, IResourceBatchRepository resourceBatchRepository,
            IItemRepository itemRepository, ISellableRepository sellableRepository)
        {
            CraftableRepository = craftableRepository;
            ResourceBatchRepository = resourceBatchRepository;
            ItemRepository = itemRepository;
            SellableRepository = sellableRepository;
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
            // Save the sellable for the item for later usage
            var sellable = item.Sellable;
            item.Sellable = null;
            // Create the item entity
            item = await ItemRepository.CreateAsync(item);
            // Update and create the sellable
            sellable.Item = item;
            await SellableRepository.CreateAsync(sellable);
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