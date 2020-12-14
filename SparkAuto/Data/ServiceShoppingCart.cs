using SparkAuto.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Model
{
    public class ServiceShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int CardId { get; set; }

        public int ServiceTypeId { get; set; }

        [ForeignKey("CardId")]
        public virtual Car Car { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
