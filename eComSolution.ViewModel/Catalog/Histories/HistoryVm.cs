using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.Catalog.Histories
{
    public class HistoryVm
    {
        public int Id { get; set; }
        public int UserId {get; set;}
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ThumbnailImage {get; set;}    // url of product's thumbnail image
        public DateTime Date { get; set; }
        public int Count {get; set;}    // how many time user view this product
    }
}