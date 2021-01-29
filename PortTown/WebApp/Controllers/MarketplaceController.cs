using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.Resources;
using WebApp.Models.TownViewModels;

namespace WebApp.Controllers
{
    public class MarketplaceController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SellList()
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            TownItemsViewModel items = new TownItemsViewModel();
            var item1 = new Models.Items.Item(Models.Items.ItemType.JEWELRY, 5, FoodStored);
            item1.ToSell = FoodStored;
            var item2 = new Models.Items.Item(Models.Items.ItemType.POTTERY, 10, FoodStored);
            item2.ToSell = FoodStored;
            items.Items.Add(item1);
            items.Items.Add(item2);
            return PartialView(items);
        }

        [ChildActionOnly]
        public ActionResult BuyList()
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            TownItemsViewModel items = new TownItemsViewModel();
            var item1 = new Models.Items.Item(Models.Items.ItemType.JEWELRY, 5, FoodStored);
            item1.ToSell = FoodStored;
            var item2 = new Models.Items.Item(Models.Items.ItemType.POTTERY, 10, FoodStored);
            item2.ToSell = FoodStored;
            items.Items.Add(item1);
            items.Items.Add(item2);
            return PartialView(items);
        }
    }
}