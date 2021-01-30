using Domain.Template;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ResourceBatchService : IResourceBatchService
    {
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly ISellableRepository SellableRepository;

        public ResourceBatchService(IResourceBatchRepository resourceBatchRepository, 
            ISellableRepository sellableRepository)
        {
            //Repositories
            ResourceBatchRepository = resourceBatchRepository;
            SellableRepository = sellableRepository;
        }

        public async Task AddInitialTemplateData()
        {
            foreach (var resourceBatch in MarketplaceTemplate.Template())
            {
                var sellable = resourceBatch.Sellable;
                resourceBatch.Sellable = null;
                // Create resource batch
                var newResourceBatch = await ResourceBatchRepository.CreateAsync(resourceBatch);
                sellable.ResourceBatch = newResourceBatch;
                // create sellable
                await SellableRepository.CreateAsync(sellable);
            }
            
        }

        public async Task<JSONFormatter> CheckInitialTemplateData()
        {
            var hasData = new JSONFormatter();
            var buildings = await ResourceBatchRepository.GetTemplateAsync();
            if (buildings.Any())
            {
                hasData.AddField("HasData", true);
            }
            else
            {
                hasData.AddField("HasData", false);
            }
            return hasData;
        }
    }
}