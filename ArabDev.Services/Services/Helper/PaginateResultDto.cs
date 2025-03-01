using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.Helper
{
    public class PaginateResultDto<T>
    {
        public PaginateResultDto(int pageindex, int pageSize, int totalCount, IReadOnlyList<T> data)
        {
            this.pageindex = pageindex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int pageindex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}