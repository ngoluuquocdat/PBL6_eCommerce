using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Entities;
using HistoryAPI.ViewModels;
using HistoryAPI.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly EComDbContext _context;
        public HistoryService(EComDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> AddHistory(int userId, int productId)
        {
            // check product
            var product = await _context.Products.Where(x=>x.Id==productId&&x.IsDeleted==false).FirstOrDefaultAsync();
            if(product==null)
            {
                return new ApiResult<int>(false, Message:$"Không tìm thấy sản phẩm có Id: {productId}");
            }
            var current_history = await _context.Histories.Where(x => x.ProductId == productId && x.UserId==userId).FirstOrDefaultAsync();
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
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Thêm vào lịch sử thành công");
        }

        public async Task<ApiResult<List<HistoryVm>>> GetHistory(int userId)
        {
            List<HistoryVm> data = new List<HistoryVm>();
            var query = from h in _context.Histories
                        join p in _context.Products on h.ProductId equals p.Id
                        where h.UserId == userId
                        select new {h, p};

            if(query==null || query.Count()==0)
                return new ApiResult<List<HistoryVm>>(false, Message:"Bạn chưa xem sản phẩm nào!");
            
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
        public async Task<List<Function>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return null;

            var query = from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _permission in _context.Permissions on _groupuser.GroupId equals _permission.GroupId
            join _function in _context.Functions on _permission.FunctionId equals _function.Id
            where _user.Id == userId
            select new Function { 
                ActionName = _function.ActionName
            }; 
            return query.Distinct().ToList();
        }
    }
}