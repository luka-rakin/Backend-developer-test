﻿namespace VehicleManager.DTO
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } 
        public int TotalCount { get; set; } //Count of items in database
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
