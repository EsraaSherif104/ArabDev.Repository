using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification.UserSpecification
{
    public class UserSpecification
    {
        public string? Id { get; set; }
        public string? Job { get; set; }

        public string? Address { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int maxpagesize = 50;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxpagesize) ? int.MaxValue : value;
        }

        private string? _search;
        public string? Search
        {
            get => _search; set => _search = value?.Trim().ToLower();
        }
    }
}