﻿namespace VehicleManager.Models
{
    public class PagedResultViewModel<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; } //Count of items in database
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
