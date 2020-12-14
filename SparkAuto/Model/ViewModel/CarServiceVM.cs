using SparkAuto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Model.ViewModel
{
    public class CarServiceVM
    {
        public Car Car { get; set; }
        public ServiceHeader ServiceHeader { get; set; }

        public ServiceType ServiceType { get; set; }

        public List<ServiceType> ServiceTypesList { get; set; }

        public List<ServiceShoppingCart> ServiceShoppingCart { get; set; }
    }
}
