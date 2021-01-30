using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IResourceBatchService
    {
        Task<JSONFormatter> CheckInitialTemplateData();
        Task AddInitialTemplateData();
    }
}
