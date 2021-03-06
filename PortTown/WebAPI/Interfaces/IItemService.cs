﻿using Domain;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IItemService
    {
        Task<JSONFormatter> CheckInitialTemplateData();
        Task AddInitialTemplateData();
        Task AddDataToTemplate(Item item);
    }
}
