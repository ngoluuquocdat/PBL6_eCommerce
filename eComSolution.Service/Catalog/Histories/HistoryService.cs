using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.Catalog.Histories;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.Catalog.Histories
{
    public class HistoryService : IHistoryService
    {
        private readonly EComDbContext _context;
        public HistoryService(EComDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddHistory(int userId, int productId)
        {
            var current_history = await _context.Histories.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if(current_history!=null)
            {
                current_history.Date = DateTime.Now;
                current_history.Count += 1;
                _context.Histories.Update(current_history);
            }
            else
            {
                var history_item = new History()
                {
                    UserId = userId,
                    ProductId = productId,
                    Date = DateTime.Now,
                    Count = 1
                };
                _context.Histories.Add(history_item);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<List<HistoryVm>>> GetHistory(int userId)
        {
            List<HistoryVm> data = new List<HistoryVm>();
            var query = from h in _context.Histories
                        join p in _context.Products on h.ProductId equals p.Id
                        where h.UserId == userId
                        select new {h, p};

            if(query==null || query.Count()==0)
                return new ApiResult<List<HistoryVm>>(false, Message:"History of this user is empty.");
            
            var list_records = await query.OrderByDescending(x=>x.h.Date).ToListAsync();  

            foreach(var record in list_records)
            {
                string path = "";
                int productId = record.p.Id;
                var image =  await _context.ProductImages
                    .Where(x=>x.ProductId==productId && x.IsDefault==true)
                    .FirstOrDefaultAsync();
                if(image!=null) path = image.ImagePath;
                data.Add(new HistoryVm()
                {
                    Id = record.h.Id,
                    UserId = record.h.UserId,
                    ProductId = record.p.Id,
                    ProductName = record.p.Name,
                    Date = record.h.Date,
                    Count = record.h.Count,
                    ThumbnailImage = path
                });
            }
            
            return new ApiResult<List<HistoryVm>>(true, ResultObj:data);
        }
    }
}