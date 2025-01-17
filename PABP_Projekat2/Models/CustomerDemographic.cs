﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PABP_Projekat2.Models
{
    public partial class CustomerDemographic
    {
        public CustomerDemographic()
        {
            CustomerCustomerDemos = new HashSet<CustomerCustomerDemo>();
        }

        [Key]
        [Column("CustomerTypeID")]
        [StringLength(10)]
        [Required]
        public string CustomerTypeId { get; set; }
        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }

        [InverseProperty(nameof(CustomerCustomerDemo.CustomerType))]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
    }
}
