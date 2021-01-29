using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Template
{
    public class MarketplaceTemplate
    {

        private static readonly Sellable coalPrice = BaseTemplate.GenerateSellable(5);
        private static readonly Sellable ironPrice = BaseTemplate.GenerateSellable(7);
        private static readonly Sellable foodPrice = BaseTemplate.GenerateSellable(1);
        private static readonly Sellable stonePrice = BaseTemplate.GenerateSellable(3);
        private static readonly Sellable woodPrice = BaseTemplate.GenerateSellable(2);


        public static ICollection<ResourceBatch> Template()
        {

            ICollection<ResourceBatch> sellableResources = new List<ResourceBatch>()
            {
                BaseTemplate.GenerateResourceBatch(coalPrice, ResourceType.Coal),
                BaseTemplate.GenerateResourceBatch(ironPrice, ResourceType.Iron),
                BaseTemplate.GenerateResourceBatch(foodPrice, ResourceType.Food),
                BaseTemplate.GenerateResourceBatch(stonePrice, ResourceType.Stone),
                BaseTemplate.GenerateResourceBatch(woodPrice, ResourceType.Wood)
            };

            return sellableResources;
        }
    }
}
