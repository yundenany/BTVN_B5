namespace BTVN_B5_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        //[ForeignKey("Book")]
        [Column(Order = 0)]
        //[ForeignKey("Book")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookID { get; set; }

        [Key]
        //[ForeignKey("Order")]
        [Column(Order = 1)]
        //[ForeignKey("Order")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderNo { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
