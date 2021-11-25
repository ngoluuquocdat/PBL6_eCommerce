using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using HistoryAPI.ViewModels;
using HistoryAPI.ViewModels.Common;

namespace HistoryAPI.Service
{
    public interface IHistoryService
    {
        Task<ApiResult<List<HistoryVm>>> GetHistory(int userId);

        Task<ApiResult<int>> AddHistory(int userId, int productId);

        Task<List<Function>> GetPermissions(int userId);
    }
}