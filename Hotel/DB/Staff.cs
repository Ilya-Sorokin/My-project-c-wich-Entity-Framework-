namespace Hotel.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Service_ = new HashSet<Service_>();
        }

        [Key]
        public int ID_Staff { get; set; }

        [Required]
        [StringLength(50)]
        public string Full_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        public int Salary { get; set; }

        [Required]
        [StringLength(50)]
        public string Telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service_> Service_ { get; set; }
    }
}
