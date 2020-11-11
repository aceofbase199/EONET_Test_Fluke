using EONET.Web.Models;
using System;
using System.Collections.Generic;

namespace EONET.Web.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<List<T>> ToPagedResponse<T>(List<T> pagedData, PaginationFilter filter, int totalRecords)
        {
            var response = new PagedResponse<List<T>>(pagedData, filter.PageNumber, filter.PageSize);
            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            response.TotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.TotalRecords = totalRecords;

            return response;
        }
    }
}
