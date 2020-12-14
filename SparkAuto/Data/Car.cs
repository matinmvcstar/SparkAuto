using SparkAuto.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        public string style { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string KM { get; set; }
        public string Color { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string UserId { get; set; }
    }
}
