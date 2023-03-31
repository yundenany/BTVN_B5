using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.IO;
using System.Web;

namespace BTVN_B5_5.Models
{
   /* using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;*/
    public class CartItem
    {
        public int Id { get; set; }
        [DisplayName("Tên Sách")]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Money
        {
            get { return Quantity * Price; }
        }
    }
}