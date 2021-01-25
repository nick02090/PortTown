using Domain;
using System;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ItemService : IItemService
    {
        #region Template
        public Task AddDataToTemplate(Item item)
        {
            throw new NotImplementedException();
        }

        public Task AddInitialTemplateData()
        {
            throw new NotImplementedException();
        }

        public Task<JSONFormatter> CheckInitialTemplateData()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}