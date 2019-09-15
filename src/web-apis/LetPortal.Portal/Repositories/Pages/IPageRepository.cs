﻿using LetPortal.Core.Persistences;
using LetPortal.Portal.Entities.Pages;
using LetPortal.Portal.Models.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetPortal.Portal.Repositories.Pages
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        Task<Page> GetOneByNameAsync(string name);

        Task<List<ShortPageModel>> GetAllShortPagesAsync();

        Task<List<ShortPortalClaimModel>> GetShortPortalClaimModelsAsync();
    }
}
