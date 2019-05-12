using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.Persistance
{
    public class BaseEntity
    {
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
