using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Histories;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Histories
{
    public interface IHistoryService
    {
        Task<ApiResult<List<HistoryVm>>> GetHistory(int userId);

        Task<int> AddHistory(int userId, int productId);
    }
}