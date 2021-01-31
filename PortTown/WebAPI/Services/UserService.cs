using Domain;
using Domain.Enums;
using Domain.Template;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly ITownRepository TownRepository;
        private readonly ITownService TownService;
        private readonly IBuildingRepository BuildingRepository;
        private readonly IItemRepository ItemRepository;
        private readonly ICraftableRepository CraftableRepository;
        private readonly IProductionBuildingRepository ProductionBuildingRepository;
        private readonly IStorageRepository StorageRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IUpgradeableRepository UpgradeableRepository;
        private readonly ISellableRepository SellableRepository;

        private readonly IAppSettings AppSettings;

        private readonly IBuildingService BuildingService;

        public UserService(IUserRepository userRepository, ITownRepository townRepository,
            ITownService townService, IAppSettings appSettings, IBuildingRepository buildingRepository,
            IItemRepository itemRepository, ICraftableRepository craftableRepository, 
            IProductionBuildingRepository productionBuildingRepository, IStorageRepository storageRepository,
            IResourceBatchRepository resourceBatchRepository, IUpgradeableRepository upgradeableRepository,
            ISellableRepository sellableRepository, IBuildingService buildingService)
        {
            UserRepository = userRepository;
            TownRepository = townRepository;

            TownService = townService;
            BuildingRepository = buildingRepository;
            ItemRepository = itemRepository;
            CraftableRepository = craftableRepository;
            ProductionBuildingRepository = productionBuildingRepository;
            StorageRepository = storageRepository;
            ResourceBatchRepository = resourceBatchRepository;
            UpgradeableRepository = upgradeableRepository;
            SellableRepository = sellableRepository;

            AppSettings = appSettings;

            BuildingService = buildingService;
        }

        public async Task<User> Authenticate(User user, string password)
        {
            var passwordHasher = new PasswordHasher();
            if (!passwordHasher.VerifyHashedPassword(user.Password, password))
            {
                return null; // invalid password
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await UserRepository.UpdateAsync(user);

            // load town data
            var town = await TownService.GetTown(user.Town.Id);
            user.Town = town;

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<JSONFormatter> CheckAvailability(string email)
        {
            var availability = new JSONFormatter();
            var user = await UserRepository.GetByEmailAsync(email);

            if (user == null)
            {
                availability.AddField("Availability", true);
            }
            else
            {
                availability.AddField("Availability", false);
            }

            return availability;
        }

        public async Task<User> CreateUserWithTown(User user, string townName)
        {
            var userdb = await UserRepository.CreateAsync(user);
            var town = TownTemplate.Template();
            town.Name = townName;
            town.User = userdb;
            town = await TownService.CreateFromTemplate(town);
            userdb.Town = town;
            return userdb;
        }

        public async Task DeleteUserWithTownAsync(Guid userId)
        {
            var user = await UserRepository.GetAsync(userId);

            var town = await TownRepository.GetAsync(user.Town.Id);

            var buildings = town.Buildings;

            var items = town.Items;

            var upgradeableCosts = await ResourceBatchRepository.GetByUpgradeableAsync(town.Upgradeable.Id);
            foreach (var upgradeableCost in upgradeableCosts)
            {
                var upgradeableCostDel = await ResourceBatchRepository.GetForDeleteAsync(upgradeableCost.Id);
                await ResourceBatchRepository.DeleteAsync(upgradeableCostDel);
            }
            var UpgradeableDel = await UpgradeableRepository.GetForDeleteAsync(town.Upgradeable.Id);
            await UpgradeableRepository.DeleteAsync(UpgradeableDel);

            foreach (var item in items)
            {
                var itemDb = await ItemRepository.GetAsync(item.Id);

                var itemSellableDel = await SellableRepository.GetForDeleteAsync(itemDb.Sellable.Id);
                await SellableRepository.DeleteAsync(itemSellableDel);

                var itemDel = await ItemRepository.GetForDeleteAsync(itemDb.Id);
                await ItemRepository.DeleteAsync(itemDel);

                var itemParentCraftableDel = await CraftableRepository.GetForDeleteAsync(itemDb.ParentCraftable.Id);
                await CraftableRepository.DeleteAsync(itemParentCraftableDel);
            }

            foreach (var building in buildings)
            {
                var buildingDb = await BuildingService.GetBuilding(building.Id);

                // Delete child
                if (buildingDb.BuildingType == BuildingType.Production)
                {
                    // Delete production building
                    var productionDel = await ProductionBuildingRepository.GetForDeleteAsync(buildingDb.ChildProductionBuilding.Id);
                    await ProductionBuildingRepository.DeleteAsync(productionDel);
                } 
                else
                {
                    // Delete storage
                    var storageResources = await ResourceBatchRepository.GetByStorageAsync(buildingDb.ChildStorage.Id);
                    foreach (var storageResource in storageResources)
                    {
                        // Delete storage resources
                        var storageResourcerDel = await ResourceBatchRepository.GetForDeleteAsync(storageResource.Id);
                        await ResourceBatchRepository.DeleteAsync(storageResourcerDel);
                    }
                    var storageDel = await StorageRepository.GetForDeleteAsync(buildingDb.ChildStorage.Id);
                    await StorageRepository.DeleteAsync(storageDel);
                }

                // Delete upgradeable
                var buildingUpgradeableCosts = await ResourceBatchRepository.GetByUpgradeableAsync(buildingDb.Upgradeable.Id);
                foreach (var buildingUpgradeableCost in buildingUpgradeableCosts)
                {
                    // Delete cost
                    var upgradeCostDel = await ResourceBatchRepository.GetForDeleteAsync(buildingUpgradeableCost.Id);
                    await ResourceBatchRepository.DeleteAsync(upgradeCostDel);
                }
                var upgradeableDel = await UpgradeableRepository.GetForDeleteAsync(buildingDb.Upgradeable.Id);
                await UpgradeableRepository.DeleteAsync(upgradeableDel);

                // Get building craftable costs before delete a building
                var buildingCraftable = buildingDb.ParentCraftable;
                var buildingCraftableCosts = await ResourceBatchRepository.GetByCraftableAsync(buildingDb.ParentCraftable.Id);

                // Delete building
                var buildingDel = await BuildingRepository.GetForDeleteAsync(buildingDb.Id);
                await BuildingRepository.DeleteAsync(buildingDel);

                // Delete craftable
                foreach (var buildingCraftableCost in buildingCraftableCosts)
                {
                    // Delete craftable cost
                    var buildingCraftableCostDel = await ResourceBatchRepository.GetForDeleteAsync(buildingCraftableCost.Id);
                    await ResourceBatchRepository.DeleteAsync(buildingCraftableCostDel);
                }
                var buildingCraftableDel = await CraftableRepository.GetForDeleteAsync(buildingCraftable.Id);
                await CraftableRepository.DeleteAsync(buildingCraftableDel);
            }
            // Delete town
            var townDel = await TownRepository.GetForDeleteAsync(town.Id);
            await TownRepository.DeleteAsync(townDel);

            // Delete user
            var userDel = await UserRepository.GetForDeleteAsync(userId);
            await UserRepository.DeleteAsync(userDel);

            return;
        }

        public async Task Logout(string token)
        {
            var user = await UserRepository.GetByTokenAsync(token);

            if (user == null)
            {
                return;
            }

            user.Token = null;

            await UserRepository.UpdateAsync(user);

            return;
        }
    }
}