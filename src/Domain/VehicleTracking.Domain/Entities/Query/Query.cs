using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTracking.Domain.Entities.Query
{
    public class Query
    {
        [Required]
        public int page { get; set; }
        [Required]

        public int limit { get; set; }


        public Query(int pageNo, int itemsPerPage)
        {
            page = page;
            limit = itemsPerPage;

            if (page <= 0)
            {
                page = 1;
            }

            if (limit <= 0)
            {
                limit = 10;
            }
        }
    }
}
