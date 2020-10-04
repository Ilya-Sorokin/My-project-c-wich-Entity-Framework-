namespace Hotel.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rooms
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Room { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Room_Type { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Client { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime Date_Arrive { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime Date_Departure { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cost_Per_Day { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual Free_Rooms Free_Rooms { get; set; }
    }
}
