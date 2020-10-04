namespace Hotel.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract_
    {
        [Key]
        public int ID_Contract { get; set; }

        public int ID_Client { get; set; }

        [StringLength(50)]
        public string Type_of_contact { get; set; }

        public int? Cost { get; set; }

        public virtual Clients Clients { get; set; }
    }
}
